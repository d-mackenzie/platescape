using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Extensions;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Extensions;
using System.Collections.ObjectModel;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Ldraw;
using AmiBitmap = Avalonia.Media.Imaging.Bitmap;
using System.IO;
using SkiaSharp;
using Avalonia;
using System;
using TeethInc.Chantry.Services;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Models;
using System.Reactive;
using ReactiveUI;
using System.Diagnostics;

namespace TeethInc.Chantry.ViewModels
{
	public class ProjectViewModel : BaseViewModel
	{
		private Project _project;
		private ObservableCollection<IFilterViewModel> _filters = new ObservableCollection<IFilterViewModel>();
		private List<MosaicAlgorithm> _mosaicAlgorithms = new List<MosaicAlgorithm>()
		{
			new BayerMatrix(),
			new FloydSteinberg(),
			new NearestColor()
		};

		private LdrawService _ldrawService;

		private Point _pan;
		private double _zoom = 1d;

		// project properties.

		public string Name => _project.Name;

		// source properties.

		public FileSource Source
		{
			get { return (FileSource)_project.Source; }
		}

		// filter properties.

		public ObservableCollection<IFilterViewModel> Filters => _filters;
		public AmiBitmap UnfilteredImage => _project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();
		public AmiBitmap FilteredImage => _project.FilteredImage.AsAvaloniaMediaImagingBitmap();
		public AmiBitmap MosaicImage => _project.Mosaic.Image.AsAvaloniaMediaImagingBitmap();

		// moasic properties.

		public List<LdPart> Baseplates => _ldrawService.Baseplates;
		public List<LdPart> Elements => _ldrawService.Elements;
		public List<AllowedColorViewModel> Colors =>
			_ldrawService.Colors.Select(x =>
				new AllowedColorViewModel(x, _project.AllowedColors.Contains(x.Number))).OrderBy(x => x.LdColor.Hue).ToList();

		public LdPart Baseplate
		{
			get { return _ldrawService.GetPart(_project.BaseplatePartNumber); }
			set { _project.BaseplatePartNumber = value.Number; RaiseMosaicPropertiesChanged(); }
		}

		public LdPart Element
		{
			get { return _ldrawService.GetPart(_project.ElementPartNumber); }
			set { _project.ElementPartNumber = value.Number; RaiseMosaicPropertiesChanged(); }
		}

		public int BaseplateExtentWidth
		{
			get { return _project.BaseplateExtent.Width; }
			set
			{
				_project.BaseplateExtent = new SKSizeI(value, _project.BaseplateExtent.Height);
				RaiseMosaicPropertiesChanged();
			}
		}

		public int BaseplateExtentHeight
		{
			get { return _project.BaseplateExtent.Height; }
			set
			{
				_project.BaseplateExtent = new SKSizeI(_project.BaseplateExtent.Width, value);
				RaiseMosaicPropertiesChanged();
			}
		}

		public string SizeInfo
		{
			get
			{
				int width = _project.BaseplateExtent.Width * Baseplate.Size.Width;
				int height = _project.BaseplateExtent.Height * Baseplate.Size.Height;

				string ret = $"{_project.ElementExtent.Width} elements by {_project.ElementExtent.Height} elements\n";
				ret += $"{width} studs by {height} studs\n";
				ret += $"{MmToCentimetersOrMeters(width * 8)} by {MmToCentimetersOrMeters(height * 8)}\n";
				ret += $"{MmToFeetAndInches(width * 8)} by {MmToFeetAndInches(height * 8)}\n";

				return ret;
			}
		}

		public MosaicAlgorithm MosaicAlgorithm
		{
			get { return _project.MosaicAlgorithm; }
			set { _project.MosaicAlgorithm = value; RaiseMosaicPropertiesChanged(); }
		}

		public List<MosaicAlgorithm> MosaicAlgorithms => _mosaicAlgorithms;

		// view properties.

