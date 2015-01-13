using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class LikeColumnView:ContentView
	{
		public LikeColumnView ()
		{
			var grid = new MyGridView{  ColumnSpacing = 5, ItemWidth = 80, ItemHeight = 40, ItemTemplate = new DataTemplate (typeof(MyGridViewCell)) };
			grid.SetBinding (MyGridView.ItemsSourceProperty, "LikeColumns");
			Content= new StackLayout{
				Orientation = StackOrientation.Vertical,
				Children={
					grid,
					new Label{HorizontalOptions=LayoutOptions.EndAndExpand,VerticalOptions=LayoutOptions.EndAndExpand,Text="长按排序或删除"}
				}
			};
		}
	}
}

