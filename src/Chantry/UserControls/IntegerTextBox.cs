using Avalonia.Controls;
using Avalonia;
using System;

namespace TeethInc.Chantry.UserControls
{
	public class IntegerTextBox : TextBox
	{
		public static readonly StyledProperty<int> MinimumValueProperty =
			AvaloniaProperty.Register<IntegerTextBox, int>(nameof(MinimumValue));

		public static readonly StyledProperty<int> MaximumValueProperty =
			AvaloniaProperty.Register<IntegerTextBox, int>(nameof(MaximumValue));

		public int MinimumValue
		{
			get => GetValue(MinimumValueProperty);
			set { SetValue(MinimumValueProperty, value); }
		}

		public int MaximumValue
		{
			get => GetValue(MaximumValueProperty);
			set { SetValue(MaximumValueProperty, value); }
		}

		public IntegerTextBox() : base() { }

		protected override Type StyleKeyOverride { get { return typeof(TextBox); } }
	}
}
