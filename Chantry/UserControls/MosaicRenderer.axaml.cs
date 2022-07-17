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
        public MosaicRenderer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void DrawImage(DrawingContext context)
        {
            context.DrawImage(Source, GetRenderedImageBounds());

            if (Zoom >= 8)
            {
                // draw studs.
            }
        }
    }
}
