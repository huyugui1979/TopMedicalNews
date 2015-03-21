using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class MyPage:ContentPage
	{
		public MyPage ()
		{

		}
	
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
//			var model = this.BindingContext as BaseViewModel;
//			model.OnAppearing ();
		}
	}
}

