using System;
using MyFormsLibCore.Mvvm;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace TopMedicalNews
{
	public class BaseViewModel:ViewModel
	{
		public BaseViewModel ()
		{
		}
		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return false;
			}

			storage = value;
			this.NotifyPropertyChanged(propertyName);
			return true;
		}
		public virtual void OnAppearing()
		{

		}
		public virtual void OnDisappearing()
		{

		}
	}
}

