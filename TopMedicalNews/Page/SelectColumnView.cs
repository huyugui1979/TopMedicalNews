using System;
using Xamarin.Forms;
using System.Collections;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class SelectColumnView:ContentView
	{
		StackLayout _Content;

		public SelectColumnView ()
		{
			_Content =new StackLayout{
				Orientation= StackOrientation.Horizontal,
			};
			//添加栏目

			var scroll =  new NoBarScrollView {
				Orientation= ScrollOrientation.Horizontal,
				Content = _Content,
			};
			//创建下拉栏目按钮
			Button button = new Button{ Text="v",WidthRequest = 40,BackgroundColor= Color.Transparent,HorizontalOptions = LayoutOptions.End,};
			Content = new  StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
				Children = {
					scroll,
					button,
				}
			};
			//

		}
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<SelectColumnView,IList > (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((SelectColumnView)bindableObject).ItemsSourceChanging ();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((SelectColumnView)bindableObject).ItemsSourceChanged ();
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
			//
			//SetValue (SelectedItem, null);
			//
		}
		void ItemsSourceChanged ()
		{
			//
			foreach (Column item in ItemsSource) {
				Label label=	new Label{ VerticalOptions = LayoutOptions.Start };
				label.BindingContext = item;
				label.SetBinding (Label.TextProperty, "Title");
				label.GestureRecognizers.Add(new TapGestureRecognizer((v)=>{
					Label s = (Label)v;
					//
					foreach( StackLayout c in (s.Parent.Parent as StackLayout).Children)
					{
						c.Children[1].IsVisible=false;
						(c.Children[0] as Label).TextColor= Color.Black;
					}
					(s.Parent as StackLayout).Children[1].IsVisible=true;
					((s.Parent as StackLayout).Children[0] as Label).TextColor=Color.Red;
					(this.BindingContext as DetailMode).SelectColumn = (s.BindingContext as Column);
				}));
				StackLayout layout = new StackLayout { Orientation = StackOrientation.Vertical,
					VerticalOptions= LayoutOptions.Center,
					Spacing=2,
					Children = { 
						label,
						new BoxView{ HeightRequest = 1,VerticalOptions= LayoutOptions.Start,IsVisible=false, Color = Color.Red }
					}
				};
				_Content.Children.Add (layout);
			}
			//

			//
		}
	}

}

