using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Linq;
using TeethInc.Chantry.App.Extensions;
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

                string? filename = GetSupportedFilenameArg(args);

                if (filename is null)
                {
                    SplashView splashView = new SplashView();
                    splashView.DataContext = new SplashViewModel(new FileDialog(splashView));
                    splashView.Initialize();
                    
                    desktop.MainWindow = splashView;
                }
                else
                {
                    switch (Path.GetExtension(filename))
                    {
                        case "json":

                            string json = File.ReadAllText(filename);
                            project = ProjectService.DeserializeProject(json);
                            break;

                        default:

                            project = new Project()
                            {
                                Source = new FileSource()
                                {
                                    Filename = filename
                                }
                            };

                            break;
                    }

                    desktop.MainWindow = new MainWindow()
                    {
                        DataContext = new MainWindowViewModel(project)
                    };
                }
            }

            base.OnFrameworkInitializationCompleted();
        }

        private string? GetSupportedFilenameArg(string[]? args)
        {
            if (args is null)
                return null;

            string[] supportedFileTypes = new string[] { ".json", ".jpeg", ".jpg", ".png" };

            return args.FirstOrDefault(x => x.EndsWithAny(supportedFileTypes));
        }
    }
}
