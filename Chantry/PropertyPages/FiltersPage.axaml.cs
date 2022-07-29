using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
            if (sender is null || !(sender is Border))
                return;

            ContextMenu? contextMenu = ((sender as Border)?.ContextMenu) ?? null;

            if (contextMenu is null)
                return;

            contextMenu.PlacementRect = new Rect(
                e.GetCurrentPoint((Avalonia.VisualTree.IVisual?)sender).Position.X,
                e.GetCurrentPoint((Avalonia.VisualTree.IVisual?)sender).Position.Y,
                1,
                1);

            contextMenu.Open();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
