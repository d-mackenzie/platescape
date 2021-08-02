using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Sources
{
    public class FileSource : ISource
    {
        private Bitmap m_image = null;
        private string m_filename = "";

        public string Filename
        {
            get { return m_filename; }
            set
            {
                m_filename = value;
                m_image = null;
            }
        }

        public Bitmap Image
        {
            get
            {
                if (m_image == null)
                {
                    if (File.Exists(Filename))
                    {
                        m_image = new Bitmap(Filename);
                    }
                    else
                    {
                        m_image = new Bitmap(50, 50);

                        for (int x = 0; x < 50; x++)
                            for (int y = 0; y < 50; y++)
                                m_image.SetPixel(x, y, Color.LawnGreen);
                    }
                }

                return m_image;
            }
        }
    }
}
