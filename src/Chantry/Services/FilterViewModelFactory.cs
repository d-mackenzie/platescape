using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Services
{
	public static class FilterViewModelFactory
	{
		public static IFilterViewModel ConstructFilterViewModel(Filter filter)
		{
			return filter switch
			{
				BrightnessContrastFilter => new BrightnessContrastViewModel((BrightnessContrastFilter)filter),
				SaturationFilter => new SaturationViewModel((SaturationFilter)filter),
				MultiplyFilter => new MultiplyViewModel((MultiplyFilter)filter),
				_ => throw new ArgumentException($"{filter.GetType()} not handled.")
			};
		}
	}
}
