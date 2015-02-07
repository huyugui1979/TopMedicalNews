using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
namespace TopMedicalNews
{
	public class CarouselScrollView:ScrollView
	{
		readonly StackLayout _stack;

		public CarouselScrollView ()
		{
			Orientation = ScrollOrientation.Horizontal;

			_stack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Spacing = 0,
				HeightRequest=this.HeightRequest,
			};

			Content = _stack;

		}

	    int _selectedIndex;
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<CarouselScrollView, IList> (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((CarouselScrollView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((CarouselScrollView)bindableObject).ItemsSourceChanged ();
				}
			);
		//
		//
		public IList ItemsSource {
			get {
				return (IList)GetValue (ItemsSourceProperty);
			}
			set {
				SetValue (ItemsSourceProperty, value);
			}
		}
		public static readonly BindableProperty SelectedItemProperty = 
			BindableProperty.Create<CarouselScrollView, object> (
				view => view.SelectedItem,
				null,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) => {
					((CarouselScrollView)bindable).UpdateSelectedIndex ();
				}
			);
		void ItemsSourceChanging ()
		{
			if (ItemsSource == null) return;
			_selectedIndex = ItemsSource.IndexOf (SelectedItem);
		}
		public DataTemplate ItemTemplate {
			get;
			set;
		}

		void ItemsSourceChanged ()
		{
			_stack.Children.Clear ();
			foreach (var item in ItemsSource) {
				var view = (View)ItemTemplate.CreateContent ();
				view.WidthRequest = App.ScreenWidth;
				var bindableObject = view as BindableObject;

				if (bindableObject != null)
					bindableObject.BindingContext = item;
				_stack.Children.Add (view);
			}

			if (_selectedIndex >= 0) SelectedIndex = _selectedIndex;
		}
		public object SelectedItem {
			get {
				return GetValue (SelectedItemProperty);
			}
			set {
				SetValue (SelectedItemProperty, value);
			}
		}
		//
		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create<CarouselScrollView, int> (
				carousel => carousel.SelectedIndex,
				0,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) => {
				
					((CarouselScrollView)bindable).UpdateSelectedItem ();
				}
			);

		public int SelectedIndex {
			get {
				return (int)GetValue (SelectedIndexProperty);
			}
			set {
				SetValue (SelectedIndexProperty, value);
			}
		}
		void UpdateSelectedItem ()
		{

			SelectedItem = ItemsSource [SelectedIndex];
		}
		void UpdateSelectedIndex ()
		{
			if (SelectedItem == BindingContext || ItemsSource==null) return;

			SelectedIndex = ItemsSource
				.IndexOf (SelectedItem);
		}
	}
}

