using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
using System.Collections.Specialized;


namespace TopMedicalNews
{
    public class PipSet : StackLayout
    {
        int _selectedIndex;

        public PipSet ()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.Center;
            Spacing = 4;
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<PipSet, IList> (
                pips => pips.ItemsSource,
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).ItemsSourceChanging ();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).ItemsSourceChanged ();
                }
            );

        public IList ItemsSource {
            get {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set {
                SetValue (ItemsSourceProperty, value);
            }
        }

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<PipSet, object> (
                pips => pips.SelectedItem,
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).SelectedItemChanged ();
                });

        public object SelectedItem {
            get {
                return GetValue (SelectedItemProperty);
            }
            set {
                SetValue (SelectedItemProperty, value);
            }
        }

        void ItemsSourceChanging ()
        {
            if (ItemsSource != null)
                _selectedIndex = ItemsSource.IndexOf (SelectedItem);
        }

        void ItemsSourceChanged ()
        {
            if (ItemsSource == null) return;

			(ItemsSource as INotifyCollectionChanged).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => {
				switch( e.Action)
				{
				case NotifyCollectionChangedAction.Add:
					//
					Children.Add (CreatePip ());
					//
					break;
				case  NotifyCollectionChangedAction.Reset:
					//
					Children.Clear();
					//
					break;
				}
			};
        }

        void SelectedItemChanged () {
            var selectedIndex = ItemsSource.IndexOf (SelectedItem);
            var pips = Children.Cast<Image> ().ToList ();

            foreach (var pip in pips) UnselectPip (pip);
			if (pips.Count == 0)
				return;
            if (selectedIndex > -1) SelectPip (pips [selectedIndex]);
        }

        static View CreatePip ()
        {
            return new Image { Source = "pip.png" };
        }

        static void UnselectPip (Image pip)
        {
            pip.Source = "pip.png";
        }

        static void SelectPip (Image pip)
        {
            pip.Source = "pip_selected.png";
        }
    }
}

