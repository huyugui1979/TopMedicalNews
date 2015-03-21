using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace TopMedicalNews.Android
{
	[Activity(Label = "医界头条", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);
			Finish();
		}
	}
}

