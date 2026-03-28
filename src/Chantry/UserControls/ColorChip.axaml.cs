using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SkiaSharp;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.UserControls
{
	public partial class ColorChip : UserControl
	{
		private static readonly StyledProperty<bool> IsSelectedProperty =
			AvaloniaProperty.Register<ColorChip, bool>(nameof(IsSelected));

		private static readonly StyledProperty<LdColor> LdColorProperty =
			AvaloniaProperty.Register<ColorChip, LdColor>(nameof(LdColor));

		public bool IsSelected
		{
			get => GetValue(IsSelectedProperty);
			set => SetValue(IsSelectedProperty, value);
		}

		public LdColor LdColor
		{
			get => GetValue(LdColorProperty);
			set => SetValue(LdColorProperty, value);
		}

		public ColorChip()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
