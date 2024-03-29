using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.IO;

namespace TeethInc.Chantry.Core.Sources
{
    public class FileSource : ISource
    {
        private SKBitmap _image = null;
        private string _filename = "";
        private bool _disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _image?.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
