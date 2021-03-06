﻿using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.Reflection;
using System.ComponentModel;
using Android.Views;
using Xamarin.Forms;


[assembly:ExportRenderer(typeof(TopMedicalNews.CarouselScrollView), typeof(TopMedicalNews.Android.CarouselScrollViewRenderer))]
namespace TopMedicalNews.Android
{
	public class CarouselScrollViewRenderer:ScrollViewRenderer
	{
		public CarouselScrollViewRenderer ()
		{
			//
			Device.StartTimer(TimeSpan.FromSeconds(5),()=>{

				var carouselLayout = (CarouselScrollView)this.Element;

				carouselLayout.SelectedIndex
				= carouselLayout.SelectedIndex+1>4?0:carouselLayout.SelectedIndex+1;
				ScrollToIndex(carouselLayout.SelectedIndex);
				return true;
			});
		}
		int _prevScrollX;
		int _deltaX;
		bool _motionDown;

		HorizontalScrollView _scrollView;
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			e.NewElement.SizeChanged += (object sender, EventArgs ee) => {
				_scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
					.GetField ("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
					.GetValue (this);
				_scrollView.HorizontalScrollBarEnabled = false;
				_scrollView.Touch += HScrollViewTouch;


			};
		}
		void SnapScroll ()
		{
			var roughIndex = (float)_scrollView.ScrollX / _scrollView.Width;
		
			var targetIndex = 
				_deltaX < 0 ? Java.Lang.Math.Floor (roughIndex)
				: _deltaX > 0 ? Java.Lang.Math.Ceil (roughIndex)
				: Java.Lang.Math.Round (roughIndex);
			ScrollToIndex ((int)targetIndex);
			var carouselLayout = (CarouselScrollView)this.Element;

			carouselLayout.SelectedIndex = (int)targetIndex;
		}
		void ScrollToIndex (int targetIndex)
		{
			
			var targetX = targetIndex * _scrollView.Width;
			_scrollView.Post (new Java.Lang.Runnable (() => {
				_scrollView.SmoothScrollTo (targetX, 0);
			}));
		}
		bool _scrolling=false;
		void HScrollViewTouch (object sender, TouchEventArgs e)
		{
			e.Handled = false;
		
			switch (e.Event.Action) {
			case MotionEventActions.Move:
				_deltaX = _scrollView.ScrollX - _prevScrollX;
				_prevScrollX = _scrollView.ScrollX;
				_scrolling = true;
				break;
			case MotionEventActions.Down:
				_motionDown = true;
				break;
			case MotionEventActions.Up:
				if (_scrolling == true && _motionDown==true) {
					SnapScroll ();
				} else  if(_scrolling==false && _motionDown==true){
					(this.Element as CarouselScrollView).SelectCommand.Execute (null);
				}
				_scrolling = false;
				_motionDown = false;
				break;

			}
		}
	}
}

