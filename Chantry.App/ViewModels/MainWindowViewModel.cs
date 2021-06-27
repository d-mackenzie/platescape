using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.App.ViewModels
{
    public class MainWindowViewModel
    {
        public FileSourceViewModel FileSourceViewModel { get; set; }

        public MainWindowViewModel()
        {
            FileSourceViewModel = new FileSourceViewModel(
                new FileSource()
                {
                    Filename = @"C:\Users\david\Pictures\eric-avatar.jpg"
                });
        }
    }
}
