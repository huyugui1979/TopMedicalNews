using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class MyNewsListPage : ContentPage
	{
		public MyNewsListPage ()
		{
			InitializeComponent ();
			pull.WidthRequest = App.ScreenWidth;

		
		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

		}
	}
}

