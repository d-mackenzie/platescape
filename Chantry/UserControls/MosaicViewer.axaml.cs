using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using SkiaSharp;
using System.Collections.Generic;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Extensions;

namespace TeethInc.Chantry.UserControls
{
    public partial class MosaicViewer : ImageViewer
    {

        private Dictionary<int, IImage> m_studOverlays = new Dictionary<int, IImage>();

        public static readonly StyledProperty<Mosaic> MosaicProperty =
            AvaloniaProperty.Register<MosaicViewer, Mosaic>(nameof(Mosaic));

        public Mosaic Mosaic
        {
            get { return GetValue(MosaicProperty); }
            set { SetValue(MosaicProperty, value); }
        }


        public MosaicViewer()
        {
            AffectsRender<MosaicViewer>(MosaicProperty);

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

            if (!m_studOverlays.ContainsKey(Zoom))
                m_studOverlays[Zoom] = GetStudOverlayImage();

            for (int x = (int)(ImageRenderBounds.Left); x < (int)(ImageRenderBounds.Right); x += (int)(m_studOverlays[Zoom].Size.Width))
            {
                for (int y = (int)(ImageRenderBounds.Top); y < (int)(ImageRenderBounds.Bottom); y += (int)(m_studOverlays[Zoom].Size.Height))
                {
                    context.DrawImage(m_studOverlays[Zoom], new Rect(x, y, m_studOverlays[Zoom].Size.Width, m_studOverlays[Zoom].Size.Height));
                }
            }
        }

        private IImage GetStudOverlayImage()
        {
            var studOverlay = new RenderTargetBitmap(new PixelSize(Mosaic.Baseplate.Size.Width * ZoomScale, Mosaic.Baseplate.Size.Height * ZoomScale));

            using (var overlayDrawingContext = studOverlay.CreateDrawingContext(null))
            {
                var pen = new Pen(new SolidColorBrush(Colors.DarkGray, 0.25), 2);
                double studSize = (double)ZoomScale * 0.6d;
                int studOffset = (int)(ZoomScale * 0.2d);

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
    }
}
