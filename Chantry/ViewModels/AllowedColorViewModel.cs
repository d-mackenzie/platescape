using Avalonia.Media;
using SkiaSharp;
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
        public LdColor LdColor { get; set; }
        public bool IsAllowed { get; set; }

        public SKColor TickColor => LdColor.IsDarkColor ? SKColors.White : SKColors.Black;

        public AllowedColorViewModel(LdColor ldColor, bool isAllowed)
        {
            LdColor = ldColor;
            IsAllowed = isAllowed;
        }
    }
}
