using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class DetailPage:ContentPage
	{
		View CreateScrollView ()
		{
			//
			var content =new StackLayout{
				Orientation= StackOrientation.Horizontal,
			};
			foreach (var column in (this.BindingContext as DetailMode).LikeColumn) {
				Label label=	new Label{ Text = column.Title, VerticalOptions = LayoutOptions.Start };
				StackLayout layout = new StackLayout { Orientation = StackOrientation.Vertical,
					VerticalOptions= LayoutOptions.Center,
					Spacing=2,
					Children = { 
						label,
						new BoxView{ HeightRequest = 1,VerticalOptions= LayoutOptions.Start,IsVisible=false, Color = Color.Red }
					}

				};
				label.GestureRecognizers.Add(new TapGestureRecognizer((v)=>{
					Label s = (Label)v;
					foreach( StackLayout c in (s.Parent.Parent as StackLayout).Children)
					{
						c.Children[1].IsVisible=false;
						(c.Children[0] as Label).TextColor= Color.Black;
					}
					(s.Parent as StackLayout).Children[1].IsVisible=true;
					((s.Parent as StackLayout).Children[0] as Label).TextColor=Color.Red;

				}));
				content.Children.Add (layout);
			}
			var scoll =  new NoBarScrollView {
				Orientation= ScrollOrientation.Horizontal,
				Content = content,
			};

			return scoll;
			//
		}
		View CreateButton ()
		{
			Button button = new Button{ Text="v"};
			button.BackgroundColor = Color.Transparent;
			button.HorizontalOptions = LayoutOptions.End;
			button.WidthRequest = 40;
			return button;
			//button.Image = new Image{ };
		}
		View CreateRootStack()
		{
			return new StackLayout {
				Children={
					new StackLayout {
						Orientation= StackOrientation.Horizontal,
						HorizontalOptions=LayoutOptions.FillAndExpand,
						VerticalOptions=LayoutOptions.Start,
						Children={
							CreateScrollView(),
							CreateButton()
						}
					},
					CreateImageView(),
				}
			};
		}

		View CreateImageView ()
		{
			var imageStack= new StackLayout{Orientation = StackOrientation.Horizontal,VerticalOptions=LayoutOptions.Start};
			foreach(var item in (this.BindingContext as DetailMode).FocusNews)
			{
				var stack = new StackLayout{ Orientation = StackOrientation.Vertical,};
				Image image = new Image{HeightRequest=200,WidthRequest=App.ScreenWidth, Source=ImageSource.FromResource("Icon.png")};
//				BoxView box = new BoxView{WidthRequest =App.ScreenWidth
//						, HeightRequest = 200, Color = Color.Red };

				stack.Children.Add (image);
				//
				stack.Children.Add (new Label{ Text = item.Title ,HorizontalOptions= LayoutOptions.Start});
				//
				imageStack.Children.Add (stack);
				//
			}
			//
			SwitchScrollView scroll = new SwitchScrollView (){ Orientation= ScrollOrientation.Horizontal, HeightRequest=180};
			scroll.Content = imageStack;
			//
			var boxStack = new StackLayout{ Orientation = StackOrientation.Horizontal };
			for (int i = 0; i < (this.BindingContext as DetailMode).FocusNews.Count; i++) {
				//
				boxStack.Children.Add (new BoxView{ WidthRequest = 2, HeightRequest = 2, Color =Color.Black });
				//
			}
			scroll.SelectedIndex = 0;
			//

			//

			//
			var relativeLayout = new RelativeLayout{HeightRequest=230};
			relativeLayout.Children.Add(scroll,
				xConstraint: Constraint.Constant(0), 
				yConstraint: Constraint.Constant(0), 
				widthConstraint: Constraint.RelativeToParent ((parent) => {return parent.Width;}),
				heightConstraint: Constraint.RelativeToParent ((parent) => {return 230;}));

	                
			relativeLayout.Children.Add(boxStack,

				Constraint.RelativeToParent((r)=>{
					return 9*r.Width/10;
				}),
				Constraint.RelativeToParent((r)=>{
					return 215;
				}));
			//
			return relativeLayout;
			//
		}
		public DetailPage ()
		{
			//
			BaseModel.CreateAndBind<DetailMode> (this, null);
			Content = CreateRootStack ();
			//
		}
	}
}

