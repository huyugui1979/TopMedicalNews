using System;
using Xamarin.Forms.Platform.Android;
using System.Collections.Specialized;
using TopMedicalNews.Model;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Reflection;

[assembly : Xamarin.Forms.ExportRenderer (typeof(TopMedicalNews.MyFirstPage), typeof(TopMedicalNews.Android.MyCarouselPageRenderer))]
namespace TopMedicalNews.Android
{
	public class MyCarouselPageRenderer:Xamarin.Forms.Platform.Android.CarouselPageRenderer
	{

		public MyCarouselPageRenderer ()
		{

		}
		//
		global::Android.Views.View _view;

		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
		{
			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
			AdjustActionBar ();

		}
		//
		public void AdjustActionBar ()
		{
	
			if (this.Element.Title == "医界头条")
				(Xamarin.Forms.Forms.Context as MainActivity).ActionBar.SetIcon (Resource.Drawable.person_center_btnx);
			else
				(Xamarin.Forms.Forms.Context as MainActivity).ActionBar.SetIcon (Resource.Drawable.return_btnx);

			var view = (global::Android.Widget.ImageView)(Xamarin.Forms.Forms.Context as MainActivity).FindViewById (global::Android.Resource.Id.Home);
			view.SetPadding (15, 0, 0, 0);

		}

		private static BindableProperty _rendererProperty;

		public static BindableProperty RendererProperty {
			get {
				Type platformType = Type.GetType ("Xamarin.Forms.Platform.Android.Platform, Xamarin.Forms.Platform.Android", true);

				return _rendererProperty ?? (_rendererProperty = (BindableProperty)platformType.GetField ("RendererProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public).GetValue (null)); 
			}
		}
		//
		IVisualElementRenderer _render;
		MyFirstPage _page;
		//
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.CarouselPage> e)
		{
			base.OnElementChanged (e);
		
			_page = e.NewElement as MyFirstPage;
			_render = RendererFactory.GetRenderer (_page.HeadView);
			_page.HeadView.SetValue (RendererProperty, _render);
			_page.HeadView.Layout (new Rectangle (0, 0, App.ScreenWidth, 40));
			this.AddView (_render.ViewGroup);
			MessagingCenter.Subscribe<object> (this, "setcolumn", obj => {
				//
				this.Post (() => {
					this.RemoveView (_render.ViewGroup);
					_render.Dispose ();
					_render = null;
					_render = RendererFactory.GetRenderer (_page.HeadView);
					_page.HeadView.SetValue (RendererProperty, _render);
				
					_page.HeadView.Layout (new Rectangle (0, 0, App.ScreenWidth, 40));
					this.AddView (_render.ViewGroup);
				});
				//
			});

		}

		public override bool OnTouchEvent (global::Android.Views.MotionEvent e)
		{
			//System.Diagnostics.Debug.WriteLine ("ontouchevent");
			return true;
		}


		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
			if (_render != null) {
				_render.ViewGroup.Layout (0, 0, r - l, (int)this.Context.ToPixels (40));
				var v = this.GetChildAt (0);
				v.Top = (int)this.Context.ToPixels (40);
			}

		}
	}
}

