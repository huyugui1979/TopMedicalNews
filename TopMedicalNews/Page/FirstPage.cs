using System;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public class FirstPage : MyCarouselPage
	{
		public FirstPage ()
		{
			this.SetBinding (FirstPage.ColumnsProperty, "LikeColumns");
			var tool = new ToolbarItem{ Icon = "setting_btn" };
			tool.SetBinding (ToolbarItem.CommandProperty, "SettingCommand");
			this.ToolbarItems.Add (tool);
			Title = "医界头条";
		}
		public void OrderColumn()
		{
			(this.BindingContext as FirstModel).OrderColumnCommand.Execute (null);
		}

	}
}


