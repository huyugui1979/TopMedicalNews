using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.Reflection;
using Xamarin.Forms;
using TopMedicalNews;
using TopMedicalNews.Android;


[assembly:ExportRenderer(typeof(NoBarScrollView), typeof(NoBarScrollViewRenderer))]
namespace TopMedicalNews.Android
{
	public class NoBarScrollViewRenderer:ScrollViewRenderer
	{
		public NoBarScrollViewRenderer ()
		{
		}
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
		
			e.NewElement.SizeChanged += (object sender, EventArgs ee) => {
				HorizontalScrollView _scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
					.GetField ("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
					.GetValue (this);
				_scrollView.HorizontalScrollBarEnabled = false;
			};
		
		}
	
	}
}

