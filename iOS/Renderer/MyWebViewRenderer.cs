using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using MyFormsLibCore.Ioc;

[assembly:ExportRenderer (typeof(TopMedicalNews.MyWebView), typeof(TopMedicalNews.iOS.MyWebViewRenderer))]
namespace TopMedicalNews.iOS
{
	public class MyWebViewRenderer:WebViewRenderer
	{
		public MyWebViewRenderer ()
		{
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{

			base.OnElementChanged (e);

//			this.Control.AddJavascriptInterface (this, "Foo");
//			this.Control.SetWebViewClient (new MyWebViewClient (this));
//			this.Control.HorizontalScrollBarEnabled = false; 
//			this.Control.VerticalScrollBarEnabled = false;
//			this.Control.Settings.SetLayoutAlgorithm (WebSettings.LayoutAlgorithm.SingleColumn);
			this.ScrollView.ShowsVerticalScrollIndicator=false;
			this.BackgroundColor = UIColor.White;
			this.LoadStarted += (object sender, EventArgs ee) => {
				//this.EvaluateJavascript(@"document.getElementsByTagName('body')[0].style.webkitTextSizeAdjust= '500%'"); 
			};
			this.LoadFinished += (object sender, EventArgs ee) => {
				int font = Resolver.Resolve<IFontService> ().GetSelectFont ();
				switch (font) {
				case 0:
					this.EvaluateJavascript(@"document.getElementsByTagName('body')[0].style.webkitTextSizeAdjust= '80%'"); 
					break;
				case 1:
					this.EvaluateJavascript(@"document.getElementsByTagName('body')[0].style.webkitTextSizeAdjust= '100%'"); 
					break;
				case 2:
					this.EvaluateJavascript(@"document.getElementsByTagName('body')[0].style.webkitTextSizeAdjust= '120%'"); 
					break;
				}

				var curHeight = this.EvaluateJavascript(@"document.body.scrollHeight;");  
				this.Element.HeightRequest = int.Parse(curHeight);
			
			};
		}
	}
}

