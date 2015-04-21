using System;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Reflection;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Mvvm;
using MyFormsLibCore.Ioc;
using MyFormsLibCore;
using Acr.XamForms.Mobile;




#if __ANDROID__
using Acr.XamForms.UserDialogs.Droid;
using Acr.XamForms.Mobile.Droid;
#else
using Acr.XamForms.UserDialogs.iOS;
using Acr.XamForms.Mobile.iOS;
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
		

			resolverContainer.Register<IDependencyContainer> (t => resolverContainer);
			resolverContainer.Register<ISQLiteClient> (new SQLiteClient());
			resolverContainer.Register<ICategoryService,CategoryService> ();
			resolverContainer.Register<IColumnService,ColumnService> ();
			resolverContainer.Register<ILikeColumnService,LikeColumnService> ();
			resolverContainer.Register<ICollectionService,CollectionService> ();
			resolverContainer.Register<INewsService,NewsService> ();
			resolverContainer.Register<ICacheService,CacheService> ();
			resolverContainer.Register<IFontService,FontService> ();
			resolverContainer.Register<IUserService,UserService> ();
			resolverContainer.RegisterSingle<IPhoneService,PhoneService> ();
			resolverContainer.Register<IDepartmentService,DepartmentService> ();
			resolverContainer.Register<IReadingService,ReadingService> ();
			resolverContainer.Register<ICommentService,CommentService> ();
			resolverContainer.Register<IUserDialogService,UserDialogService> ();
			resolverContainer.Register<IJsonService,JsonService> ();
			resolverContainer.Register<ISoftService,SoftService> ();

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
				WelcomeModel,Page>() as Page;
		}
	
	}
}
