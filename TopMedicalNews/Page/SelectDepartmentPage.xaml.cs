using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class SelectDepartmentPage : ContentPage
	{
		public SelectDepartmentPage ()
		{
			InitializeComponent ();
			listView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				(this.BindingContext as SelectDepartmentModel).SelectDepartmentCommand.Execute(e.Item);
			};
		}
	}
}

