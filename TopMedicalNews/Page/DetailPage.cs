using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class DetailPage:ContentPage
	{
		//创建切换图像
		View CreateSwitchImage()
		{
			SwitchScrollView scroll = new SwitchScrollView (){ VerticalOptions= LayoutOptions.StartAndExpand, HeightRequest=180,ItemTemplate=new DataTemplate(typeof(Image))};
			scroll.SetBinding (SwitchScrollView.ItemsSourceProperty, "FocusNews");
			return scroll;
		}
		//创建栏目
		View CreateColumn()
		{
			SelectColumnView view = new SelectColumnView{ };
			view.SetBinding (SelectColumnView.ItemsSourceProperty, "LikeColumn");
			return view;
		}
		//创建新联列表
		View CreateNewsList()
		{

			var listView = new ListView {
				ItemTemplate = new DataTemplate(typeof(NewsCell)),
				RowHeight =100
			};
			listView.SetBinding (ListView.ItemsSourceProperty, "SelectNews");
			//
			return listView;
		}
		View CreateRootView ()
		{
			var total =  new StackLayout {
				Children={
					CreateColumn(),
					CreateSwitchImage(),
					CreateNewsList()
				}
			};
			return total;
			//
		}
		public DetailPage ()
		{
			//
			BaseModel.CreateAndBind<DetailMode> (this, null);
			Content = CreateRootView();
			//
		
	
			//对于选中的每一列，进行更新图像
			
			//
		}
	}
}

