using System;
using Xamarin.Forms.Platform.Android;
using System.Collections.Specialized;
using XLabs.Forms.Mvvm;
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

		global::Android.Views.View _view;
		//

		//
		//		public void Load()
		//		{
		//			var ll = _view.FindViewById<global::Android.Widget.LinearLayout> (Resource.Id.content);
		//
		//			//找出新增加的
		//			var columns = view.Columns.Except (_Columns, Equality<Column>.CreateComparer (r => r.ID)).ToList ();
		//			columns.ForEach (cc => {
		//				//
		//				var page = ViewFactory.CreatePage<MyNewsListModel,Xamarin.Forms.ContentPage> (null);
		//				((page as Xamarin.Forms.ContentPage).BindingContext as MyNewsListModel).Init(cc);
		//
		//				this.Element.Children.Add (page as Xamarin.Forms.ContentPage);
		//
		//				//
		//				_Columns.Add(cc);
		//			});
		//			//找出删除的
		//			columns = _Columns.Except(view.Columns, Equality<Column>.CreateComparer (r => r.ID)).ToList();
		//			columns.ForEach (cc => {
		//				var index = _Columns.IndexOf(cc);
		//				//
		//				this.Element.Children.RemoveAt(index);
		//				//
		//				_Columns.Remove(cc);
		//			});
		//			//根据排序,重新整理
		//			List<Xamarin.Forms.ContentPage> pages = new List<Xamarin.Forms.ContentPage> (this.Element.Children);
		//			this.Element.Children.Clear ();
		//			for(int i =0;i<view.Columns.Count;i++)
		//			{ bn
		//				var index = _Columns.IndexOf(view.Columns[i]);
		//				this.Element.Children.Add (pages [index]);
		//			}
		//		    ll.RemoveAllViews ();
		//
		//			_Columns = view.Columns;
		//			_Columns.ForEach(c=>{var text = new global::Android.Widget.TextView (this.Context){ Text = c.Title };
		//				//text.TextAlignment = global::Android.Views.TextAlignment.Center;
		//				text.SoundEffectsEnabled = false;
		//				text.Gravity= global::Android.Views.GravityFlags.FillVertical;
		//				text.TextSize=18;
		//				text.SetPadding (20, 0, 20, 0);
		//				ll.AddView (text);
		//				text.Click += (object so, EventArgs eee) => {
		//					var tt = so as global::Android.Widget.TextView;
		//					int index = ll.IndexOfChild(tt);
		//
		//					this.Element.CurrentPage = this.Element.Children [index];
		//					Xamarin.Forms.MessagingCenter.Send<Column> (c, "ColumnSelect");
		//
		//				};
		//			});
		//			oldView = (global::Android.Widget.TextView)ll.GetChildAt (0);
		//			oldView.SetTextColor (global::Android.Graphics.Color.Blue);
		//			Xamarin.Forms.MessagingCenter.Send<Column> (_Columns [0], "ColumnSelect");
		//		}
		//		global::Android.Widget.TextView oldView;
		public override void MeasureAndLayout (int p0, int p1, int p2, int p3, int p4, int p5)
		{
			base.MeasureAndLayout (p0, p1, p2, p3, p4, p5);
			AdjustActionBar ();

		}

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

		IVisualElementRenderer _render;
		MyFirstPage _page;

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
		


		

			//


			//
//			view = this.Element as MyFirstPage;
//			//
//			var inflater = global::Android.App.Application.Context.GetSystemService ("layout_inflater") as global::Android.Views.LayoutInflater;
//			//
//			_view = inflater.Inflate (Resource.Layout.column, null);
//			//
//			this.Element.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs ee) => {
//				if (ee.PropertyName == "Columns") {
//					Load();
//				}
//			};
//			//

			//
//			this.Element.CurrentPageChanged += (object sender, EventArgs ee) => {
//				int index = this.Element.Children.IndexOf (this.Element.CurrentPage);
//			
//				var vol = _view.FindViewById<global::Android.Widget.HorizontalScrollView> (Resource.Id.mColumnHorizontalScrollView);
//				var content = _view.FindViewById<global::Android.Widget.LinearLayout> (Resource.Id.content);
//				var cview = (global::Android.Widget.TextView)content.GetChildAt (index);
//				if (oldView != null)
//					oldView.SetTextColor (global::Android.Graphics.Color.Black);
//				if (cview != null) {
//					Xamarin.Forms.MessagingCenter.Send<Column> (_Columns [index], "ColumnSelect");
//					cview.SetTextColor (global::Android.Graphics.Color.Blue);
//					oldView = cview;
//					vol.Post (() => {
//						vol.ScrollTo (cview.Left, 0);
//					});
//
//				}
//			};
//			//
//			var more = _view.FindViewById<global::Android.Widget.ImageView> (Resource.Id.button_more_columns);
//			more.Click += (object sender, EventArgs ee) => {
//				//
//				FirstPage page = view as FirstPage;
//				page.OrderColumn();
//				//
//
//			};
//			//
//
//			var pager = this.GetChildAt (0) as global::Android.Support.V4.View.ViewPager;
//		
//			this.AddView (_view);
			//
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

