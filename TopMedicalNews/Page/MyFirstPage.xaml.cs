 using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using MyFormsLibCore.Mvvm;


namespace TopMedicalNews
{
	public partial class MyFirstPage : CarouselPage
	{
		public MyFirstPage ()
		{
			InitializeComponent ();
			HeadView = new MyColumnView ();
			var tool = new ToolbarItem{ Icon = "setting_btn" };
			tool.SetBinding (ToolbarItem.CommandProperty, "SettingCommand");
			this.ToolbarItems.Add (tool);
			//
			this.CurrentPageChanged += (object sender, EventArgs e) => {
				var model = this.BindingContext as FirstModel;
				int index = this.Children.IndexOf(this.CurrentPage);
				model.SelectColumnCommand.Execute(index);
				MessagingCenter.Send<Column> (model.LikeColumns[index], "ColumnSelect");
				HeadView.ScrollToPos(index);
			};

		}

		public MyColumnView HeadView{ get; set; }

		public static readonly BindableProperty SelectIndexPropery = BindableProperty.Create<MyFirstPage,int> (
			                                                             v => v.SelectIndex, defaultValue: -1, propertyChanged: (bindable, oldValue, newValue) => {
			var myFirstPage = bindable as MyFirstPage;
			if (newValue != -1) {

				myFirstPage.CurrentPage = myFirstPage.Children.ElementAt (newValue);
			}

		});

		public int SelectIndex{ get; set; }

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			HeadView.BindingContext = this.BindingContext;
		
			var collection = (this.BindingContext as FirstModel).LikeColumns;
			collection.ToList ().ForEach (
				
				c => { 
					var page2 = (ContentPage)ViewFactory.CreatePage<MyNewsListModel,Page>((m ,p) => {
						(m as MyNewsListModel).Init (c);
					});

					this.Children.Add (page2);
				}
			);
			this.SetBinding (MyFirstPage.SelectIndexPropery, "SelectIndex");
			collection.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				//
				switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					{
						ContentPage page = (ContentPage)ViewFactory.CreatePage<MyNewsListModel,Page> ((m , p) => {
							
							(m as MyNewsListModel).Init (e.NewItems [0] as Column);
						});
						this.Children.Add (page);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					{
						//
						this.Children.RemoveAt (e.OldStartingIndex);
						//
					}
					break;
				case NotifyCollectionChangedAction.Move:
					{
						var page = this.Children.ElementAt (e.OldStartingIndex);
						this.Children.RemoveAt (e.OldStartingIndex);
						this.Children.Insert (e.NewStartingIndex, page);

					}
					break;
				}
				//
			};
			//(this.BindingContext as FirstModel).Init ();
		}

	}
}

