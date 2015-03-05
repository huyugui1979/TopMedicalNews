using System;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Services;
using XLabs.Forms.Services;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;

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
			//
			MessagingCenter.Subscribe<object> (this, "TakePicture", sender => {
				//
				var act = new ActionSheetConfig();
				act.Add("拍照",()=>{
					var model = this.BindingContext as MainModel;
					model.SelfTakPictureCommand.Execute(null);
				});
				act.Add("从相册上传",()=>{
					var model = this.BindingContext as MainModel;
					model.SelectPictureCommand.Execute(null);
				});
				act.Add("取消");
				act.Title="选择照片";

				Resolver.Resolve<IUserDialogService>().ActionSheet(act);
				this.IsPresented=false;
				//
			});
			//
			MessagingCenter.Subscribe<object> (this, "IsPresented", sender => {
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

