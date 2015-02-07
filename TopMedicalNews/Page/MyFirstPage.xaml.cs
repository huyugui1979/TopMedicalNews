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
			var newsList = this.FindByName<ListView> ("newsList");
			newsList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				//
				if(e.SelectedItem == null)return;
				int  newsId = (e.SelectedItem as News).ID;
				(this.BindingContext as FirstModel).GotoNewsDetailCommand.Execute(newsId);
				(sender as ListView).SelectedItem =null;
				//
			};
			//

			//
		}
	
	
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var scrollContent = this.FindByName<StackLayout> ("scrollContent");
			foreach (var column in (this.BindingContext as FirstModel).LikeColumns) {
				Button button = new Button{};
			
				button.BindingContext = column;
				button.BackgroundColor = Color.Transparent;
				button.SetBinding (Button.TextProperty, "Title");
				button.Clicked += (object sender, EventArgs e) => {
					//
					scrollContent.Children.ToList().ForEach(v=>(v as Button).TextColor=Color.Black);
					(sender as Button).TextColor=Color.Red;
					(this.BindingContext as FirstModel).SelectColumnCommand.Execute(button.BindingContext);
					//
				};
				scrollContent.Children.Add (button);
			}
			(scrollContent.Children [0] as Button).TextColor = Color.Red;

		}
	}
}

