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
		public static readonly DirectProperty<MosaicViewer, LdPart> BaseplateProperty =
			AvaloniaProperty.RegisterDirect<MosaicViewer, LdPart>(nameof(Baseplate), x => x.Baseplate, (o, v) => o.Baseplate = v);

		public static readonly DirectProperty<MosaicViewer, LdPart> ElementProperty =
			AvaloniaProperty.RegisterDirect<MosaicViewer, LdPart>(nameof(Element), x => x.Element, (o, v) => o.Element = v);

		private Dictionary<string, IImage> _cachedStudOverlayImages = new Dictionary<string, IImage>();

		private LdPart _baseplate;
		private LdPart _element;

		public LdPart Baseplate
		{
			get { return _baseplate; }
			set { SetAndRaise(BaseplateProperty, ref _baseplate, value); }
		}

		public LdPart Element
		{
			get { return _element; }
			set { SetAndRaise(BaseplateProperty, ref _element, value); }
		}

		static MosaicViewer()
		{
			AffectsRender<MosaicViewer>(BaseplateProperty);
			AffectsRender<MosaicViewer>(ElementProperty);
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
			string cacheKey = BuildCacheKey(_baseplate, _element, Zoom);

			if (_cachedStudOverlayImages.ContainsKey(cacheKey))
				return _cachedStudOverlayImages[cacheKey];

			var studOverlay = new RenderTargetBitmap(new PixelSize(Baseplate.Size.Width * ZoomScale, Baseplate.Size.Height * ZoomScale));

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

				for (double x = 0; x < studOverlay.PixelSize.Width; x += ZoomScale * Element.Size.Width)
				{
					for (double y = 0; y < studOverlay.PixelSize.Height; y += ZoomScale * Element.Size.Height)
					{
						overlayDrawingContext.DrawRectangle(null, elementPen, new Rect(x, y, ZoomScale * Element.Size.Width, ZoomScale * Element.Size.Height));
					}
				}

				_cachedStudOverlayImages[cacheKey] = studOverlay;

				return _cachedStudOverlayImages[cacheKey];
			}
		}

		private string BuildCacheKey(LdPart baseplate, LdPart element, int zoom)
		{
			return $"{baseplate.Number};{element.Number};{zoom}";
		}
	}
}
