using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;


[assembly:ExportRenderer(typeof(TopMedicalNews.NoScrollView), typeof(TopMedicalNews.Android.NoScrollViewRenderer))]

namespace TopMedicalNews.Android
{
	public class NoScrollViewRenderer:ScrollViewRenderer
	{
		public NoScrollViewRenderer ()
		{
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);


			this.LayoutChange += (object sender, LayoutChangeEventArgs ee) => {
				var hv = this.GetChildAt(0) as HorizontalScrollView;
				//hv.VerticalScrollBarEnabled = false;
				if(hv !=null)
				hv.HorizontalScrollBarEnabled= false;
			};
		

//			var s = this.GetChildAt (0);
//			int j = 2;
		}
	}
}

