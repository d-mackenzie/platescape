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

        public static readonly StyledProperty<int> ZoomProperty =
            AvaloniaProperty.Register<ImageViewer, int>(nameof(Zoom));

        public static readonly StyledProperty<Point> PanProperty =
            AvaloniaProperty.Register<ImageViewer, Point>(nameof(Pan));

        public static readonly StyledProperty<double> ZoomMultiplierProperty =
            AvaloniaProperty.Register<ImageViewer, double>(nameof(ZoomMultiplier));

        private Point m_oldPanPoint;
        private bool m_isPanning;

        private int[] m_zoomLevels = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };

        private BoxShadows m_boxShadows = new BoxShadows(
            new BoxShadow()
            {
                Color = Colors.Black,
                Blur = 10
            });

        public Bitmap Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public int Zoom
        {
            get { return GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public Point Pan
        {
            get { return GetValue(PanProperty); }
            set { SetValue(PanProperty, value); }
        }

        public double ZoomMultiplier
        {
            get { return GetValue(ZoomMultiplierProperty); }
            set { SetValue(ZoomMultiplierProperty, value); }
        }

        static ImageViewer()
        {
            AffectsRender<ImageViewer>(SourceProperty);
            AffectsRender<ImageViewer>(ZoomProperty);
            AffectsRender<ImageViewer>(PanProperty);
            AffectsRender<ImageViewer>(ZoomMultiplierProperty);
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
            Zoom = 1;
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
            DrawImage(context);
            base.Render(context);
        }

        protected virtual void DrawImage(DrawingContext context)
        {
            if (Source is null)
                return;

            var renderedImageBounds = GetRenderedImageBounds();

            context.DrawRectangle(Brushes.Black, null, renderedImageBounds, 0, 0, m_boxShadows);
            context.DrawImage(Source, renderedImageBounds);
        }

        protected Rect GetRenderedImageBounds()
        {
            Size renderSize = Source.Size * ZoomMultiplier * m_zoomLevels[Zoom];
            Point origin = Bounds.Center + Pan;

            return new Rect(new Point(origin.X - renderSize.Width / 2, origin.Y - renderSize.Height / 2), renderSize);
        }

        private void ImageViewer_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            Zoom = Math.Clamp(Zoom + (int)e.Delta.Y, 1, 8);
            e.Handled = true;
        }

        private void ImageViewer_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            m_oldPanPoint = e.GetPosition(this);
            m_isPanning = true;
        }

        private void ImageViewer_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            if (m_isPanning)
            {
                Pan += e.GetPosition(this) - m_oldPanPoint;
                m_oldPanPoint = e.GetPosition(this);
            }
        }
        private void ImageViewer_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            m_isPanning = false;
        }
    }
}
