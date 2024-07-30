using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Filters.ViewModels
{
	public class SaturationViewModel : BaseFilterViewModel<SaturationFilter>
	{
		public int Factor
		{
			get { return (int)(Filter.Saturation * 100); }
			set { Filter.Saturation = value / 100d; RaisePropertyChanged(); RaisePropertyChanged(nameof(DisplayFactor)); }
		}

		public string DisplayFactor => $"{Factor.ToString("N0")}%";

		public SaturationViewModel(SaturationFilter filter) : base(filter)
		{ }
	}
}
