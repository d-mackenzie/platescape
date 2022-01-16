using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.ViewModels
{
    public class AllowedColorViewModel
    {
        public bool IsAllowed { get; set; }
        public LdColor LdColor { get; set; }

        public AllowedColorViewModel(bool isAllowed, LdColor ldColor)
        {
            IsAllowed = isAllowed;
            LdColor = ldColor;
        }
    }
}
