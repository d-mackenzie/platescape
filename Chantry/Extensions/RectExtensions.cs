using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Extensions
{
    public static class RectExtensions
    {
        public static bool IsEntirelyWithin(this Rect rect, Rect other)
        {
            return rect.Intersect(other) == rect;
        }
    }
}
