using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.MosaicAlgorithms
{
    public class BayerMatrix : MosaicAlgorithm
    {
        private int m_matrixSize = 2;

        private float[,] m_matrix;

        public override LdColor GetColor(int x, int y, Color sourceColor, ColorService colorService)
        {
            float spread = 255f / colorService.AllowedColors.ToList().Count();
            float matrixValue = m_matrix[x % m_matrixSize, y % m_matrixSize];

            float red = sourceColor.R + spread * (matrixValue - 0.5f);
            float green =  sourceColor.G + spread * (matrixValue - 0.5f);
            float blue = sourceColor.B + spread * (matrixValue - 0.5f);

            return colorService.GetClosestLdColor(Color.FromArgb(
                Math.Clamp((int)(red), 0, 255),
                Math.Clamp((int)(green), 0, 255),
                Math.Clamp((int)(blue), 0, 255)));
        }

        public override void Reset(Size size)
        {
            m_matrix = new float[2, 2]
                {
                    { 0.25f, 0.75f },
                    { 1f,    0.5f }
                };
        }
    }
}
