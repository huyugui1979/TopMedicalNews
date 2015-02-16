using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms;
using CoreGraphics;

namespace TopMedicalNews.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
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

