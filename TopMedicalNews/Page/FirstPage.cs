using System;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public class FirstPage : CarouselPage
	{
		public FirstPage ()
		{
			var tool = new ToolbarItem{ Icon = "setting_btn" };
			tool.SetBinding (ToolbarItem.CommandProperty, "SettingCommand");
			this.ToolbarItems.Add (tool);
			Title = "医界头条";
		}

	}
}


