using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.Exports.Views
{
	public partial class ExportSettings : UserControl
	{
		public ExportSettings()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
