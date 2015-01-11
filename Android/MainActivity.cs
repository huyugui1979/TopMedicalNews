using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace TopMedicalNews.Android
{
	[Activity (Label = "TopMedicalNews.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
        
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels/Resources.DisplayMetrics.Density); // real pixels
			App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels/Resources.DisplayMetrics.Density); // real pixels

			SetPage (App.GetMainPage ());
		}
	}
}

