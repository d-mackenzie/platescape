using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.App.Extensions;
using TeethInc.Chantry.Core;

namespace TeethInc.Chantry.App.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        private Project m_project;

        public FileSource Source
        {
            get { return (FileSource)m_project.Source; }
        }

        public Bitmap UnfilteredImage => m_project.UnfilteredImage.AsAvaloniaMediaImagingBitmap();

        public Bitmap FilteredImage => m_project.FilteredImage.AsAvaloniaMediaImagingBitmap();

        public string Name => m_project.Name;

        public ProjectViewModel(Project project)
        {
            m_project = project;
        }
    }
}
