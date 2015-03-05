using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms;
using System.Reflection;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;
using Xamarin.Forms;
using Android.Graphics.Drawables;

namespace TopMedicalNews.Android
{
	[Activity (Label = "",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid
	{
        
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			App.ScreenWidth = this.FromPixels(Resources.DisplayMetrics.WidthPixels); // real pixels
			App.ScreenHeight =this.FromPixels(Resources.DisplayMetrics.HeightPixels); // real pixels

//			var assembly = typeof(MainActivity).GetTypeInfo().Assembly;
//			foreach (var res in assembly.GetManifestResourceNames()) 
//				System.Diagnostics.Debug.WriteLine("found resource: " + res);

			LoadApplication (new App ());
			//showShare ();
		}

//		public override bool OnCreateOptionsMenu (IMenu menu)
//		{
//			//ActionBar.SetHomeButtonEnabled (true);
//			//ActionBar.set (Resource.Drawable.person_center_btnx);
//			ActionBar.SetBackgroundDrawable (new ColorDrawable (global::Android.Graphics.Color.ParseColor ("#ff2b82d9")));
//			ActionBar.SetDisplayHomeAsUpEnabled (false);
//			//
//			ActionBar.SetDisplayShowTitleEnabled (false);
////			ActionBar.SetDisplayShowCustomEnabled (true);//ActionBar.DISPLAY_SHOW_CUSTOM);  
////			ActionBar.SetCustomView (Resource.Layout.Main);
//			return base.OnCreateOptionsMenu (menu);
//		}
	
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.TitleFormatted != null && item.TitleFormatted.ToString() == "医界头条") {
				if (Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1) == -1) {
					MessagingCenter.Send<object>(this, "ClickLogin");
					return true;
				}
			}
			return base.OnOptionsItemSelected (item);
		}
	}
}

