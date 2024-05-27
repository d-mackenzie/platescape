using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.Exports.Views
{
	public partial class ExporterView : UserControl
	{
		public ExporterView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
