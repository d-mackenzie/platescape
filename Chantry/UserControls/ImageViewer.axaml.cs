using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TeethInc.Chantry.Extensions;

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

        private Point _oldPanPoint;
        private bool _isPanning;

        private List<Action<DrawingContext>> _layers = new List<Action<DrawingContext>>();

        private int[] _zoomScales = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };

        private BoxShadows _boxShadows = new BoxShadows(
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
            set
            {
                SetValue(ZoomProperty, value);
                SetValue(PanProperty, Pan);
            }
        }

        public Point Pan
        {
            get { return GetValue(PanProperty); }
            set
            {
                double horizontalConstraint = (RenderedImageSize.Width + 25 - Bounds.Width) / 2;
                double verticalConstraint = (RenderedImageSize.Height + 25 - Bounds.Height) / 2;

                Point pan = new Point(
                    Math.Clamp(value.X, Math.Min(-horizontalConstraint, 0), Math.Max(0, horizontalConstraint)),
                    Math.Clamp(value.Y, Math.Min(-verticalConstraint, 0), Math.Max(0, verticalConstraint)));

                SetValue(PanProperty, pan);
            }
        }

        public double ZoomMultiplier
        {
            get { return GetValue(ZoomMultiplierProperty); }
            set
            {
                SetValue(ZoomMultiplierProperty, value);
                SetValue(PanProperty, Pan);
            }
        }

        protected List<Action<DrawingContext>> Layers
        {
            get { return _layers; }
        }

        protected int ZoomScale
        {
            get { return _zoomScales[Zoom]; }
        }

        protected Rect ImageRenderBounds
        {
            get
            {
                if (Source == null)
                    return new Rect(0, 0, 0, 0);

                Point origin = Bounds.Center + Pan;

                return new Rect(new Point(origin.X - RenderedImageSize.Width / 2, origin.Y - RenderedImageSize.Height / 2), RenderedImageSize);
            }
        }

        public Size RenderedImageSize
        {
            get
            {
				if (Source == null)
					return new Size(0, 0);

                return Source.Size * ZoomMultiplier * ZoomScale;
            }
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
            this.Background = Brushes.Transparent;
            this.PointerWheelChanged += ImageViewer_PointerWheelChanged;
            this.PointerPressed += ImageViewer_PointerPressed;
            this.PointerMoved += ImageViewer_PointerMoved;
            this.PointerReleased += ImageViewer_PointerReleased;

            this.Layers.Add(DrawImage);

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
            Layers.ForEach(x => x.Invoke(context));
            base.Render(context);
        }

        private void DrawImage(DrawingContext context)
        {
            if (Source is null)
                return;

            context.DrawRectangle(Brushes.Black, null, ImageRenderBounds.Deflate(1), 0, 0, _boxShadows);
            context.DrawImage(Source, ImageRenderBounds);
        }

        private void ImageViewer_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            Zoom = Math.Clamp(Zoom + (int)e.Delta.Y, 1, 8);
            e.Handled = true;
        }

        private void ImageViewer_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            _oldPanPoint = e.GetPosition(this);
            _isPanning = true;
            e.Handled = true;
        }

        private void ImageViewer_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            if (_isPanning)
            {
                Pan += e.GetPosition(this) - _oldPanPoint;
                _oldPanPoint = e.GetPosition(this);
            }
            e.Handled = true;
        }
        private void ImageViewer_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            _isPanning = false;
            e.Handled = true;
        }
    }
}
