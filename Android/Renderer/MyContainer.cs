using System;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

namespace TopMedicalNews.Android
{
	public class MyContainer:global::Android.Widget.LinearLayout
	{
		public MyContainer (Context context):base(context)
		{
			this.Orientation = global::Android.Widget.Orientation.Vertical;
		}
		public ScrollView MyElement{ get; set; }
		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			var c = this.GetChildAt (0) ;
			var s = c as IVisualElementRenderer;
		
			//c.Layout (0, 0, r - l, Math.Max (b - t, (int)base.Context.ToPixels(1000)));
			base.OnLayout (changed, l, t, r, b);
			s.UpdateLayout ();
			//


		}
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			//
			var sz = MyElement.GetSizeRequest (0, 0);
			base.SetMeasuredDimension((int) base.Context.ToPixels((sz.Request.Width)),
				(int) base.Context.ToPixels((sz.Request.Height)));

		}
	}
}

