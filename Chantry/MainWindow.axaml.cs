using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TeethInc.Chantry.App.ViewModels;

namespace TeethInc.Chantry.App
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
