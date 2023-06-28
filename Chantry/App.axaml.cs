using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Linq;
using TeethInc.Chantry.Extensions;
using TeethInc.Chantry.Helpers;
using TeethInc.Chantry.ViewModels;
using TeethInc.Chantry.Views;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Services;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.Filters.Views;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry
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
                FilterViewResolver.Register<BrightnessContrastFilter, BrightnessContrastViewModel, BrightnessContrastView>();
                FilterViewResolver.Register<SaturationFilter, SaturationViewModel, SaturationView>();
                FilterViewResolver.Register<MultiplyFilter, MultiplyViewModel, MultiplyView>();

                var args = Environment.GetCommandLineArgs();
                ProjectViewModel? projectViewModel = null;

                string? filename = GetSupportedFilenameArg(args);

                if (filename is not null)
                {
                    projectViewModel = new ProjectViewModel(ProjectService.Load(filename));
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
