using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core
{
    public class Mosaic
    {
        public int BaseplatePartNumber { get; set; }

        public int ElementPartNumber { get; set; }

        public Size BaseplateExtent { get; set; }

        public int[] LdrawColors { get; set; }

        public Size ElementExtent
        {
            get
            {
                Size baseplateSize = m_ldrawService.Baseplates.FirstOrDefault(x => x.Number == BaseplatePartNumber).Size;
                Size elementSize = m_ldrawService.Elements.FirstOrDefault(x => x.Number == ElementPartNumber).Size;

                return new Size(
                    baseplateSize.Width * BaseplateExtent.Width / elementSize.Width,
                    baseplateSize.Height * BaseplateExtent.Height / elementSize.Height);
            }
        }

        private LdrawService m_ldrawService;

        public Mosaic()
        {
            m_ldrawService = new LdrawService();
        }

        public Mosaic(LdrawService ldrawService)
        {
            m_ldrawService = ldrawService;
        }



        public int[,] GetMosaic(Bitmap sourceImage)
        {
            var ret = new int[ElementExtent.Width, ElementExtent.Height];

            float scale = sourceImage.Width / ElementExtent.Width;

            for (int x = 0; x < ElementExtent.Width; x++)
            {
                for (int y = 0; y < ElementExtent.Height; y++)
                {
                    Color[] colors = GetColorsInRegion(sourceImage, scale, new Point(x, y));

                    // average the colors.

                    double avgRed = colors.Average(x => x.R);
                    double avgGreen = colors.Average(x => x.G);
                    double avgBlue = colors.Average(x => x.B);

                    Color avgColor = Color.FromArgb((int)avgRed, (int)avgGreen, (int)avgBlue);

                    // find nearest ldraw color.

                    int minDistance = 255;
                    int closestColor = 0;

                    foreach (int ldrawColor in LdrawColors)
                    {
                        // get color value.

                        Color candidateColor = m_ldrawService.GetColor(ldrawColor).Color;

                        // get distance.

                        int distance = Math.Abs(avgColor.R - candidateColor.R) +
                            Math.Abs(avgColor.G - candidateColor.G) +
                            Math.Abs(avgColor.B - candidateColor.B);

                        if (distance < minDistance)
                        {
                            closestColor = ldrawColor;
                            minDistance = distance;
                        }
                    }

                    ret[x, y] = closestColor;
                }
            }

            return ret;
        }

        private Color[] GetColorsInRegion(Bitmap sourceImage, float scale, Point offset)
        {
            int intScale = (int)scale;
            var ret = new Color[intScale * intScale];
            int index = 0;

            Point p = new Point((int)(scale * offset.X), (int)(scale * offset.Y));

            for (int x = p.X; x < p.X + intScale; x++)
            {
                for (int y = p.Y; y < p.Y + intScale; y++)
                {
                    ret[index++] = sourceImage.GetPixel(x, y);
                }
            }

            return ret;
        }


    }
}
