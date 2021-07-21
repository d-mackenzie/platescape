using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.App.Views
{
    public partial class SplashView : Window
    {
        public SplashView()
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
