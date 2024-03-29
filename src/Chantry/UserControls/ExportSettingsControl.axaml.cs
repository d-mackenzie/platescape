using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.UserControls
{
    public partial class ExportSettingsControl : UserControl
    {
        public ExportSettingsControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
