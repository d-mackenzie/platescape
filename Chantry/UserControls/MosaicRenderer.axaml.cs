using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Diagnostics;
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

            for (int x = 0; x < colors.GetUpperBound(0); x++)
            {
                for (int y = 0; y < colors.GetUpperBound(1); y++)
                {
                    Color color = Color.FromRgb(colors[x, y].Color.R, colors[x, y].Color.G, colors[x, y].Color.B);
                    context.DrawRectangle(
                        new SolidColorBrush(color),
                        new Pen(new SolidColorBrush(color)),
                        new Rect(x * 10, y * 10, 10, 10));
                }
            }

            Debug.WriteLine($"Render(): {sw.ElapsedMilliseconds}ms");

            base.Render(context);
        }
    }
}
