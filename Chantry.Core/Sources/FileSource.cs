using Newtonsoft.Json;
using SkiaSharp;
using System.IO;

namespace TeethInc.Chantry.Core.Sources
{
    public class FileSource : ISource
    {
        private SKBitmap m_image = null;
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

        [JsonIgnore]
        public SKBitmap Image
        {
            get
            {
                if (m_image == null)
                {
                    if (File.Exists(Filename))
                    {
                        m_image = SKBitmap.Decode(Filename);
                    }
                    else
                    {
                        m_image = new SKBitmap(50, 50);
                    }
                }

                return m_image;
            }
        }
    }
}
