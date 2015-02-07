using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class TotalHeight
	{
		public static double Total;
	}
	public partial class MyStackLayout : ViewCell
	{
		public MyStackLayout ()
		{
			InitializeComponent ();


		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			TotalHeight.Total = 0;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			if (Device.OS == TargetPlatform.iOS) {
				var text = (string)(BindingContext as CommentData).Content;

				var len = text.Length;
				//
				if (len <=15 ){
					// fits in one cell
					 Height = 50;
					TotalHeight.Total += Height;

				} else {
					len = len - 15;
					var extraRows = len / 15;
					Height = 50 + extraRows * 30;
					TotalHeight.Total += Height;
				}
			}


		}
	}
}

