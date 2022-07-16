using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Extensions;

namespace TeethInc.Chantry.UserControls
{
    public partial class MosaicRenderer : ImageViewer
    {
        private const int ZOOM_FACTOR = 4;

        public static readonly StyledProperty<Mosaic> MosaicProperty =
            AvaloniaProperty.Register<MosaicRenderer, Mosaic>(nameof(Mosaic));

        public Mosaic Mosaic
        {
            get { return GetValue(MosaicProperty); }
            set { SetValue(MosaicProperty, value); }
        }

        static MosaicRenderer()
        {
            AffectsRender<MosaicRenderer>(MosaicProperty);
        }

        public MosaicRenderer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void RenderImage(DrawingContext context)
        {
            context.DrawImage(Mosaic.Image.AsAvaloniaMediaImagingBitmap(), GetRenderedImageBounds());
        }
    }
}
