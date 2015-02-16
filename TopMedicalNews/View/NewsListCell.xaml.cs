using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public partial class NewsListCellXaml : ContentView
	{
		public NewsListCellXaml ()
		{
			InitializeComponent ();
		}

	}
	public class NewsListCell : ViewCell
	{
		public NewsListCell ()
		{
			View = new NewsListCellXaml ();
		}
		public static double TotalHeight;
		protected override void OnAppearing ()
		{

			base.OnAppearing ();
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			if (Device.OS == TargetPlatform.iOS) {
				var news = this.BindingContext as News;
				if (news.Type == 2) {
					Height = 65;
					TotalHeight += Height;
				} else {
					Height = 90;
					TotalHeight += Height;
				}
			} else {

				//System.Diagnostics.Debug.WriteLine ("RenderHeight:" + RenderHeight);
			}
			 

		}

	}
}

