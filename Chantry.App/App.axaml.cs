using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Linq;
using TeethInc.Chantry.App.Helpers;
using TeethInc.Chantry.App.ViewModels;
using TeethInc.Chantry.App.Views;
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
                Project? project = null;

                if (args.Length != 0)
                {
                    string? projectFilename = args.FirstOrDefault(x => x.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase));

                    if (projectFilename is not null)
                    {
                        string json = File.ReadAllText(projectFilename);
                        project = ProjectService.DeserializeProject(json);
                        desktop.MainWindow = new MainWindow()
                        {
                            DataContext = new MainWindowViewModel(project)
                        };
                    }
                }

                if (project is null)
                {
                    desktop.MainWindow = new SplashView();
                    desktop.MainWindow.DataContext = new SplashViewModel(new FileDialog(desktop.MainWindow));
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
