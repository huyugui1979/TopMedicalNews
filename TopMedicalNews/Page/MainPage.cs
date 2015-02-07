using System;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Services;
using XLabs.Forms.Services;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{

	public class MainPage:MasterDetailPage
	{
		public MainPage ()
		{
			//this.Navigation.p (new SplashScreenPage ());
			this.Master = ViewFactory.CreatePage<MasterModel,Page> () as Page;
			this.Detail = new NavigationPage(ViewFactory.CreatePage<FirstModel,Page> () as Page){BarTextColor=Color.White,BarBackgroundColor=Color.FromRgb(0x36,0x88,0xdb)};
			//
			MessagingCenter.Subscribe<object> (this, "LogoutSucceed", sender => {
				//
				this.IsPresented=false;
				//
			});
			//
			Resolver.Resolve<IDependencyContainer>()
				.Register<INavigationService>(t => new NavigationService(this.Detail.Navigation));
			this.IsGestureEnabled = false;
		}

	
	
	}
}

