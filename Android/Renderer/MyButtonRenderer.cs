using System;
using Xamarin.Forms.Platform.Android;

[assembly : Xamarin.Forms.ExportRenderer (typeof(TopMedicalNews.MyButton), typeof(TopMedicalNews.Android.MyButtonRenderer))]

namespace TopMedicalNews.Android
{
	public class MyButtonRenderer:ButtonRenderer
	{
		public MyButtonRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged (e);
			this.Control.SetTextSize (global::Android.Util.ComplexUnitType.Dip, 12);
		}
	}
}

