using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Filters.ViewModels
{
	public class MultiplyViewModel : BaseFilterViewModel<MultiplyFilter>
	{
		public double Factor
		{
			get { return Filter.Factor; }
			set { Filter.Factor = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(DisplayFactor)); }
		}

		public string DisplayFactor => Filter.Factor.ToString("F2");

		public MultiplyViewModel(MultiplyFilter filter) : base(filter)
		{ }
	}
}
