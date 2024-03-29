using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public class BrightnessContrastViewModel : BaseFilterViewModel<BrightnessContrastFilter>
    {
        public int Brightness
        {
            get { return Filter.Brightness; }
            set { Filter.Brightness = value; RaisePropertyChanged(); }
        }

        public int Contrast
        {
            get { return Filter.Contrast; }
            set { Filter.Contrast = value; RaisePropertyChanged(); }
        }

        public BrightnessContrastViewModel(BrightnessContrastFilter filter) : base(filter)
        { }
    }
}
