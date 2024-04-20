using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.UserControls
{
	public partial class MosaicViewer : ImageViewer
	{
		public static readonly DirectProperty<MosaicViewer, SKSizeI> BaseplateSizeProperty =
			AvaloniaProperty.RegisterDirect<MosaicViewer, SKSizeI>(nameof(BaseplateSize), x => x.BaseplateSize, (o, v) => o.BaseplateSize = v);

		public static readonly DirectProperty<MosaicViewer, SKSizeI> ElementSizeProperty =
			AvaloniaProperty.RegisterDirect<MosaicViewer, SKSizeI>(nameof(ElementSize), x => x.ElementSize, (o, v) => o.ElementSize = v);

		private Dictionary<string, IImage> _cachedStudOverlayImages = new Dictionary<string, IImage>();

		private SKSizeI _baseplateSize;
		private SKSizeI _elementSize;

		public SKSizeI BaseplateSize
		{
			get { return _baseplateSize; }
			set { SetAndRaise(BaseplateSizeProperty, ref _baseplateSize, value); }
		}

		public SKSizeI ElementSize
		{
			get { return _elementSize; }
			set { SetAndRaise(BaseplateSizeProperty, ref _elementSize, value); }
		}

		static MosaicViewer()
		{
			AffectsRender<MosaicViewer>(BaseplateSizeProperty);
			AffectsRender<MosaicViewer>(ElementSizeProperty);
		}

		public MosaicViewer()
		{
			Layers.Add(DrawStuds);
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void DrawStuds(DrawingContext context)
		{
			if (Zoom < 6)
				return;

			IImage studOverlayImage = GetStudOverlayImage();

			for (int x = (int)(ImageRenderBounds.Left); x < (int)(ImageRenderBounds.Right); x += (int)(studOverlayImage.Size.Width))
			{
				for (int y = (int)(ImageRenderBounds.Top); y < (int)(ImageRenderBounds.Bottom); y += (int)(studOverlayImage.Size.Height))
				{
					var targetRect = new Rect(x, y, studOverlayImage.Size.Width, studOverlayImage.Size.Height);

					if (this.Bounds.Intersects(targetRect))
						context.DrawImage(studOverlayImage, targetRect);
				}
			}
		}

		private IImage GetStudOverlayImage()
		{
			string cacheKey = BuildCacheKey(_baseplateSize, _elementSize, Zoom);

			if (_cachedStudOverlayImages.ContainsKey(cacheKey))
				return _cachedStudOverlayImages[cacheKey];

			var studOverlay = new RenderTargetBitmap(new PixelSize(BaseplateSize.Width * ZoomScale, BaseplateSize.Height * ZoomScale));

			using (var overlayDrawingContext = studOverlay.CreateDrawingContext())
			{
				var baseplatePen = new Pen(new SolidColorBrush(Colors.DarkGray, 0.5d), 2.0d);
				var studPen = new Pen(new SolidColorBrush(Colors.DarkGray, 0.5d), 0.5d);
				var elementPen = new Pen(new SolidColorBrush(Colors.DarkGray, 0.5d), 0.5d);

				double studSize = ZoomScale * 0.3d;
				double studOffset = ZoomScale * 0.5d;

				// baseplate outlines.

				overlayDrawingContext.DrawRectangle(baseplatePen, new Rect(studOverlay.Size));

				// stud circles.

				for (double x = 0; x < studOverlay.PixelSize.Width; x += ZoomScale)
				{
					for (double y = 0; y < studOverlay.PixelSize.Height; y += ZoomScale)
					{
						overlayDrawingContext.DrawEllipse(null, studPen, new Point(x + studOffset, y + studOffset), studSize, studSize);
					}
				}

				// element outlines.

				for (double x = 0; x < studOverlay.PixelSize.Width; x += ZoomScale * ElementSize.Width)
				{
					for (double y = 0; y < studOverlay.PixelSize.Height; y += ZoomScale * ElementSize.Height)
					{
						overlayDrawingContext.DrawRectangle(null, elementPen, new Rect(x, y, ZoomScale * ElementSize.Width, ZoomScale * ElementSize.Height));
					}
				}

				_cachedStudOverlayImages[cacheKey] = studOverlay;

				return _cachedStudOverlayImages[cacheKey];
			}
		}

		private string BuildCacheKey(SKSizeI baseplateSize, SKSizeI elementSize, int zoom)
		{
			return $"{baseplateSize};{elementSize};{zoom}";
		}
	}
}
