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

[assembly:ExportRenderer (typeof(TopMedicalNews.MyStackLayout), typeof(TopMedicalNews.Android.MyStackLayoutRenderer))]

[assembly:ExportRenderer (typeof(TopMedicalNews.MyPullToRefreshScrollView), typeof(TopMedicalNews.Android.MyPullToRefreshSCrollViewRenderer))]
namespace TopMedicalNews.Android
{
	public class MyStackLayoutRenderer: VisualElementRenderer<Xamarin.Forms.View>{

		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
		{
			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
		}
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
		}
	}
	public class MyPullToRefreshSCrollViewRenderer:PullRefreshScrollView,
	IVisualElementRenderer,IRegisterable, IDisposable
	{
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
					return this;
			}
		}

		public MyPullToRefreshSCrollViewRenderer () : base (Forms.Context)
		{

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
				SizeRequest sz = this.Element.GetSizeRequest (0, 0);
				this.MContentLy.LayoutParameters = new LinearLayout.LayoutParams ((int)this.Context.ToPixels(sz.Request.Width),
					(int)this.Context.ToPixels((int)sz.Request.Height));

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
