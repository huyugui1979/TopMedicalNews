using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class MyGridViewCell:ViewCell
	{

		public MyGridViewCell ()
		{
		

			//
			Button button = new Button{BorderRadius=20, FontSize=12,VerticalOptions= LayoutOptions.StartAndExpand,
				HorizontalOptions=LayoutOptions.StartAndExpand};
			button.SetBinding (Button.TextProperty, "Title");

			this.View= button;

		}

	}
}

