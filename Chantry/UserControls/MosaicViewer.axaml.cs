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

		private Dictionary<int, IImage> _cachedStudOverlayImages = new Dictionary<int, IImage>();

		private LdPart _baseplate;
		private LdPart _element;

		public LdPart Baseplate
		{
			get { return _baseplate; }
			set { SetAndRaise(BaseplateProperty, ref _baseplate, value); InvalidateStudOverlayImageCache(); }
		}

		public LdPart Element
		{
			get { return _element; }
			set { SetAndRaise(BaseplateProperty, ref _element, value); InvalidateStudOverlayImageCache(); }
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

			if (!_cachedStudOverlayImages.ContainsKey(Zoom))
				_cachedStudOverlayImages[Zoom] = GetStudOverlayImage();

			for (int x = (int)(ImageRenderBounds.Left); x < (int)(ImageRenderBounds.Right); x += (int)(_cachedStudOverlayImages[Zoom].Size.Width))
			{
				for (int y = (int)(ImageRenderBounds.Top); y < (int)(ImageRenderBounds.Bottom); y += (int)(_cachedStudOverlayImages[Zoom].Size.Height))
				{
					var targetRect = new Rect(x, y, _cachedStudOverlayImages[Zoom].Size.Width, _cachedStudOverlayImages[Zoom].Size.Height);
					context.DrawImage(_cachedStudOverlayImages[Zoom], targetRect);
				}
			}
		}

		private IImage GetStudOverlayImage()
		{
			var studOverlay = new RenderTargetBitmap(new PixelSize(Baseplate.Size.Width * ZoomScale, Baseplate.Size.Height * ZoomScale));

			using (var overlayDrawingContext = studOverlay.CreateDrawingContext())
			{
				var pen = new Pen(new SolidColorBrush(Colors.DarkGray, 0.25), 2);
				double studSize = (double)ZoomScale * 0.6d;
				int studOffset = (int)(ZoomScale * 0.2d);

				overlayDrawingContext.DrawRectangle(pen, new Rect(studOverlay.Size));

				for (int x = studOffset; x < studOverlay.PixelSize.Width; x += ZoomScale)
				{
					for (int y = studOffset; y < studOverlay.PixelSize.Height; y += ZoomScale)
					{
						overlayDrawingContext.DrawEllipse(null, pen, new Rect(x, y, studSize, studSize));
					}
				}
			}

			return studOverlay;
		}

		private void InvalidateStudOverlayImageCache()
		{
			Debug.WriteLine("cache invalidated.");
			_cachedStudOverlayImages.Clear();
		}
	}
}
