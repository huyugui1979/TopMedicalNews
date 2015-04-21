using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using Android.Runtime;
using Java.Interop;
using Android.Content;
using System.Threading;
using MyFormsLibCore.Ioc;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyWebView), typeof(TopMedicalNews.Android.MyWebViewRenderer))]
namespace TopMedicalNews.Android
{
	public class MyWebViewClient:WebViewClient
	{
		public override void OnPageStarted (global::Android.Webkit.WebView view, string url, global::Android.Graphics.Bitmap favicon)
		{

			base.OnPageStarted (view, url, favicon);
			//
			int font = Resolver.Resolve<IFontService> ().GetSelectFont ();
			switch (font) {
			case 0:
				_view.Control.Settings.DefaultFontSize = 13;
				break;
			case 1:
				_view.Control.Settings.DefaultFontSize = 20;
				break;
			case 2:
				_view.Control.Settings.DefaultFontSize = 35;
				break;
			}
			//
			SemaphoreSlim semaPhoneSlim = new SemaphoreSlim (0, 1);
			MessagingCenter.Subscribe<object> (this, "Share", async (obj) => {
				if (_view.Control != null) {
					global::Android.Graphics.Picture pic=null;
					Device.BeginInvokeOnMainThread (() => {
						pic = _view.Control.CapturePicture ();
						semaPhoneSlim.Release ();
					}
					);
					await semaPhoneSlim.WaitAsync();
				
					var bitmap = global::Android.Graphics.Bitmap.CreateBitmap (pic.Width, pic.Height, global::Android.Graphics.Bitmap.Config.Argb8888);
					var c = new global::Android.Graphics.Canvas (bitmap);
					pic.Draw (c);
					var path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
					System.IO.FileStream fos = null;
					try {
						fos = new System.IO.FileStream (path + "/share.png", System.IO.FileMode.Create);
						if (fos != null) {
							bitmap.Compress (global::Android.Graphics.Bitmap.CompressFormat.Png, 100, fos);
							fos.Close ();
						}
					
					} catch (Exception e) {
						//

						//
					}
					finally{
						pic.Dispose();
						bitmap.Dispose();
					}
				}
			});

		}

		public override void OnPageFinished (global::Android.Webkit.WebView view, string url)
		{
			base.OnPageFinished (view, url);
			if (_view.Control != null) {
				_view.Control.LoadUrl ("javascript:Foo.setHeight(document.body.offsetHeight)");

			}
		}

		public MyWebViewClient (MyWebViewRenderer view)
		{
			_view = view;

		}

		MyWebViewRenderer _view;
	}

	public class MyWebViewRenderer:WebViewRenderer
	{
		public MyWebViewRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.WebView> e)
		{
		
			base.OnElementChanged (e);
			this.Control.Settings.JavaScriptEnabled = true;//(true);  
			this.Control.AddJavascriptInterface (this, "Foo");
			this.Control.SetWebViewClient (new MyWebViewClient (this));
			this.Control.HorizontalScrollBarEnabled = false; 
			this.Control.VerticalScrollBarEnabled = false;
			this.Control.Settings.SetLayoutAlgorithm (WebSettings.LayoutAlgorithm.SingleColumn);

			this.Control.SetBackgroundColor (global::Android.Graphics.Color.White);
		}

		[Export ("setHeight")]
		[JavascriptInterface]
		// to become consistent with Java/JS interop convention, the argument cannot be System.String.
		public void setHeight (double height)
		{
			Device.BeginInvokeOnMainThread (() => {

				this.Element.HeightRequest = height + 20;
			});
		}
	}
}

