using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using TeethInc.Chantry.Core.Algorithms;
using TeethInc.Chantry.Core.Models;
using System.Reactive;
using ReactiveUI;
using TeethInc.Chantry.Exports.ViewModels;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Helpers;

namespace TeethInc.Chantry.ViewModels
{
	public class ProjectViewModel : BaseViewModel
	{
		private Project _project;
		private ObservableCollection<IFilterViewModel> _filterViewModels = new ObservableCollection<IFilterViewModel>();
		private List<ColorChipViewModel> _allowedColors;
		private string? _filename = null;
		private IFileDialog _fileDialog;

		private List<Algorithm> _mosaicAlgorithms = new List<Algorithm>()
		{
			new BayerMatrix(),
			new FloydSteinberg(),
			new NearestColor()
		};

		private Point _pan;
		private double _zoom = 1d;

		// project properties.

		public string Name => _project.Name;

		public string? Filename
		{
			get { return _filename; }
			set { _filename = value; RaisePropertyChanged(); }
		}

		// moasic properties.

		public List<SKSizeI> BaseplateSizes => LdrawService.Baseplates.Select(x => x.Size).Distinct().ToList();

		public List<SKSizeI> ElementSizes => LdrawService.Elements.Select(x => x.Size).Distinct().ToList();

		public List<ColorChipViewModel> AllowedColors => _allowedColors;

		public SKSizeI BaseplateSize
		{
			get { return _project.ExtentSettings.BaseplateSize; }
			set { _project.ExtentSettings.BaseplateSize = value; RaiseMosaicPropertiesChanged(); }
		}

		public SKSizeI ElementSize
		{
			get { return _project.ExtentSettings.ElementSize; }
			set { _project.ExtentSettings.ElementSize = value; RaiseMosaicPropertiesChanged(); }
		}

		public int BaseplateExtentWidth
		{
			get { return _project.ExtentSettings.BaseplateExtent.Width; }
			set
			{
				_project.ExtentSettings.BaseplateExtent = new SKSizeI(value, _project.ExtentSettings.BaseplateExtent.Height);
				RaisePropertyChanged();
				RaiseMosaicPropertiesChanged();
			}
		}

		public int BaseplateExtentHeight
		{
			get { return _project.ExtentSettings.BaseplateExtent.Height; }
			set
			{
				_project.ExtentSettings.BaseplateExtent = new SKSizeI(_project.ExtentSettings.BaseplateExtent.Width, value);
				RaisePropertyChanged();
				RaiseMosaicPropertiesChanged();
			}
		}

		public string SizeInfo => _project.ExtentSettings.ToPhysicalSizeDisplayString();

		public Algorithm MosaicAlgorithm
		{
			get { return _project.AlgorithmSettings.Algorithm; }
			set { _project.AlgorithmSettings.Algorithm = value; RaiseMosaicPropertiesChanged(); }
		}

		public List<Algorithm> MosaicAlgorithms => _mosaicAlgorithms;

		// filter properties.

		public ObservableCollection<IFilterViewModel> FilterViewModels => _filterViewModels;

		public AmiBitmap FilteredImage => _project.FilteredImage.AsAvaloniaMediaImagingBitmap();

		public AmiBitmap MosaicImage => _project.Mosaic.Image.AsAvaloniaMediaImagingBitmap();

		public Mosaic Mosaic => _project.Mosaic;

		// export properties.

		internal PngExporterViewModel PngExporter { get; private set; }


		// commands.
		public void SaveProjectCommand()
		{
			if (_filename is null)
			{
				_fileDialog
					.ShowSaveDialog($"{Name}.json", FileFilters.Projects)
					.ContinueWith(x => SaveProject(x.Result));
			}
			else
			{
				SaveProject(_filename);
			}
		}

		private void SaveProject(string? filename)
		{
			if (filename is null)
				return;

			SerializeProject(filename);

			var config = ApplicationHelper.LoadConfiguration();
			config.Mru = [filename];
			ApplicationHelper.SaveConfiguration(config);

			_filename = filename;
		}

		public void CloseProjectCommand()
		{
			// todo: check dirty.

			// todo: save.

			Close();
		}

		private void Close()
		{
			// ProjectViewModel = null;
		}


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

		public ReactiveCommand<Unit, Unit> AddNewBrightnessContrastFilterCommand { get; }
		public ReactiveCommand<Unit, Unit> AddNewMultiplyFilterCommand { get; }
		public ReactiveCommand<Unit, Unit> AddNewSaturationFilterCommand { get; }

		public ReactiveCommand<IFilterViewModel, Unit> RemoveFilterCommand { get; }

		public ProjectViewModel(Project project)
		{
			_project = project;
			_project.Filters.ForEach(AddFilter);
			_project.AlgorithmSettings.Algorithm = _mosaicAlgorithms.FirstOrDefault(x => x.DisplayName == _project.AlgorithmSettings.Algorithm.DisplayName);

			_allowedColors = new List<ColorChipViewModel>(
				LdrawService.Colors
					.Select(x => BuildAllowedColorViewModel(x, _project.AlgorithmSettings.AllowedColors.Contains(x)))
					.OrderBy(x => x.LdColor.Order));

			AddNewBrightnessContrastFilterCommand = ReactiveCommand.Create<Unit>(x => AddFilter(new BrightnessContrastFilter()));
			AddNewMultiplyFilterCommand = ReactiveCommand.Create<Unit>(x => AddFilter(new MultiplyFilter()));
			AddNewSaturationFilterCommand = ReactiveCommand.Create<Unit>(x => AddFilter(new SaturationFilter()));
			RemoveFilterCommand = ReactiveCommand.Create<IFilterViewModel>(RemoveFilter);

			Zoom = 4;

			PngExporter = new PngExporterViewModel(_project.PngExporter, this);

			_fileDialog = new FileDialog();
		}

		public void AddFilter(Filter filter)
		{
			if (!_project.Filters.Contains(filter))
				_project.Filters.Add(filter);

			IFilterViewModel viewModel = FilterViewModelFactory.ConstructFilterViewModel(filter);
			viewModel.PropertyChanged += FilterPropertyChanged;
			FilterViewModels.Add(viewModel);
		}

		public void RemoveFilter(IFilterViewModel filterViewModel)
		{
			filterViewModel.PropertyChanged -= FilterPropertyChanged;

			int index = FilterViewModels.IndexOf(filterViewModel);
			FilterViewModels.RemoveAt(index);
			_project.Filters.RemoveAt(index);

			RaiseFilterPropertyChanged();
		}

		private void AllowedColorChanged(object? sender, PropertyChangedEventArgs e)
		{
			_project.AlgorithmSettings.AllowedColors = _allowedColors.Where(x => x.IsSelected).Select(x => x.LdColor).ToArray();
			RaisePropertyChanged(nameof(MosaicImage));
		}

		public void SerializeProject(string filename)
		{
			string json = _project.Serialize();
			File.WriteAllText(filename, json);
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
			RaisePropertyChanged(nameof(BaseplateSize));
			RaisePropertyChanged(nameof(ElementSize));
			RaisePropertyChanged(nameof(SizeInfo));
		}

		private ColorChipViewModel BuildAllowedColorViewModel(LdColor color, bool isAllowed)
		{
			var ret = new ColorChipViewModel(color, _project.AlgorithmSettings.AllowedColors.Contains(color));
			ret.PropertyChanged += AllowedColorChanged;

			return ret;
		}
	}
}
