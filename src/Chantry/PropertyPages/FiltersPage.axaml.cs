using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.PropertyPages
{
    public partial class FiltersPage : UserControl
    {
        public FiltersPage()
        {
            InitializeComponent();
        }

        private void Border_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            if (!(sender is Border border))
                return;

            if (!(border.ContextMenu is ContextMenu contextMenu))
                return;

            contextMenu.PlacementRect = new Rect(e.GetCurrentPoint(border).Position.X, e.GetCurrentPoint(border).Position.Y, 1, 1);
            contextMenu.Open();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
