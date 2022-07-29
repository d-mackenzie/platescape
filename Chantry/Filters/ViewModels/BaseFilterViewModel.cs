using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public class BaseFilterViewModel<T> : BaseViewModel where T : Filter
    {
        private T m_filter;

        protected T Filter => m_filter;

        public string DisplayName => m_filter.DisplayName;

        public bool Enabled
        {
            get { return m_filter.Enabled; }
            set { m_filter.Enabled = value; RaisePropertyChanged(); }
        }

        public BaseFilterViewModel(T filter)
        {
            m_filter = filter;
        }
    }
}
