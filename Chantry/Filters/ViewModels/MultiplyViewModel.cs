using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public class MultiplyViewModel : BaseViewModel
    {
        private MultiplyFilter m_multiplyFilter;

        public double Factor
        {
            get { return m_multiplyFilter.Factor * 10; }
            set { m_multiplyFilter.Factor = value / 10; RaisePropertyChanged(); }
        }

        public MultiplyViewModel(MultiplyFilter multiplyFilter)
        {
            m_multiplyFilter = multiplyFilter;
        }
    }
}
