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
            get { return Filter.Factor * 10; }
            set { Filter.Factor = value / 10; RaisePropertyChanged(); }
        }

        public MultiplyViewModel(MultiplyFilter filter) : base(filter)
        { }
    }
}
