using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;

namespace TeethInc.Chantry.UserControls
{
    public partial class ImageViewer : UserControl
    {
        public static readonly StyledProperty<Bitmap> SourceProperty =
            AvaloniaProperty.Register<ImageViewer, Bitmap>(nameof(Source));

        public static readonly StyledProperty<double> ZoomProperty =
            AvaloniaProperty.Register<ImageViewer, double>(nameof(Zoom));

        public static readonly StyledProperty<Point> PanProperty =
            AvaloniaProperty.Register<ImageViewer, Point>(nameof(Pan));

        private Point m_oldPoint;
        private bool m_isPanning;

        public Bitmap Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public double Zoom
        {
            get { return GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public Point Pan
        {
            get { return GetValue(PanProperty); }
            set { SetValue(PanProperty, value); }
        }

        static ImageViewer()
        {
            AffectsRender<ImageViewer>(SourceProperty);
            AffectsRender<ImageViewer>(ZoomProperty);
            AffectsRender<ImageViewer>(PanProperty);
        }

        public ImageViewer()
        {
            this.PointerWheelChanged += ImageViewer_PointerWheelChanged;
            this.PointerPressed += ImageViewer_PointerPressed;
            this.PointerMoved += ImageViewer_PointerMoved;
            this.PointerReleased += ImageViewer_PointerReleased;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Zoom = 0d;
            Pan = new Point(0, 0);
            AvaloniaXamlLoader.Load(this);
        }

        ~ImageViewer()
        {
            this.PointerWheelChanged -= ImageViewer_PointerWheelChanged;
            this.PointerPressed -= ImageViewer_PointerPressed;
            this.PointerMoved -= ImageViewer_PointerMoved;
            this.PointerReleased -= ImageViewer_PointerReleased;
        }

        public override void Render(DrawingContext context)
        {
            RenderImage(context);
            base.Render(context);
        }

        protected virtual void RenderImage(DrawingContext context)
        {
            if (Source is null)
                return;

            context.DrawImage(Source, GetRenderedImageBounds());
        }

        protected Rect GetRenderedImageBounds()
        {
            Size renderSize = Source.Size * Math.Pow(1.25d, Zoom);
            Point origin = Bounds.Center + Pan;

            return new Rect(new Point(origin.X - renderSize.Width / 2, origin.Y - renderSize.Height / 2), renderSize);
        }

        private void ImageViewer_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            Zoom = Math.Clamp(Zoom + e.Delta.Y, 0, 10);
            e.Handled = true;
        }

        private void ImageViewer_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            m_oldPoint = e.GetPosition(this);
            m_isPanning = true;
        }

        private void ImageViewer_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            if (m_isPanning)
            {
                Pan += e.GetPosition(this) - m_oldPoint;
                m_oldPoint = e.GetPosition(this);
            }
        }
        private void ImageViewer_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            m_isPanning = false;
        }
    }
}
