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
		public static readonly StyledProperty<bool> IsAllowedProperty =
			AvaloniaProperty.Register<ColorChip, bool>(nameof(IsAllowed));

		public static readonly StyledProperty<LdColor> LdColorProperty =
			AvaloniaProperty.Register<ColorChip, LdColor>(nameof(LdColor));

		public bool IsAllowed
		{
			get => GetValue(IsAllowedProperty);
			set { SetValue(IsAllowedProperty, value); }
		}

		public LdColor LdColor
		{
			get => GetValue(LdColorProperty);
			set { SetValue(LdColorProperty, value); }
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
