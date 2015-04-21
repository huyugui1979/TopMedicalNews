using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.Reflection;
using UIKit;
using Refractored.Xam.Settings;
using MyFormsLibCore.Ioc;


[assembly : Xamarin.Forms.ExportRenderer (typeof(TopMedicalNews.MyFirstPage), typeof(TopMedicalNews.iOS.MyCarouselPageRenderer))]

namespace TopMedicalNews.iOS
{
	public class MyCarouselPageRenderer:CarouselPageRenderer
	{
		public MyCarouselPageRenderer ()
		{	
			
			loginButton = new UIKit.UIBarButtonItem (new UIImage ("person_center.png"), UIKit.UIBarButtonItemStyle.Plain, null);
			//button.Image = new UIKit.UIImage ("");
			loginButton.Clicked += (object sender, EventArgs e) => {
				if(Resolver.Resolve<IUserService>().GetLoginUser() == null)
					MessagingCenter.Send<object> (this, "ClickLogin");
				else
					MessagingCenter.Send<object> (this, "LoginIn");

			};
			MessagingCenter.Subscribe<object> (this, "LoginSucceed", sender => {
				//
				this.ParentViewController.NavigationItem.LeftBarButtonItem = loginButton;

				//
			});

		}
		private static BindableProperty _rendererProperty;

		public static BindableProperty RendererProperty {
			get {
				Type platformType = Type.GetType ("Xamarin.Forms.Platform.iOS.Platform, Xamarin.Forms.Platform.iOS", true);

				return _rendererProperty ?? (_rendererProperty = (BindableProperty)platformType.GetField ("RendererProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public).GetValue (null)); 
			}
		}
		public UIBarButtonItem loginButton = null;
		ObjCRuntime.Selector action = null;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.ParentViewController.NavigationItem.LeftBarButtonItem= loginButton;

		}
		IVisualElementRenderer _render;
		MyFirstPage _page;
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			_page = e.NewElement as MyFirstPage;
			_render = RendererFactory.GetRenderer (_page.HeadView);
			_page.HeadView.SetValue (RendererProperty, _render);
			_page.HeadView.Layout (new Rectangle (0, 0, App.ScreenWidth, 40));
			this.Add (_render.NativeView);
			MessagingCenter.Subscribe<object> (this, "setcolumn", obj => {
				//

					_render.NativeView.RemoveFromSuperview();
					_render.Dispose ();
					_render = null;
					_render = RendererFactory.GetRenderer (_page.HeadView);
					_page.HeadView.SetValue (RendererProperty, _render);

					_page.HeadView.Layout (new Rectangle (0, 0, App.ScreenWidth, 40));
					this.Add (_render.NativeView);

				//
			});

		}
		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			_render.NativeView.Frame = new CoreGraphics.CGRect (0, 0, (nfloat)App.ScreenWidth, 40);
			var rect = this.NativeView.Subviews [0].Frame;

			var newRect = new CoreGraphics.CGRect (rect.Left, rect.Top+40, rect.Width, rect.Height);
			this.NativeView.Subviews [0].Frame = newRect;
		}
	}
}

