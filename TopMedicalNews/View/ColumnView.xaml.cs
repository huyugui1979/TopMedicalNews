using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;

namespace TopMedicalNews
{
	public partial class ColumnView : ContentView
	{
		public ColumnView ()
		{
			InitializeComponent ();

		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<ColumnView, IList> (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((ColumnView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((ColumnView)bindableObject).ItemsSourceChanged ();
				}
			);

		public IList ItemsSource {
			get {
				return (IList)GetValue (ItemsSourceProperty);
			}
			set {
				SetValue (ItemsSourceProperty, value);
			}
		}

		void ItemsSourceChanging ()
		{
			if (ItemsSource == null)
				return;

		}

		public DataTemplate ItemTemplate {
			get;
			set;
		}

		public static readonly BindableProperty SelectedColumnIndexProperty =
			BindableProperty.Create<ColumnView, int> (
				view => view.SelectedColumnIndex,
				0,
				BindingMode.OneWayToSource,
				propertyChanging: (bindable, oldValue, newValue) => {
					//

					//
				},
				propertyChanged: (bindable, oldValue, newValue) => {
					//
					((ColumnView)bindable).SelectedColumnIndexChanged (oldValue, newValue);
					//
				}
			);

		void SelectedColumnIndexChanged (int oldValue, int newValue)
		{
			if (oldValue != -1) {
				(stack.Children [oldValue] as Label).TextColor  = Color.Black;

			}
			if (newValue != -1) {
				(stack.Children [newValue] as Label).TextColor = Color.Blue;
			}
		}

		public int SelectedColumnIndex {
			get {
				return (int)GetValue (SelectedColumnIndexProperty);
			}
			set {
				SetValue (SelectedColumnIndexProperty, value);
			}
		}

		void ItemsSourceChanged ()
		{

			var list = (ItemsSource as IList);
			//_stack.Children.Clear();
			foreach (var item in list) {

				var view = (View)ItemTemplate.CreateContent ();

				if (view != null)
					view.BindingContext = item;
					view.GestureRecognizers.Add (new TapGestureRecognizer (v => {
					this.SelectedColumnIndex = stack.Children.IndexOf (v);
				}));
				stack.Children.Add (view);
			
			}
			(stack.Children [0] as Label).TextColor = Color.Blue;

		
		}
	}
}

