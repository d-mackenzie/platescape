using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using TeethInc.Chantry.App.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.Views
{
    public class SplashView : Window
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
