using System;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Reflection;
using TinyIoC;


namespace TopMedicalNews
{

	public class App
	{
     
		public static int ScreenWidth;
		public static int ScreenHeight;
		static void Init()
		{
			//container.Register<ISQLiteClient,SQLiteClient> ();
			TinyIoCContainer.Current.Register<ISQLiteClient,SQLiteClient> ();
			TinyIoCContainer.Current.Register<ICategoryService,CategoryService> ();
			TinyIoCContainer.Current.Register<IColumnService,ColumnService> ();
			TinyIoCContainer.Current.Register<INewsService,NewsService> ();
			TinyIoCContainer.Current.Register<ISettings,Settings> ();
			TinyIoCContainer.Current.Register<DetailMode> ();
            
        }
		public static TinyIoCContainer Container {
			get {
				return TinyIoCContainer.Current;
			}
		}
		public static Page GetMainPage ()
		{
            Init();
			return new MainPage ();
		}
	}
}

