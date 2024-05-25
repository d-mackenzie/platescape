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
	internal class PngExportViewModel : BaseViewModel
	{
		private PngExporter _pngExporter;

		public int PixelsPerStud
		{
			get { return _pngExporter.PixelsPerStud; }
			set { _pngExporter.PixelsPerStud = value; RaisePropertyChanged(); }
		}

		public bool DrawStuds
		{
			get { return _pngExporter.DrawStuds; }
			set { _pngExporter.DrawStuds = value; RaisePropertyChanged(); }
		}

		public bool DrawOutlines
		{
			get { return _pngExporter.DrawOutlines; }
			set { _pngExporter.DrawOutlines = value; RaisePropertyChanged(); }
		}

		public PngExportViewModel(PngExporter pngExportSettings)
		{
			_pngExporter = pngExportSettings;
		}
	}
}
