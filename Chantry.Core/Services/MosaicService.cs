using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Services
{
    public class MosaicService
    {
        public int BaseplatePartNumber { get; set; }

        public int ElementPartNumber { get; set; }

        public Size BaseplateExtent { get; set; }

        public LdrawColor[] AllowedColors { get; set; }

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

        public MosaicService()
        {
            m_ldrawService = new LdrawService();
        }

        public MosaicService(LdrawService ldrawService)
        {
            m_ldrawService = ldrawService;
        }

        public LdrawColor[,] GetMosaic(Bitmap sourceImage, IMosaicAlgorithm mosaicAlgorithm)
        {
            var ret = new LdrawColor[ElementExtent.Width, ElementExtent.Height];

            float scale = sourceImage.Width / ElementExtent.Width;

            for (int x = 0; x < ElementExtent.Width; x++)
            {
                for (int y = 0; y < ElementExtent.Height; y++)
                {
                    Color[] colors = GetColorsInRegion(sourceImage, scale, new Point(x, y));

                    ret[x, y] = mosaicAlgorithm.GetColor(colors, AllowedColors);
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
