using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPullToRefreshScrollView), typeof(TopMedicalNews.iOS.MyPullToRefreshSCrollViewRenderer))]
namespace TopMedicalNews.iOS
{

	public class MyPullToRefreshSCrollViewRenderer:ScrollViewRenderer
	{
		public MyPullToRefreshSCrollViewRenderer ()
		{

		}
		private UIRefreshControl refreshControl;
		private MyPullToRefreshScrollView view;
		bool bReqMoring=false;
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
		
			if (refreshControl != null)
				return;
		
			this.DraggingEnded += (object sender, DraggingEventArgs ee) => {
				nfloat bottomEdge = this.ContentOffset.Y + this.Frame.Size.Height;
				if (bottomEdge >= this.ContentSize.Height+50 && bReqMoring == false && view.IsEnd == false) {
					//
					if(this.ViewWithTag(7777) !=null)
						this.ViewWithTag(7777).RemoveFromSuperview();

					UIActivityIndicatorView act = new UIActivityIndicatorView(new CoreGraphics.CGRect((nfloat)0.0, this.ContentSize.Height, this.ContentSize.Width, (nfloat)50.0));//.Gray);
				
					act.Tag=999;
					act.Color=UIColor.Black;
					this.AddSubview(act);
					act.StartAnimating();
					//
					this.view.RequestMoreCommand.Execute (null);
					//
					this.ContentSize = new CGSize(this.ContentSize.Width,this.ContentSize.Height+50);
					bReqMoring = true;
				}

			};
			this.DecelerationEnded += (object sender, EventArgs ee) => {
				if(this.ViewWithTag(7777) !=null)
					this.ViewWithTag(7777).RemoveFromSuperview();
				UILabel temp = new UILabel(new CGRect(0,this.ContentSize.Height,this.ContentSize.Width,50));
				temp.Tag = 7777;
				temp.TextAlignment = UITextAlignment.Center;
				this.AddSubview(temp);
				if(view.IsEnd == true) //已经到达数据的顶端
				{
					temp.Text="已经到达顶端";
				}else
				{
					temp.Text="上拉加载更多";
				}
			};
			view = (MyPullToRefreshScrollView)this.Element; 

			refreshControl = new UIRefreshControl ();
			refreshControl.AttributedTitle = new Foundation.NSAttributedString ("正在加载");
			refreshControl.AddTarget (RefreshEventHandler, UIControlEvent.ValueChanged);
			this.AddSubview (refreshControl);
			view.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs ee) => {
				;
				if (ee.PropertyName == "IsRefreshing") {
					if (view.IsRefreshing == false) {
						refreshControl.EndRefreshing ();

						//var szz = this.Element.GetSizeRequest (double.PositiveInfinity, double.PositiveInfinity);

					} else {
						refreshControl.BeginRefreshing ();
						this.SetContentOffset(new CoreGraphics.CGPoint(0,this.ContentOffset.Y-refreshControl.Frame.Size.Height),true);

						//[self.tableView setContentOffset:CGPointMake(0, self.tableView.contentOffset.y-self.refreshControl.frame.size.height) animated:YES];
					}
				}
				if(ee.PropertyName =="RequestMoring")
				{
					if(view.RequestMoring == false)
					{
						bReqMoring=false;
						this.ViewWithTag(999).RemoveFromSuperview();
						if(this.view.IsEnd==true)
						this.ContentSize = new CGSize(this.ContentSize.Width,this.ContentSize.Height-50);
					}
				}

			};
		}

		private void RefreshEventHandler (object obj, EventArgs args)
		{
			System.Threading.ThreadPool.QueueUserWorkItem ((callback) => {  
				InvokeOnMainThread (delegate() {
					view.RefreshCommand.Execute (null);        

				});
			});
		}
	}
}

