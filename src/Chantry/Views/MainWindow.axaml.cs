using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
