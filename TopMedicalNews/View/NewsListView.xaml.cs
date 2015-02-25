using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TopMedicalNews
{
	public partial class NewsListView : ContentView
	{
		private StackLayout _stack;

		public NewsListView ()
		{

			InitializeComponent ();
			_stack = this.FindByName<StackLayout> ("NewsContent");

		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<NewsListView, IList> (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((NewsListView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((NewsListView)bindableObject).ItemsSourceChanged ();
				}
			);
		//
		public static readonly BindableProperty ItemTapCommandProperty = BindableProperty.Create<NewsListView, ICommand> (p => p.ItemTapCommand, null);

		public ICommand ItemTapCommand {
			get { return (ICommand)GetValue (ItemTapCommandProperty); }
			set { SetValue (ItemTapCommandProperty, value); }
		}
		//
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

		void ItemsSourceChanged ()
		{
		
			(ItemsSource as INotifyCollectionChanged).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => {
				//_stack.Children.Clear();

				switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems) {

						var view = (View)ItemTemplate.CreateContent ();

						view.GestureRecognizers.Clear ();
						view.GestureRecognizers.Add (new TapGestureRecognizer (v => {
							if (ItemTapCommand != null)
								ItemTapCommand.Execute (item);
						}));
						var bindableObject = view as BindableObject;

						if (bindableObject != null)
							bindableObject.BindingContext = item;
						_stack.Children.Add (view);
						
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					_stack.Children.Clear ();
					break;

				}	
			};
		}
	}
}

