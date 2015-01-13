using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class SelectColumnPage:ContentPage
	{
		View CreateTopView()
		{
			Label label = new Label{ Text = "12个栏目点击进入",VerticalOptions=LayoutOptions.Center, HorizontalOptions=LayoutOptions.StartAndExpand};
			Button button = new Button{ WidthRequest=40, FontSize=15,BackgroundColor=Color.Transparent, Text = "V", HorizontalOptions = LayoutOptions.EndAndExpand };

			return new StackLayout { Orientation = StackOrientation.Horizontal,
				Children = {label,
					button
				},
				VerticalOptions = LayoutOptions.Start
			};

		}
		View CreateMiddleView()
		{

			return new   StackLayout { Orientation = StackOrientation.Horizontal,
				Children = {
					new LikeColumnView()
				},
				VerticalOptions = LayoutOptions.Start
			};
		}
		public SelectColumnPage ()
		{
			//
			BaseModel.CreateAndBind<SelectColumnModel> (this,null);
			Content = new StackLayout { Orientation = StackOrientation.Vertical,
				Children = {
					CreateTopView (),
					CreateMiddleView (),
				}
			};
			//
		}
	}
}