		public double Zoom
		{
			get { return _zoom; }
			set { _zoom = value; RaisePropertyChanged(); }
		}

		public Point Pan
		{
			get { return _pan; }
			set { _pan = value; RaisePropertyChanged(); }
		}

		public double ZoomMultiplier
		{
			get { return _project.FilteredImage.Info.Size.GetScaleToFill(_project.Mosaic.Image.Info.Size); }
		}

		public Project Project => _project;


		// commands.

		public ReactiveCommand<Type, Unit> AddNewFilterCommand { get; }

		public ReactiveCommand<IFilterViewModel, Unit> RemoveFilterCommand { get; }

		public ReactiveCommand<AllowedColorViewModel, Unit> ToggleColorCommand { get; }

		public ProjectViewModel(Project project)
		{
			_ldrawService = new LdrawService();

			_project = project;
			_project.Filters.ForEach(AddFilterViewModel);
			_project.MosaicAlgorithm = _mosaicAlgorithms.FirstOrDefault(x => x.DisplayName == _project.MosaicAlgorithm.DisplayName);

			AddNewFilterCommand = ReactiveCommand.Create<Type>(AddFilter);
			RemoveFilterCommand = ReactiveCommand.Create<IFilterViewModel>(RemoveFilter);
			ToggleColorCommand = ReactiveCommand.Create<AllowedColorViewModel>(ToggleColor);

			Zoom = 4;
		}

		public void AddFilter(Type filterType)
		{
			Filter? filter = Activator.CreateInstance(filterType) as Filter;

			if (filter is null)
				throw new ArgumentException($"Could not create filter of type {filterType}");

			_project.Filters.Add(filter);
			AddFilterViewModel(filter);
		}

		public void RemoveFilter(IFilterViewModel filterViewModel)
		{
			filterViewModel.PropertyChanged -= FilterPropertyChanged;

			int index = Filters.IndexOf(filterViewModel);
			Filters.RemoveAt(index);
			_project.Filters.RemoveAt(index);

			RaiseFilterPropertyChanged();
		}

		public void ToggleColor(AllowedColorViewModel color)
		{
			int number = color.LdColor.Number;

			if (_project.AllowedColors.Contains(number))
			{
				_project.AllowedColors = _project.AllowedColors.Where(x => x != number).ToArray();
			}
			else
			{
				_project.AllowedColors = _project.AllowedColors.Concat(new int[] { number }).ToArray();
			}

			RaisePropertyChanged(nameof(Colors));
			RaiseMosaicPropertiesChanged();
		}

		public void SerializeProject(string filename)
		{
			string json = _project.Serialize();
			File.WriteAllText(filename, json);
		}

		private void AddFilterViewModel(Filter filter)
		{
			IFilterViewModel viewModel = FilterViewResolver.ConstructFilterViewModel(filter);
			viewModel.PropertyChanged += FilterPropertyChanged;
			Filters.Add(viewModel);
		}

		private void RaiseFilterPropertyChanged()
		{
			RaisePropertyChanged(nameof(FilteredImage));
			RaiseMosaicPropertiesChanged();
		}

		private void FilterPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			RaiseFilterPropertyChanged();
		}

		private void RaiseMosaicPropertiesChanged()
		{
			RaisePropertyChanged(nameof(MosaicImage));
			RaisePropertyChanged(nameof(Baseplate));
			RaisePropertyChanged(nameof(Element));
			RaisePropertyChanged(nameof(SizeInfo));
		}

		private string MmToFeetAndInches(int mm)
		{
			int inches = (int)(mm / 25.4f);

			if (inches < 12)
				return $"{inches}in";

			if (inches % 12 == 0)
				return $"{inches / 12}ft";

			return $"{inches / 12}ft {inches % 12}in";
		}

		private string MmToCentimetersOrMeters(int mm)
		{
			if (mm >= 1000)
				return string.Format("{0:F1}m", mm / 1000d);

			return string.Format("{0:F0}cm", mm / 10d);
		}
	}
}
