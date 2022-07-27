using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public class SaturationViewModel : BaseViewModel
    {
        private SaturationFilter m_saturationFilter;

        public int Saturation
        {
            get { return (int)(m_saturationFilter.Saturation * 100); }
            set { m_saturationFilter.Saturation = value / 100d; RaisePropertyChanged(); }
        }

        public SaturationViewModel(SaturationFilter saturationFilter)
        {
            m_saturationFilter = saturationFilter;
        }
    }
}
