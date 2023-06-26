using Newtonsoft.Json;
using SkiaSharp;
using System.IO;

namespace TeethInc.Chantry.Core.Sources
{
    public class FileSource : ISource
    {
        private SKBitmap _image = null;
        private string _filename = "";

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                _image = null;
            }
        }

        [JsonIgnore]
        public SKBitmap Image
        {
            get
            {
                if (_image == null)
                {
                    if (File.Exists(Filename))
                    {
                        _image = SKBitmap.Decode(Filename);
                    }
                    else
                    {
                        _image = new SKBitmap(50, 50);
                    }
                }

                return _image;
            }
        }
    }
}
