using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class ChoseFontPage : ContentPage
	{
		public ChoseFontPage ()
		{
			InitializeComponent ();
			var listView = this.FindByName<ListView> ("listView");
			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				(this.BindingContext as ChoseFontModel).SelectFontId = (e.SelectedItem as FontChose).Font.ID;
			};

		}
	}
}

