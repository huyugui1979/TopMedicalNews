using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly:ExportRenderer(typeof(TopMedicalNews.NoScrollListView), typeof(TopMedicalNews.Android.NoScrollListViewRenderer))]
namespace TopMedicalNews.Android
{
	public class NoScrollListViewRenderer:ListViewRenderer
	{
		public NoScrollListViewRenderer ()
		{

		}
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			this.Control.SetScrollContainer (false);

		}
		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
		{

			NoScrollListView.TotalHeight = 0;
			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
			//第一次布局的时候
			if (run) {
				this.Element.HeightRequest = NoScrollListView.TotalHeight;
				run = false;
			}
		}
		bool run =true;
	}
}

