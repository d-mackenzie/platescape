using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using TeethInc.Chantry.App.ViewModels;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var args = Environment.GetCommandLineArgs();
                Project project = null;

                if (args.Length == 2)
                {
                    string json = File.ReadAllText(args[1]);

                    project = ProjectService.DeserializeProject(json);
                }

                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = new MainWindowViewModel(project);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
