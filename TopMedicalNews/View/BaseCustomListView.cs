using System;
using Xamarin.Forms;
using System.Collections;

namespace TopMedicalNews
{
	public class BaseCustomListView:ContentView
	{
		protected StackLayout _stack;

		public BaseCustomListView ()
		{
			ScrollView scroll = new ScrollView{ Orientation = ScrollOrientation.Vertical };

			_stack = new StackLayout{ Orientation = StackOrientation.Vertical,Padding=new Thickness(10,0,10,0)};
			scroll.Content = _stack;
			this.Content = scroll;
			//
			_Button = new  MyButton{ Text = "加载更多", HeightRequest=30, HorizontalOptions = LayoutOptions.Fill, IsVisible = false};

			_Button.SetBinding (Button.CommandProperty, "GetMoreCommand");
		   
			_Button.BackgroundColor = Color.FromRgb (0xCD,0xCD,0xCD);
		
			_stack.Children.Add (_Button);
		}
		protected MyButton _Button=null;

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<BaseCustomListView, IList> (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((BaseCustomListView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((BaseCustomListView)bindableObject).ItemsSourceChanged ();
				}
			);
		//
		public IList ItemsSource {
			get {
				return (IList)GetValue (ItemsSourceProperty);
			}
			set {
				SetValue (ItemsSourceProperty, value);
			}
		}
		protected virtual void ItemsSourceChanging ()
		{
		}
		protected virtual void ItemsSourceChanged ()
		{
		}
	}
}

