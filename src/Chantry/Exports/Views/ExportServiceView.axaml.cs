using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.Exports.Views
{
	public partial class ExportServiceView : UserControl
	{
		public ExportServiceView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
