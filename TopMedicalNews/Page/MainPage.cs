using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class MainPage:MasterDetailPage
	{
		public MainPage ()
		{
			this.Master = new MasterPage ();
			this.Detail = new NavigationPage(new DetailPage());
		}
	}
}

