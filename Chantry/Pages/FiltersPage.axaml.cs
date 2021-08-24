using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.App.Pages
{
    public partial class FiltersPage : UserControl
    {
        public FiltersPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
