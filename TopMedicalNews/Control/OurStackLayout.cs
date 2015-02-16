using System;
using Xamarin.Forms;

namespace TopMedicalNews
{

	//用来计算表中每一行的高度
	public class OurStackLayout:StackLayout
	{
		public OurStackLayout ()
		{
		}
		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			SizeRequest sr =  base.OnSizeRequest (widthConstraint, heightConstraint);
			NoScrollListView.TotalHeight += sr.Request.Height;
			System.Diagnostics.Debug.WriteLine ("height:" + sr.Request.Height);
			System.Diagnostics.Debug.WriteLine ("total:" + NoScrollListView.TotalHeight);
			return sr;
		}

	}
}