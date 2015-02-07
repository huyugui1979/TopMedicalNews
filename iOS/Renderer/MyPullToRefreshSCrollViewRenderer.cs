using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPullToRefreshScrollView), typeof(TopMedicalNews.iOS.MyPullToRefreshSCrollViewRenderer))]
namespace TopMedicalNews.iOS
{
	public class MyPullToRefreshSCrollViewRenderer:ScrollViewRenderer
	{
		public MyPullToRefreshSCrollViewRenderer ()
		{

		}
		private UIRefreshControl refreshControl;
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
		
			if (refreshControl != null)
				return;

			var pullToRefreshListView = (MyPullToRefreshScrollView)this.Element; 

			refreshControl = new UIRefreshControl ();
			refreshControl.AttributedTitle = new Foundation.NSAttributedString ("正在加载");
			refreshControl.AddTarget(RefreshEventHandler,UIControlEvent.ValueChanged);
			this.AddSubview (refreshControl);
		}
		private void RefreshEventHandler (object obj, EventArgs args)
		{
			System.Threading.ThreadPool.QueueUserWorkItem ((callback) => {  
				InvokeOnMainThread (delegate() {
					System.Threading.Thread.Sleep (3000);             
					refreshControl.EndRefreshing ();
				});
			});
		}
	}
}

