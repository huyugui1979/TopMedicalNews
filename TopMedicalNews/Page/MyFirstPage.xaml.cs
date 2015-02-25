using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Behaviors;
using System.Threading.Tasks;
using XLabs.Forms.Controls;
using TopMedicalNews.Model;
using System.Linq;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;


namespace TopMedicalNews
{
	public partial class MyFirstPage : MyPage
	{
		public MyFirstPage ()
		{

			InitializeComponent ();

//			var newsList = this.FindByName<ListView> ("newsList");
//			newsList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
//				//
//				if(e.SelectedItem == null)return;
//				int  newsId = (e.SelectedItem as News).ID;
//				(this.BindingContext as FirstModel).GotoNewsDetailCommand.Execute(newsId);
//				(sender as ListView).SelectedItem =null;
//				//
//
//				//
//			};
		}
		protected override void OnAppearing ()
		{ 

			base.OnAppearing ();
//			var newsList = this.FindByName<ListView> ("newsList");
////			newsList.HeightRequest = 1000;
//			#if __IOS__
//			newsList.HeightRequest = 1000;//NewsListCell.TotalHeight;
//			#endif
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			//栏目列表
		}
	}
}

