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


[assembly:ExportRenderer (typeof(TopMedicalNews.MyPullToRefreshScrollView), typeof(TopMedicalNews.Android.MyPullToRefreshSCrollViewRenderer))]
namespace TopMedicalNews.Android
{

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
				var content = this.MContentLy;
				SizeRequest sz = this.Element.GetSizeRequest (0, 0);
				this.MContentLy.LayoutParameters = new LinearLayout.LayoutParams (App.ScreenWidth * 3, (int)sz.Request.Height * 3);

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

