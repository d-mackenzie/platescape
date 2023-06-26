using SkiaSharp;
using System.Diagnostics;
using System.Linq;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Services
{
    public class ColorService
    {
        private LdrawService _ldrawService;
        private int[] _allowedColors;
        private LdColor[] _allowedLdColors;
        private LdColor[,,] _closestLdColorCache = new LdColor[64, 64, 64];
        private float[,,,] _distanceCache;
        private int[] _ldColorIndex;

        public int[] AllowedColors
        {
            get { return _allowedColors; }
            set
            {
                _allowedColors = value;

                if (_allowedColors.Length == 0)
                    _allowedColors = new int[] { 0 };

                _allowedLdColors = _ldrawService.GetColors(_allowedColors).ToArray();

                BuildClosestLdColorCache();
            }
        }

        public ColorService(LdrawService ldrawService)
        {
            _ldrawService = ldrawService;
            BuildDistanceCache();

            AllowedColors = new int[0];
        }

        public LdColor GetClosestLdColor(SKColor color)
        {
            return _closestLdColorCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2];
        }

        private void BuildDistanceCache()
        {
            var sw = Stopwatch.StartNew();

            var ldColors = _ldrawService.Colors;
            _distanceCache = new float[64, 64, 64, ldColors.Count];
            _ldColorIndex = new int[ldColors.Select(x => x.Number).Max() + 1];

            for (int i = 0; i < ldColors.Count; i++)
            {
                _ldColorIndex[ldColors[i].Number] = i;
            }

            foreach (LdColor ldColor in ldColors)
            {
                int ldColorIndex = _ldColorIndex[ldColor.Number];

                for (int r = 0; r < 256; r += 4)
                {
                    for (int g = 0; g < 256; g += 4)
                    {
                        for (int b = 0; b < 256; b += 4)
                        {
                            SKColor color = new SKColor((byte)r, (byte)g, (byte)b);
                            _distanceCache[r >> 2, g >> 2, b >> 2, ldColorIndex] = color.DistanceFrom(ldColor.Color);
                        }
                    }
                }
            }

            Debug.WriteLine($"BuildDistanceCache(): {sw.ElapsedMilliseconds}ms.");
        }

        private void BuildClosestLdColorCache()
        {
            var sw = Stopwatch.StartNew();

            for (int r = 0; r < 256; r += 4)
            {
                for (int g = 0; g < 256; g += 4)
                {
                    for (int b = 0; b < 256; b += 4)
                    {
                        _closestLdColorCache[r >> 2, g >> 2, b >> 2] = CalculateClosestLdColor(new SKColor((byte)r, (byte)g, (byte)b));
                    }
                }
            }

            Debug.WriteLine($"BuildClosestLdColorCache(): {sw.ElapsedMilliseconds}ms.");
        }

        private LdColor CalculateClosestLdColor(SKColor color)
        {
            float minDistance = float.MaxValue;
            LdColor closestColor = null;

            foreach (LdColor ldColor in _allowedLdColors)
            {
                // get distance.

                float distance = _distanceCache[color.Red >> 2, color.Green >> 2, color.Blue >> 2, _ldColorIndex[ldColor.Number]];

                if (distance < minDistance)
                {
                    closestColor = ldColor;
                    minDistance = distance;
                }
            }

            return closestColor;
        }
    }
}
