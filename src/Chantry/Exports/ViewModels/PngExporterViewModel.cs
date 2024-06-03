using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Exports.ViewModels
{
	internal class PngExporterViewModel : ExporterViewModel<PngExporter>
	{
		public int PixelsPerStud
		{
			get { return _exporter.PixelsPerStud; }
			set { _exporter.PixelsPerStud = value; RaisePropertyChanged(); }
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
