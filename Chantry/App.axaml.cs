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
using TeethInc.Chantry.Core.Filters;
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
                ProjectViewModel? projectViewModel = null;

                string? filename = GetSupportedFilenameArg(args);

                if (filename is not null)
                {
                    switch (Path.GetExtension(filename))
                    {
                        case "json":

                            string json = File.ReadAllText(filename);
                            projectViewModel = new ProjectViewModel(ProjectService.DeserializeProject(json));
                            break;

                        default:

                            projectViewModel = new ProjectViewModel(Project.CreateSimpleProject(filename));
                            break;
                    }
                }

                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowViewModel(projectViewModel, new SplashViewModel())
                };

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
