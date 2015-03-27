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
			//resolverContainer.RegisterSingle<ISettings,Settings>();
			#if __IOS__
			resolverContainer.Register<IDevice> (t => AppleDevice.CurrentDevice);
			#else
			resolverContainer.Register<IDevice> (t => AndroidDevice.CurrentDevice);
			#endif
			resolverContainer.Register<IDependencyContainer> (t => resolverContainer);
			resolverContainer.Register<ISQLiteClient> (new SQLiteClient());
			resolverContainer.RegisterSingle<ICategoryService,CategoryService> ();
			resolverContainer.RegisterSingle<IColumnService,ColumnService> ();
			resolverContainer.RegisterSingle<ILikeColumnService,LikeColumnService> ();
			resolverContainer.RegisterSingle<ICollectionService,CollectionService> ();
			resolverContainer.RegisterSingle<INewsService,NewsService> ();
			resolverContainer.RegisterSingle<ICacheService,CacheService> ();
			resolverContainer.RegisterSingle<IFontService,FontService> ();
			resolverContainer.RegisterSingle<IUserService,UserService> ();
			resolverContainer.RegisterSingle<IDepartmentService,DepartmentService> ();
			resolverContainer.RegisterSingle<IReadingService,ReadingService> ();
			resolverContainer.RegisterSingle<ICommentService,CommentService> ();
			resolverContainer.RegisterSingle<IUserDialogService,UserDialogService> ();
			resolverContainer.RegisterSingle<IJsonService,JsonService> ();
			resolverContainer.RegisterSingle<ISoftService,SoftService> ();

			//Resolver.SetResolver(resolverContainer.GetResolver());
			ViewFactory.Register<MyFirstPage,FirstModel> ();
			ViewFactory.Register<LoginPage,LoginModel> ();
			ViewFactory.Register<NewsThemePage,NewsThemeModel> ();
			ViewFactory.Register<ForgetPage,ForgetModel> ();
			ViewFactory.Register<MyNewsListPage,MyNewsListModel> ();
			ViewFactory.Register<ModifyPasswordPage,ModifyPasswordModel> ();
			ViewFactory.Register<RegisterPage,RegisterModel> ();
			ViewFactory.Register<NewsDetailPage,NewsDetailModel> ();
			ViewFactory.Register<RegisterRulePage,RegisterRuleModel> ();
			ViewFactory.Register<SetttingPage,SettingModel> ();
			ViewFactory.Register<MyMasterPage,MasterModel> ();
			ViewFactory.Register<FeedBackPage,FeedBackModel> ();
			ViewFactory.Register<SetColumnPage,SelectColumnModel> ();
			ViewFactory.Register<MyAboutUs,AboutUsModel> ();
			ViewFactory.Register<CollectionPage,MyCollectionModel> ();
			ViewFactory.Register<MyCommentPage,MyCommentModel> ();
			ViewFactory.Register<MyReadingPage,MyReadingModel> ();
			ViewFactory.Register<MainPage,MainModel> ();
			ViewFactory.Register<SelectDepartmentPage,SelectDepartmentModel> ();
			ViewFactory.Register<WelcomePage,WelcomeModel> ();
			//



//			if (CrossSettings.Current.GetValueOrDefault<bool>("MyInit",false) == false)
//			{
//				//
//				Resolver.Resolve<ISQLiteClient> ().Init ();
//				Resolver.Resolve<IFontService> ().Init ();
//				//
//				 Resolver.Resolve<IColumnService> ().Init ();
//				 Resolver.Resolve<IDepartmentService> ().Init ();
//				CrossSettings.Current.AddOrUpdateValue<bool>("MyInit", true);
//
//			}

			//
			//Resolver.Resolve<IDepartmentService> ().Init ();
			//
		}
		public App()
		{
			Init ();
			MainPage = ViewFactory.CreatePage<
				WelcomeModel,Page>(  (m,p)=> m.Init()) as Page;
		}
	
	}
}
