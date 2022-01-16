using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Extensions;

namespace TeethInc.Chantry.UserControls
{
    public partial class MosaicRenderer : UserControl
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

        public override void Render(DrawingContext context)
        {
            context.DrawImage(Mosaic.Image.AsAvaloniaMediaImagingBitmap(), new Rect(0, 0, Mosaic.StudExtent.Width * ZOOM_FACTOR, Mosaic.StudExtent.Height * ZOOM_FACTOR));

            //var colors = Mosaic.Colors;
            //var colorDict = new Dictionary<SdColor, StringBuilder>();
            //string square = $" l {RECTANGLE_SIZE},0 0,{RECTANGLE_SIZE} {-RECTANGLE_SIZE},0 Z ";

            //for (int x = 0; x < colors.GetUpperBound(0); x++)
            //{
            //    for (int y = 0; y < colors.GetUpperBound(1); y++)
            //    {
            //        if (!colorDict.ContainsKey(colors[x, y].Color))
            //        {
            //            colorDict[colors[x, y].Color] = new StringBuilder();
            //        }

            //        colorDict[colors[x, y].Color].Append($"M {x * RECTANGLE_SIZE} {y * RECTANGLE_SIZE}").Append(square);
            //    }
            //}


            //foreach (SdColor key in colorDict.Keys)
            //{
            //    AmColor color = AmColor.FromRgb(key.R, key.G, key.B);

            //    GeometryDrawing gd = new GeometryDrawing();
            //    gd.Geometry = Geometry.Parse(colorDict[key].ToString());

            //    gd.Brush = new SolidColorBrush(color);

            //    gd.Draw(context);
            //}

            base.Render(context);
        }
    }
}
