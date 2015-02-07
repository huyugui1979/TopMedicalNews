using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;
using UIKit;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPage), typeof(TopMedicalNews.iOS.MyPageRenderer))]

namespace TopMedicalNews.iOS
{
	public class MyPageRenderer:PageRenderer
	{
		public MyPageRenderer ()
		{
			logoutButton = new UIKit.UIBarButtonItem (new UIImage ("个人中心_btn"), UIKit.UIBarButtonItemStyle.Plain, null);
			//button.Image = new UIKit.UIImage ("");
			logoutButton.Clicked += (object sender, EventArgs e) => {
				MessagingCenter.Send<object> (this, "ClickLogin");
			};
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			if ((Element as Page).Title == "医界头条") {
				MessagingCenter.Subscribe<object> (this, "LoginSucceed", sender => {
					//
					this.ParentViewController.NavigationItem.LeftBarButtonItem = loginButton;

					//
				});
				MessagingCenter.Subscribe<object> (this, "LogoutSucceed", sender => {
					//
					this.ParentViewController.NavigationItem.LeftBarButtonItem = logoutButton;
					//
				});
			}
		}

		public UIBarButtonItem loginButton = null;
		public UIBarButtonItem logoutButton = null;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (run == false) {
				if ((Element as Page).Title == "医界头条") {
					loginButton = this.ParentViewController.NavigationItem.LeftBarButtonItem;
					loginButton.Image = new UIImage ("个人中心_btn");
					loginButton.Title = "";
					if (Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1) == -1) {
						//
						this.ParentViewController.NavigationItem.LeftBarButtonItem = logoutButton;		
					} 
						
					//this.ParentViewController.NavigationItem.LeftBarButtonItem.Image = new UIImage ("个人中心_btn").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
				} else {
					    var c = new UIBarButtonItem (new UIImage ("返回_btn"), UIKit.UIBarButtonItemStyle.Plain, new EventHandler ((s, e) => {
						this.ParentViewController.NavigationController.PopViewController (true);
					}));
					this.ParentViewController.NavigationItem.LeftBarButtonItem = c;
				}
				//

				//
//				this.ParentViewController.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0x36,0x88,0xdb);
//				this.ParentViewController.NavigationController.NavigationBar.TintColor = UIColor.White;
////				this.ParentViewController.NavigationController.NavigationBar.Items [0].TitleView = new UILabel{ Text=(Element as Page).Title,TextColor=UIColor.White};
//				//this.ParentViewController.NavigationController.NavigationItem.TitleView.TintColor = UIColor.White;
				run = true;
			}
		}

		bool run = false;
	}
}

