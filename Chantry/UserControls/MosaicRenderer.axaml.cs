using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.App.UserControls
{
    public partial class MosaicRenderer : UserControl
    {
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
            var sw = Stopwatch.StartNew();

            var colors = Mosaic.Colors;

            // var colorDict = new Dictionary<Color, StringBuilder>();

            //for (int x = 0; x < colors.GetUpperBound(0); x++)
            //{
            //    for (int y = 0; y < colors.GetUpperBound(1); y++)
            //    {
            //        Color color = Color.FromRgb(colors[x, y].Color.R, colors[x, y].Color.G, colors[x, y].Color.B);
            //        context.DrawRectangle(
            //            new SolidColorBrush(color),
            //            new Pen(new SolidColorBrush(color)),
            //            new Rect(x * 10, y * 10, 10, 10));
            //    }
            //}

            var sb = new StringBuilder();

            for (int x = 0; x < colors.GetUpperBound(0); x++)
            {
                for (int y = 0; y < colors.GetUpperBound(1); y++)
                {
                    sb.Append($"M {x * 10} {y * 10} l 10,0 0,10 -10,0 Z ");
                }
            }

            GeometryDrawing gd = new GeometryDrawing();
            gd.Geometry = Geometry.Parse(sb.ToString());

            gd.Pen = new Pen(Brushes.Brown);
            gd.Brush = Brushes.Brown;

            gd.Draw(context);

            Debug.WriteLine($"Render(): {sw.ElapsedMilliseconds}ms");

            base.Render(context);
        }
    }
}
