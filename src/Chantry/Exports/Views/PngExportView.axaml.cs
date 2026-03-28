using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.Exports.Views
{
	public partial class PngExportView : UserControl
	{
		public PngExportView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
