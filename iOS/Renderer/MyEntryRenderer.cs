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

		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);
			this.Control.BorderStyle = UIKit.UITextBorderStyle.None;
		}
	}
}

