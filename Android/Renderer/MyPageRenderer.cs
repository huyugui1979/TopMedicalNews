using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.Views;


[assembly:ExportRenderer(typeof(TopMedicalNews.MyPage), typeof(TopMedicalNews.Android.MyPageRenderer))]

namespace TopMedicalNews.Android
{
	public class MyPageRenderer:PageRenderer
	{
		public MyPageRenderer ()
		{
			//
		
			//
		} 
		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
		{
			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
			AdjustActionBar ();
		
		}
		public void AdjustActionBar()
		{
//			(Forms.Context as MainActivity).ActionBar.SetBackgroundDrawable (new ColorDrawable (global::Android.Graphics.Color.ParseColor ("#ff2b82d9")));
//			//(Forms.Context as MainActivity).ActionBar.SetDisplayShowTitleEnabled (false);
			if(this.Element.Title == "医界头条")
				(Forms.Context as MainActivity).ActionBar.SetIcon (Resource.Drawable.person_center_btnx);
			else
				(Forms.Context as MainActivity).ActionBar.SetIcon (Resource.Drawable.return_btnx);

			ImageView view = (ImageView)(Forms.Context as MainActivity).FindViewById (global::Android.Resource.Id.Home);
			view.SetPadding(30, 0, 0, 0);
			//(Forms.Context as MainActivity).ActionBar.DisplayOptions = global::Android.App.ActionBarDisplayOptions.ShowTitle;
//			var titleId = Resources.GetIdentifier("action_bar_title", "id", "android");
//
//			var titleTextView = (Forms.Context as MainActivity).FindViewById<TextView>(titleId);
//			var layoutParams = (LinearLayout.LayoutParams) titleTextView.LayoutParameters;
//			layoutParams.Gravity = GravityFlags.CenterHorizontal;
//			layoutParams.Width = Resources.DisplayMetrics.WidthPixels;
//			titleTextView.LayoutParameters = layoutParams;
//			titleTextView.Gravity = GravityFlags.Center;
		}
	
	}
}

