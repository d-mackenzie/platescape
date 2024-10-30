using ExCSS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private bool _isDirty = false;

		public bool IsDirty
		{
			get { return _isDirty; }
		}

		public void ClearDirty()
		{
			_isDirty = false;

			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsDirty)));
			}
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			_isDirty = true;

			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				if (propertyName != nameof(IsDirty))
				{
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsDirty)));
				}
			}
		}
	}
}
