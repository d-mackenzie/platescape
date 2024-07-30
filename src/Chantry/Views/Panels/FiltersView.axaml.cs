using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Views.Panels
{
	public partial class FiltersView : UserControl
	{
		public FiltersView()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}
	}
}
