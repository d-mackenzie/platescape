using SkiaSharp;
using System;

namespace TeethInc.Chantry.Core.Sources
{
    public interface ISource : IDisposable
    {
        public SKBitmap Image { get; }
    }
}
