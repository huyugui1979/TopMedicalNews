using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;


namespace TopMedicalNews.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate :FormsApplicationDelegate
	{
	
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			App.ScreenWidth = 	UIScreen.MainScreen.Bounds.Width;
			App.ScreenHeight = 	UIScreen.MainScreen.Bounds.Height;
			LoadApplication (new App ()); 

			return base.FinishedLaunching (app, options);
		}
	}
}

