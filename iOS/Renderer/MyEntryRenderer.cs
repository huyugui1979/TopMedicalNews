using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using CoreGraphics;
using Foundation;


[assembly:ExportRenderer(typeof(TopMedicalNews.MyEntry), typeof(TopMedicalNews.iOS.MyEntryRenderer))]

namespace TopMedicalNews.iOS
{
	public class MyEntryRenderer:EntryRenderer
	{
		public MyEntryRenderer ()
		{
		}
		protected async override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged (e);
			//

			if (e.NewElement != null) {
				this.Control.BorderStyle = UIKit.UITextBorderStyle.None;

				this.Control.Layer.CornerRadius = 15.0f;
				this.Control.Layer.MasksToBounds = true;
				var entry = (e.NewElement as MyEntry);
				var handle = new FileImageSourceHandler ();
				var image = await handle.LoadImageAsync (entry.Source);
				// for left padding
				this.Control.LeftView = new UIView (new CGRect (0, 0, image.Size.Width / 2, image.Size.Height / 2));
				this.Control.LeftViewMode = UITextFieldViewMode.Always;
				this.Control.Background = image.StretchableImage ((nint)image.Size.Width / 2, (nint)image.Size.Height / 2);
			}
			//this.Control.LeftViewMode = UITextFieldViewMode.Always;

		}
	}
}

