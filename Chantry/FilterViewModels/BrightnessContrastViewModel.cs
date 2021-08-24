using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.App.FilterViewModels
{
    public class BrightnessContrastViewModel : BaseViewModel
    {
        private BrightnessContrastFilter m_brightnessContrastFilter;

        public int Brightness
        {
            get { return m_brightnessContrastFilter.Brightness; }
            set { m_brightnessContrastFilter.Brightness = value; RaisePropertyChanged(); }
        }

        public int Contrast
        {
            get { return m_brightnessContrastFilter.Contrast; }
            set { m_brightnessContrastFilter.Contrast = value; RaisePropertyChanged(); }
        }

        public BrightnessContrastViewModel(BrightnessContrastFilter brightnessContrastFilter)
        {
            m_brightnessContrastFilter = brightnessContrastFilter;
        }
    }
}
