using Newtonsoft.Json;
using SkiaSharp;
using System;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Sources
{
	public class ScaledImageSource : ISource
	{
		private SKBitmap _image = null;
		private bool _disposedValue;
		private SKSizeI _maximumSize;

		public string Base64
		{
			get
			{
				using (var pixmap = Image.PeekPixels())
				{
					var filters = SKPngEncoderFilterFlags.NoFilters;
					int compress = 9;
					var encoderOptions = new SKPngEncoderOptions(filters, compress);

					using (var data = pixmap.Encode(encoderOptions))
					{
						return Convert.ToBase64String(data.Span);
					}
				}
			}
			set
			{
				var data = Convert.FromBase64String(value);
				_image = SKBitmap.Decode(data);
			}
		}

		[JsonIgnore]
		public SKBitmap Image
		{
			get
			{
				if (_image is null)
					_image = new SKBitmap(_maximumSize.Width, _maximumSize.Height);

				return _image;
			}
			set
			{
				_image = value.Resize(value.Info.Size.GetSizeToFit(_maximumSize), SKFilterQuality.High);
            }
        }

		public ScaledImageSource(int maximumSize)
		{
			_maximumSize = new SKSizeI(maximumSize, maximumSize);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_image.Dispose();
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
