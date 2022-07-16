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
            AvaloniaProperty.Register<MosaicRenderer, Bitmap>(nameof(Source));

        public static readonly StyledProperty<double> ZoomProperty =
            AvaloniaProperty.Register<MosaicRenderer, double>(nameof(Zoom));

        public static readonly StyledProperty<Point> PanProperty =
            AvaloniaProperty.Register<MosaicRenderer, Point>(nameof(Pan));

        private Point _oldPoint;
        private bool _isPanning;

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
            Zoom = 0d;
            Pan = new Point(0, 0);
            this.PointerWheelChanged += ImageViewer_PointerWheelChanged;
            this.PointerPressed += ImageViewer_PointerPressed;
            this.PointerMoved += ImageViewer_PointerMoved;
            this.PointerReleased += ImageViewer_PointerReleased;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
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
            Size renderSize = Source.Size * Math.Pow(1.25d, Zoom);
            Point origin = Bounds.Center + Pan;

            context.DrawImage(Source, new Rect(new Point(origin.X - renderSize.Width / 2, origin.Y - renderSize.Height / 2), renderSize));
            base.Render(context);
        }
        private void ImageViewer_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            Zoom = Math.Clamp(Zoom + e.Delta.Y, 0, 10);
            e.Handled = true;
        }

        private void ImageViewer_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            _oldPoint = e.GetPosition(this);
            _isPanning = true;
        }

        private void ImageViewer_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            if (_isPanning)
            {
                Pan += e.GetPosition(this) - _oldPoint;
                _oldPoint = e.GetPosition(this);
            }
        }
        private void ImageViewer_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            _isPanning = false;
        }
    }
}
