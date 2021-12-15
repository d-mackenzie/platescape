using Newtonsoft.Json;
using System.Drawing;
using System.IO;

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

        [JsonIgnore]
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
                    }
                }

                return m_image;
            }
        }
    }
}
