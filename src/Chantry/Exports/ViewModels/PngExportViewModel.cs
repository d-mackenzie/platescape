using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Exports.ViewModels
{
	internal class PngExportViewModel : BaseViewModel
	{
		private PngExportSettings _pngExportSettings;

		public int PixelsPerStud
		{
			get { return _pngExportSettings.PixelsPerStud; }
			set { _pngExportSettings.PixelsPerStud = value; RaisePropertyChanged(); }
		}

		public bool DrawStuds
		{
			get { return _pngExportSettings.DrawStuds; }
			set { _pngExportSettings.DrawStuds = value; RaisePropertyChanged(); }
		}

		public bool DrawOutlines
		{
			get { return _pngExportSettings.DrawOutlines; }
			set { _pngExportSettings.DrawOutlines = value; RaisePropertyChanged(); }
		}

		public PngExportViewModel(PngExportSettings pngExportSettings)
		{
			_pngExportSettings = pngExportSettings;
		}
	}
}
