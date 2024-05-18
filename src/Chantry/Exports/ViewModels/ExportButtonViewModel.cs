using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Exports.ViewModels
{
	internal class ExportButtonViewModel : BaseViewModel
	{
		private readonly ExportSettings _exportSettings;

		public ExportButtonViewModel(ExportSettings exportSettings)
		{
			_exportSettings = exportSettings;
		}
	}
}
