using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using TeethInc.Chantry.App.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Threading;

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

        public void Initialize()
        {
            if (DataContext is SplashViewModel viewModel)
            {
                viewModel.FileSelected += SplashViewModel_FileSelected;
                viewModel.CloseApplication += ViewModel_CloseApplication;
            }

        }

        private void ViewModel_CloseApplication(object? sender, System.EventArgs e)
        {
            this.Close();
        }

        private void SplashViewModel_FileSelected(object? sender, string e)
        {
            string? currentExe = System.Diagnostics.Process.GetCurrentProcess()?.MainModule?.FileName;

            if (currentExe is not null)
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(currentExe, $"\"{e}\""));
            }
        }
    }
}
