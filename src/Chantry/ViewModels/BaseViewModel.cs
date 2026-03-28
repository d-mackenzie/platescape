using ExCSS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.ViewModels
{
	public abstract class BaseViewModel : IViewModel
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private bool _isDirty = false;
		
		private List<string> _dependentViewModelProperties = new List<string>();
		private List<string> _dependentViewModelCollections = new List<string>();

		public bool IsDirty
		{
			get => _isDirty;
			set
			{
				if (_isDirty != value)
				{
					_isDirty = value;
					RaisePropertyChanged();
				}
			}
		}

		protected BaseViewModel()
		{
			// find dependent viewmodels.
			
			var properties = GetType().GetProperties();

			foreach (var property in properties)
			{
				// get declaring type.
				
				var getMethodInfo = property.GetGetMethod();
				
				if (getMethodInfo?.DeclaringType is null)
					continue;
				
				if (IsIViewModel(getMethodInfo.DeclaringType))
				{
					_dependentViewModelProperties.Add(property.Name);
				}
				
				if (getMethodInfo.DeclaringType == typeof(ObservableCollection<>))
				{
					// if generic type implements IViewModel.
					
					if (IsIViewModel(getMethodInfo.DeclaringType.GetGenericArguments().First()))
					{
						_dependentViewModelCollections.Add(property.Name);
					}
				}				
			}
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				IsDirty = true;
			}
		}
		
		private bool IsIViewModel(Type? type)
		{
			if (type is null)
				return false;
		
			return type.GetInterfaces().Any(x => x == typeof(IViewModel));
		}
	}
}
