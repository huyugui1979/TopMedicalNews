using System;

using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Com.Li6a209;
using System.ComponentModel;
using Android.Content;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using Android.Widget;
using System.Threading.Tasks;


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPullToRefreshScrollView), typeof(TopMedicalNews.Android.MyPullToRefreshSCrollViewRenderer))]
namespace TopMedicalNews.Android
{

	public class MyPullToRefreshSCrollViewRenderer:PullRefreshScrollView,Com.Li6a209.PullRefreshScrollView.IOnRefereshListener,
	Com.Li6a209.PullRefreshScrollView.IOnReqMoreListener,
	IVisualElementRenderer,IRegisterable, IDisposable
	{
		public void OnReqMore ()
		{
			this.view.RequestMoreCommand.Execute (null);
		}

		public void  OnReferesh ()
		{
			this.view.RefreshCommand.Execute (null);
	
		}

		//
		// Fields
		//
		private MyPullToRefreshScrollView view;

		private VisualElementTracker tracker;

		public VisualElement Element {
			get {
				return this.view;
			}
		}

		public VisualElementTracker Tracker {
			get {
				return this.tracker;
			}
		}
		public ViewGroup ViewGroup {
			get {
				if (bLoading)
					return this.MContentLy;
				else
					return layout;
			}
		}
		private LinearLayout layout;
	

		public MyPullToRefreshSCrollViewRenderer () : base (Forms.Context)
		{
			this.SetOnRefereshListener (this);
			this.SetOnReqMoreListener (this);
			layout = new LinearLayout (Forms.Context);
			layout.SetBackgroundColor(global::Android.Graphics.Color.White);
				layout.AddView (this);
		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			this.SetElement (null);
			if (disposing) {
				this.tracker.Dispose ();
				this.tracker = null;
				this.RemoveAllViews ();
			}
		}

		public SizeRequest GetDesiredSize (int widthConstraint, int heightConstraint)
		{
			base.Measure (widthConstraint, heightConstraint);
			return new SizeRequest (new Size ((double)base.MeasuredWidth, (double)base.MeasuredHeight), default(Size));
		}

		protected virtual void OnElementChanged (VisualElementChangedEventArgs e)
		{
			EventHandler<VisualElementChangedEventArgs> elementChanged = this.ElementChanged;
			if (elementChanged != null) {
				elementChanged (this, e);
			}
		}
		private VisualElementPackager packager;

		bool bLoading=true;
		public void SetElement (VisualElement element)
		{
			MyPullToRefreshScrollView oldView = this.view;
			this.view = (MyPullToRefreshScrollView)element;
			if (oldView != null) {

			}
			if (element != null) {

				this.tracker = new VisualElementTracker (this);

				packager = new VisualElementPackager (this);
				packager.Load ();
				bLoading = false;
				var sz = this.Element.GetSizeRequest (double.PositiveInfinity, double.PositiveInfinity);
				if (sz.Request.Height < App.ScreenHeight)
					sz.Request = new Size(sz.Request.Width,App.ScreenHeight);
				this.MContentLy.LayoutParameters = new LinearLayout.LayoutParams ((int)this.Context.ToPixels((int)sz.Request.Width),
					(int)this.Context.ToPixels((int)sz.Request.Height));
				this.Element.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
					//
					if(e.PropertyName == "IsRefreshing")
					{
						if(view.IsRefreshing == false)
						{
							this.RefreshOver();

							var szz = this.Element.GetSizeRequest (double.PositiveInfinity, double.PositiveInfinity);
							if (szz.Request.Height < App.ScreenHeight)
								szz.Request = new Size(sz.Request.Width,App.ScreenHeight);
							this.MContentLy.LayoutParameters = new LinearLayout.LayoutParams ((int)this.Context.ToPixels((int)szz.Request.Width),
								(int)this.Context.ToPixels((int)szz.Request.Height));
						}else
						{
							this.SetToRefreshing();
						}

					}
					if(e.PropertyName =="RequestMoring")
					{
						if(view.RequestMoring == false)
						{

							this.GetMoreOver();
							var szz = this.Element.GetSizeRequest (double.PositiveInfinity, double.PositiveInfinity);
							if (szz.Request.Height < App.ScreenHeight)
								szz.Request = new Size(sz.Request.Width,App.ScreenHeight);
							this.MContentLy.LayoutParameters = new LinearLayout.LayoutParams ((int)this.Context.ToPixels((int)szz.Request.Width),
								(int)this.Context.ToPixels((int)szz.Request.Height));
						}
					}
					//
				};
			}
		}
	
		public void UpdateLayout ()
		{
			if (this.tracker != null) {
			
				this.tracker.UpdateLayout ();

			}
		}

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
	}

}
