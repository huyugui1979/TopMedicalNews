using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;


[assembly:ExportRenderer(typeof(TopMedicalNews.NoScrollListView), typeof(TopMedicalNews.Android.NoScrollListViewRenderer))]
namespace TopMedicalNews.Android
{
	public class NoScrollListViewRenderer:ListViewRenderer
	{
		public NoScrollListViewRenderer ()
		{

		}
		public double GetListViewHeightBasedOnChildren() {  
			// get the list view adapter, so this function must be invoked after set the adapter.  
			var listAdapter = this.Control.Adapter;  
			if (listAdapter == null) {  
				return 0;  
			}  
			double totalHeight = 0;
			int count = listAdapter.Count;
			for (int i = 0; i < count; i++) {  

				var listItem = listAdapter.GetView(i, null, this.Control) as ViewGroup;  

				var c1 = listItem.GetChildAt (0);

				c1.Measure(0,0); 
			
				var sz = ((c1 as ViewGroup).GetChildAt (0) as IVisualElementRenderer).Element.GetSizeRequest (double.PositiveInfinity, double.PositiveInfinity);
				totalHeight += sz.Request.Height;
			}  
			 return totalHeight;


		}  
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			this.Control.SetScrollContainer (false);
			//
			this.Element.HeightRequest = GetListViewHeightBasedOnChildren ();
			//

		}
	
//		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
//		{
//			//int mExpandSpec = MeasureSpec.MakeMeasureSpec(int.MaxValue>>2,global::Android.Views.MeasureSpecMode.AtMost);
//
//			base.OnMeasure (1080, 10000);
//		}

//		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
//		{
//			NoScrollListView.TotalHeight = 0;;
//			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
//			if (run == true) {
//				this.Element.HeightRequest = NoScrollListView.TotalHeight;
//				run = false;
//			}
//		}
		bool run =true;
	}

}

