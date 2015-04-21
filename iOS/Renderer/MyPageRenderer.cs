using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using UIKit;
using Refractored.Xam.Settings;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPage), typeof(TopMedicalNews.iOS.MyPageRenderer))]

namespace TopMedicalNews.iOS
{
	public class MyPageRenderer:PageRenderer
	{
		public MyPageRenderer ()
		{
			int k = 2;
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (run == false) {
				
					    var c = new UIBarButtonItem (new UIImage ("return_btn"), UIKit.UIBarButtonItemStyle.Plain, new EventHandler ((s, e) => {
						this.ParentViewController.NavigationController.PopViewController (true);
					}));
					this.ParentViewController.NavigationItem.LeftBarButtonItem = c;

				run = true;
			}
		}

		bool run = false;
	}
}

