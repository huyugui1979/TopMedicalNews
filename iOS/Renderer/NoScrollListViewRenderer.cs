using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using TopMedicalNews.iOS;
using Foundation;
using UIKit;
using System.Drawing;
using TopMedicalNews;


[assembly:ExportRenderer(typeof(NoScrollListView), typeof(NoScrollListViewRenderer))]
namespace TopMedicalNews.iOS
{
	public class NoScrollListViewRenderer:ListViewRenderer
	{
		public NoScrollListViewRenderer ()
		{

		}
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			this.Control.ScrollEnabled = false;
			//
			 // The 50 is just padding

		}

		public override void LayoutSubviews ()
		{
			this.Element.HeightRequest = TotalHeight.Total;
			base.LayoutSubviews ();
		}
	}

}

