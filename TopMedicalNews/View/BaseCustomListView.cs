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

			_stack = new StackLayout{ Orientation = StackOrientation.Vertical };
			scroll.Content = _stack;
			this.Content = scroll;
		}

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

