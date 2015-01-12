using System;
using Xamarin.Forms;
using System.Collections;

namespace TopMedicalNews
{
	public class SwitchScrollView:ContentView
	{
		StackLayout _ItemStack=null;
		StackLayout _ContainStack=null;
		Label       _Title =null;
		StackLayout _BoxStack=null;
		CarouselScrollView  _scrollView=null;

		public SwitchScrollView ()
		{
			//

			_scrollView = new CarouselScrollView { Orientation= ScrollOrientation.Horizontal,BackgroundColor=Color.Blue,VerticalOptions=LayoutOptions.FillAndExpand};
			_scrollView.OnSelctItem += (object sender, int e) => {
				UpdateSelectIndex(e);
			};

			//
			_Title = new Label{VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.Start};
			//
			_ItemStack= new StackLayout{Orientation = StackOrientation.Horizontal, VerticalOptions=LayoutOptions.FillAndExpand};

			_scrollView.Content = _ItemStack;

			_BoxStack = new StackLayout{ Orientation = StackOrientation.Horizontal,HorizontalOptions=LayoutOptions.EndAndExpand,
				VerticalOptions=LayoutOptions.CenterAndExpand
			};
			_ContainStack = new StackLayout { Orientation = StackOrientation.Vertical,
				VerticalOptions=LayoutOptions.FillAndExpand,
				Children = {
					_scrollView,
					new StackLayout { Orientation = StackOrientation.Horizontal,
						Padding = new Thickness (5, 0, 10, 0),
						Children = {
							_Title,
							_BoxStack,
						}
					}
				}
			};
			Content = _ContainStack;
		}
//		public static readonly BindableProperty SelectedItemProperty =
//			BindableProperty.Create<SwitchScrollView, object> (
//				carousel => carousel.SelectedItem,
//				null,
//				BindingMode.OneWayToSource,
//				propertyChanged: (bindable, oldValue, newValue) => {
//
//				}
//			);

//		public object SelectedItem {
//			get {
//				return (object)GetValue (SelectedItemProperty);
//			}
//			set {
//				SetValue (SelectedItemProperty, value);
//				if (OnItemSelected != null)
//					OnItemSelected.Invoke (this, value);
//
//			}
//		}
		/// <summary>
		/// Occurs when item is selected.
		/// </summary>
		//public event EventHandler<int> OnItemSelected;
		//
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<SwitchScrollView,IList > (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((SwitchScrollView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((SwitchScrollView)bindableObject).ItemsSourceChanged ();
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
	public void UpdateSelectIndex(int newIndex)
		{
			(_BoxStack.Children [_SelectIndex] as BoxView).Color = Color.Gray;
			(_BoxStack.Children [_SelectIndex] as BoxView).WidthRequest = 1;
			(_BoxStack.Children [_SelectIndex] as BoxView).WidthRequest = 1;
			//
			(_BoxStack.Children [newIndex] as BoxView).Color = Color.Black;
			(_BoxStack.Children [newIndex] as BoxView).WidthRequest = 2;
			(_BoxStack.Children [newIndex] as BoxView).WidthRequest = 2;
			//
			_Title.Text = ItemsSource[newIndex].ToString ();
			_SelectIndex = newIndex;
		}
		void ItemsSourceChanging ()
		{
			//
			//SetValue (SelectedItem, null);
			//
		}
		public DataTemplate ItemTemplate {
			get;
			set;
		}
		int _SelectIndex=0;
		void ItemsSourceChanged ()
		{
			_ItemStack.Children.Clear ();
			_BoxStack.Children.Clear ();
			foreach (var item in ItemsSource) {
				Image view = (Image)ItemTemplate.CreateContent ();

				var bindableObject = view as BindableObject;
				if (bindableObject != null)
					bindableObject.BindingContext = item;
				var binding = new Binding("ImageUri", BindingMode.Default, new StringToImageSourceConverter());
				view.SetBinding(Image.SourceProperty,binding);
				view.WidthRequest = App.ScreenWidth;
				view.Aspect = Aspect.Fill;
				view.VerticalOptions = LayoutOptions.FillAndExpand;
				_ItemStack.Children.Add (view);
				BoxView box = new BoxView{WidthRequest=1,HeightRequest=1,Color=Color.Gray};
				_BoxStack.Children.Add (box);
			}

			(_BoxStack.Children [0] as BoxView).Color = Color.Black;
			(_BoxStack.Children [0] as BoxView).WidthRequest = 2;
			(_BoxStack.Children [0] as BoxView).WidthRequest = 2;
			_Title.Text = ItemsSource[0].ToString ();
			//
		}

	    ///

	
	}
}

