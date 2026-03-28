using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeethInc.Chantry.Views.Panels
{
	public partial class ExportView : UserControl
	{
		public ExportView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
