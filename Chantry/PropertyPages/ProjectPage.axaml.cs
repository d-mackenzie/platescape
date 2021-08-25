using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.App.PropertyPages
{
    public partial class ProjectPage : UserControl
    {
        public ProjectPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
