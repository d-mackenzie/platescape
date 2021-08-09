using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ProjectViewModel ProjectViewModel { get; set; }

        public MainWindowViewModel(Project project)
        {
            ProjectViewModel = new ProjectViewModel(project);
        }
    }
}
