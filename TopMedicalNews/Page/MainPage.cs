using System;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Mvvm;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{

	public class MainPage:MasterDetailPage
	{
		public MainPage ()
		{
			
			//this.Navigation.p (new SplashScreenPage ());
			this.Master = ViewFactory.CreatePage<MasterModel,Page>() as Page;

			this.Detail = new NavigationPage(ViewFactory.CreatePage<FirstModel,Page>() as Page){BarTextColor=Color.White,BarBackgroundColor=Color.FromRgb(0x36,0x88,0xdb)};
			//

			MessagingCenter.Subscribe<object> (this, "IsPresented", sender => {
				//
				this.IsPresented=false;
				//
			});
			MessagingCenter.Subscribe<object> (this, "LoginIn", sender => {
				//
				this.IsPresented=true;
				//
			});
			//
			Resolver.Resolve<IDependencyContainer>()
				.Register<INavigationService>(t => new NavigationService(this.Detail.Navigation));
			this.IsGestureEnabled = false;
		}

	
	
	}
}

