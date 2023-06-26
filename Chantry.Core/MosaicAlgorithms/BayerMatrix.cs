using SkiaSharp;
using System;
using System.Linq;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class BayerMatrix : MosaicAlgorithm
    {
        private int _matrixSize = 2;

        private float[,] _matrix;

        public override string DisplayName => "Bayer Matrix";

        public override LdColor GetColor(int x, int y, SKColor sourceColor, ColorService colorService)
        {
            float spread = 255f / colorService.AllowedColors.ToList().Count();
            float matrixValue = _matrix[x % _matrixSize, y % _matrixSize];

            float red = sourceColor.Red + spread * (matrixValue - 0.5f);
            float green =  sourceColor.Green + spread * (matrixValue - 0.5f);
            float blue = sourceColor.Blue + spread * (matrixValue - 0.5f);

            return colorService.GetClosestLdColor(new SKColor(
                (byte)Math.Clamp((int)(red), 0, 255),
                (byte)Math.Clamp((int)(green), 0, 255),
                (byte)Math.Clamp((int)(blue), 0, 255)));
        }

        public override void Reset(SKSizeI size)
        {
            _matrix = new float[2, 2]
                {
                    { 0.25f, 0.75f },
                    { 1f,    0.5f }
                };
        }
    }
}
