using Avalonia.Media;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.ViewModels
{
	public class ColorChipViewModel : BaseViewModel
	{
		private bool _isSelected;

		public LdColor LdColor { get; set; }

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				RaisePropertyChanged();
			}
		}

		public ColorChipViewModel(LdColor ldColor, bool isSelected)
		{
			LdColor = ldColor;
			_isSelected = isSelected;
		}
	}
}
