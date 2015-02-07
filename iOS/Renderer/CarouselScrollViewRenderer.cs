﻿using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.Diagnostics;
using UIKit;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;

[assembly:ExportRenderer(typeof(TopMedicalNews.CarouselScrollView), typeof(TopMedicalNews.iOS.CarouselScrollViewRenderer))]
namespace  TopMedicalNews.iOS
{
	public class CarouselScrollViewRenderer : ScrollViewRenderer
	{
		UIScrollView _native;

		public CarouselScrollViewRenderer ()
		{
			PagingEnabled = true;
			ShowsHorizontalScrollIndicator = false;
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) return;

			_native = (UIScrollView)NativeView;
			_native.Scrolled += NativeScrolled;
			e.NewElement.PropertyChanged += ElementPropertyChanged;
		}

		void NativeScrolled (object sender, EventArgs e)
		{
			var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
			((CarouselScrollView)Element).SelectedIndex = ((int)center) / ((int)_native.Bounds.Width);
		}

		void ElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == CarouselScrollView.SelectedIndexProperty.PropertyName && !Dragging) {
				ScrollToSelection (false);
			}
		}

		void ScrollToSelection (bool animate)
		{
			if (Element == null) return;

			_native.SetContentOffset (new CGPoint 
				(_native.Bounds.Width * 
					Math.Max(0, ((CarouselScrollView)Element).SelectedIndex), 
					_native.ContentOffset.Y), 
				animate);
		}

//		public override void Draw (RectangleF rect)
//		{
//			base.Draw (rect);
//			ScrollToSelection (false);
//		}
	}
}

