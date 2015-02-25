using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class MyPullToRefreshScrollView:ScrollView
	{
	
	
		public static readonly BindableProperty IsRefreshingProperty = 
			BindableProperty.Create<MyPullToRefreshScrollView,bool> (
				p => p.IsRefreshing,false);

		public bool IsRefreshing {
			get { return (bool)GetValue (IsRefreshingProperty); }
			set { SetValue (IsRefreshingProperty, value); }
		}
		public static readonly BindableProperty RequestMoringProperty = 
			BindableProperty.Create<MyPullToRefreshScrollView,bool> (
				p => p.RequestMoring,false);

		public bool RequestMoring {
			get { return (bool)GetValue (RequestMoringProperty); }
			set { SetValue (RequestMoringProperty, value); }
		}
		public static readonly BindableProperty RequestMoreCommandProperty = 
			BindableProperty.Create<MyPullToRefreshScrollView,Command> (
				p => p.RequestMoreCommand, null);

		public Command RequestMoreCommand {
			get { return (Command)GetValue (RequestMoreCommandProperty); }
			set { SetValue (RequestMoreCommandProperty, value); }
		}
		//

		//
		public static readonly BindableProperty RefreshCommandProperty = 
			BindableProperty.Create<MyPullToRefreshScrollView,Command> (
				p => p.RefreshCommand, null);

		public Command RefreshCommand {
			get { return (Command)GetValue (RefreshCommandProperty); }
			set { SetValue (RefreshCommandProperty, value); }
		}
//		protected override void LayoutChildren (double x, double y, double width, double height)
//		{
//			#if __ANDROID__
//			var stack = (this.Content as VisualElement);
//		
//			//SizeRequest sz= stack.GetSizeRequest (0, 0);
//
//			base.LayoutChildren (x, y, width,height);
//			#else
//			base.LayoutChildren ( x,  y,  width,  height);
//			#endif
//			//
//		
//		}

	


	}
}

