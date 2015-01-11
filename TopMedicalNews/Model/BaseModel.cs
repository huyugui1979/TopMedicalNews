using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Security.Cryptography;
using System.Text;

namespace TopMedicalNews
{
	public abstract class BaseModel : INotifyPropertyChanged
	{
		public INavigation Navigation { get; set; }

		internal virtual Task Initialize (params object[] args)
		{
			return Task.FromResult (0);
		}

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged == null) return;
			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		protected void SetObservableProperty<T>(
			ref T field, 
			T value,
			[CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return;
			field = value;
			OnPropertyChanged (propertyName);
		}

		public static void CreateAndBind<T> (Page page, params object[] args) where T : BaseModel {
			page.BindingContext = App.Container.Resolve<T> ();
			page.Appearing += (object sender, EventArgs e) => {
				((BaseModel)((Page)sender).BindingContext).Initialize (args);
			};
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}