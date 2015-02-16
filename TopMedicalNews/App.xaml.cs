using System;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Reflection;
using XLabs.Ioc;
using XLabs.Forms.Mvvm;
using Acr.XamForms.UserDialogs;
using XLabs.Platform.Device;


#if __ANDROID__
using Acr.XamForms.UserDialogs.Droid;
#else
using Acr.XamForms.UserDialogs.iOS;
#endif

namespace TopMedicalNews
{

	public partial class App:Application
	{

		public static double ScreenWidth;
		public static double ScreenHeight;
		static void Init()
		{

			var resolverContainer = new SimpleContainer();

			Resolver.SetResolver(resolverContainer.GetResolver());
			resolverContainer.RegisterSingle<ISettings,Settings>();
			#if __IOS__
			resolverContainer.Register<IDevice> (t => AppleDevice.CurrentDevice);
			#else
			resolverContainer.Register<IDevice> (t => AndroidDevice.CurrentDevice);
			#endif
			resolverContainer.Register<IDependencyContainer> (t => resolverContainer);
			resolverContainer.Register<ISQLiteClient> (new SQLiteClient());
			resolverContainer.RegisterSingle<ICategoryService,CategoryService> ();
			resolverContainer.RegisterSingle<IColumnService,ColumnService> ();
			resolverContainer.RegisterSingle<ICollectionService,CollectionService> ();
			resolverContainer.RegisterSingle<INewsService,NewsService> ();
			resolverContainer.RegisterSingle<IFontService,FontService> ();
			resolverContainer.RegisterSingle<IFontService,FontService> ();
			resolverContainer.RegisterSingle<IUserService,UserService> ();
			resolverContainer.RegisterSingle<ICommentService,CommentService> ();
			resolverContainer.RegisterSingle<IUserDialogService,UserDialogService> ();
			//Resolver.SetResolver(resolverContainer.GetResolver());
			ViewFactory.Register<MyFirstPage,FirstModel> ();
			ViewFactory.Register<LoginPage,LoginModel> ();
			ViewFactory.Register<ForgetPage,ForgetModel> ();
			ViewFactory.Register<RegisterPage,RegisterModel> ();
			ViewFactory.Register<NewsDetailPage,NewsDetailModel> ();

			ViewFactory.Register<SetttingPage,SettingModel> ();
			ViewFactory.Register<MyMasterPage,MasterModel> ();
			ViewFactory.Register<FeedBackPage,FeedBackModel> ();
			ViewFactory.Register<SetColumnPage,SelectColumnModel> ();
			ViewFactory.Register<MyAboutUs,AboutUsModel> ();
			ViewFactory.Register<MyCollectionPage,MyCollectionModel> ();
			ViewFactory.Register<MainPage,MainModel> ();


		}
		public App()
		{
			Init ();
			MainPage = ViewFactory.CreatePage<
				MainModel,Page>() as Page;
		}
	
	}
}
