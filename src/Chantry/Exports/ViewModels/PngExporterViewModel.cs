using Avalonia.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Exports.ViewModels
{
	internal class PngExporterViewModel : ExporterViewModel<PngExporter>
	{
		private const string PIXELS_PER_STUD_VALIDATION_MESSAGE = "Enter a number 1 - 94";

		[Required(ErrorMessage = PIXELS_PER_STUD_VALIDATION_MESSAGE)]
		[Range(1, 94, ErrorMessage = PIXELS_PER_STUD_VALIDATION_MESSAGE)]
		public int? PixelsPerStud
		{
			get { return _exporter.PixelsPerStud; }
			set
			{
				_exporter.PixelsPerStud = value ?? throw new DataValidationException(PIXELS_PER_STUD_VALIDATION_MESSAGE);
				RaisePropertyChanged();
			}
		}

		public bool DrawStuds
		{
			get { return _exporter.DrawStuds; }
			set { _exporter.DrawStuds = value; RaisePropertyChanged(); }
		}

		public bool DrawOutlines
		{
			get { return _exporter.DrawOutlines; }
			set { _exporter.DrawOutlines = value; RaisePropertyChanged(); }
		}

		public PngExporterViewModel(PngExporter exporter, ProjectViewModel parent) : base(exporter, parent) { }
	}
}
