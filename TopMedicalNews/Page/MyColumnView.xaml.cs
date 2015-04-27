using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Collections.Specialized;
using System.Linq;
#if __IOS__
using Xamarin.Forms.Platform.iOS;
#endif
namespace TopMedicalNews
{
	public partial class MyColumnView : ContentView
	{
		public MyColumnView ()
		{
			InitializeComponent ();
			scroll.Scrolled += (object sender, ScrolledEventArgs e) => {
				scroll.Scroll_X = e.ScrollX;
			};
		}
		//
	

		public static readonly BindableProperty SelectIndexPropery = BindableProperty.Create<MyColumnView,int> (
			                                                             v => v.SelectIndex, defaultValue: -1, propertyChanged: (bindable, oldValue, newValue) => {
			var myColumnView = bindable as MyColumnView;
			if (oldValue != -1) {
						
					if(oldValue<myColumnView.columnStatck.Children.Count)
					{
				Label oldView = (Label)myColumnView.columnStatck.Children.ElementAt (oldValue);
				
				oldView.TextColor = Color.Black; 
					}
			}
			if (newValue != -1) {
					Label newView = (Label)myColumnView.columnStatck.Children.ElementAt (newValue);
					newView.TextColor = Color.Blue;
			}

		});
		
		public int SelectIndex{ get; set; }
		public void ScrollToPos(int pos)
		{
			if (columnStatck.Children [pos].X > (scroll.Scroll_X + scroll.Width) || columnStatck.Children [pos].X < scroll.Scroll_X)
				scroll.ScrollToAsync (columnStatck.Children [pos].X, columnStatck.Children [pos].Y, false);
		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var collection = (this.BindingContext as FirstModel).LikeColumns;
			collection.ToList ().ForEach (c => {
				var view = new Label{ Text = c.Title,YAlign= TextAlignment.Center,XAlign= TextAlignment.Center, HeightRequest=40,FontSize=18, WidthRequest=80,VerticalOptions=LayoutOptions.Center };
				view.GestureRecognizers.Add (new TapGestureRecognizer (v => {
					int index = columnStatck.Children.IndexOf (v);
					(this.BindingContext as FirstModel).SelectColumnCommand.Execute (index);
				}));
				columnStatck.Children.Add (view);
			});

			this.SetBinding (MyColumnView.SelectIndexPropery, "SelectIndex");
			collection.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				//

				switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					{
						Column c = e.NewItems[0] as Column;
						var view = new Label{ Text = c.Title,YAlign= TextAlignment.Center,XAlign= TextAlignment.Center, HeightRequest=40,FontSize=18, WidthRequest=80,VerticalOptions=LayoutOptions.Center };

						view.GestureRecognizers.Add (new TapGestureRecognizer (v => {
							int index = columnStatck.Children.IndexOf (v);
							(this.BindingContext as FirstModel).SelectColumnCommand.Execute (index);
						}));
						view.BindingContext = e.NewItems;
						columnStatck.Children.Add (view);
					

					}
					break;
				case NotifyCollectionChangedAction.Remove:
					{
						
						columnStatck.Children.RemoveAt (e.OldStartingIndex);
					
					}
					break;
				case NotifyCollectionChangedAction.Move:
					{
						
						var vv = (Label)columnStatck.Children.ElementAt (e.OldStartingIndex);
						vv.TextColor=Color.Black;
						columnStatck.Children.RemoveAt(e.OldStartingIndex);
						columnStatck.Children.Insert(e.NewStartingIndex,vv);
					}
					break;
				}
				//
			};
		}
		                                                             
	}

}

