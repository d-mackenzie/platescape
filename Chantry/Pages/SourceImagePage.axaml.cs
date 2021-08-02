using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.App.Pages
{
    public partial class SourceImagePage : UserControl
    {
        public SourceImagePage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
