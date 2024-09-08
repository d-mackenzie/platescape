using Newtonsoft.Json;
using SkiaSharp;
using System;

namespace TeethInc.Chantry.Core.Sources
{
	public class ImageSource : ISource
	{
		private SKBitmap _image = null;
		private bool _disposedValue;

		public string Base64
		{
			get
			{
				using (var pixmap = _image.PeekPixels())
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
					_image = new SKBitmap(50, 50);

				return _image;
			}
			set
			{
				_image = value;
			}
		}

		public string OriginalFilename { get; set; }

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
