using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public class BaseFilterViewModel<T> : BaseViewModel, IFilterViewModel where T : Filter
    {
        private T _filter;

        protected T Filter => _filter;

        public string DisplayName => _filter.DisplayName;

        public bool Enabled
        {
            get { return _filter.Enabled; }
            set { _filter.Enabled = value; RaisePropertyChanged(); }
        }

        public BaseFilterViewModel(T filter)
        {
            _filter = filter;
        }
    }
}
