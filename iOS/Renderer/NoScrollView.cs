using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(TopMedicalNews.NoScrollView), typeof(TopMedicalNews.iOS.NoScrollViewRenderer))]

namespace TopMedicalNews.iOS
{
	public class NoScrollViewRenderer:ScrollViewRenderer
	{
		public NoScrollViewRenderer ()
		{
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			this.Bounces=false;
		
			this.AlwaysBounceHorizontal = false;
		
		   //((UIViewController)this.Controller).AutomaticallyAdjustsScrollViewInsets = false;;
			//this. = true;
		}

	}
}

