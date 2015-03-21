using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Views;
using System.Collections;
using Xamarin.Forms;
using TopMedicalNews.Model;

[assembly:ExportRenderer(typeof(TopMedicalNews.MyGridView), typeof(TopMedicalNews.Android.MyGridViewRenderer))]
namespace TopMedicalNews.Android
{
	public class MyGridViewRenderer:ViewRenderer<MyGridView,DragGridView>
	{
		public MyGridViewRenderer ()
		{
		}
	
	
		public DragGridView CollectionView;
		protected override void OnElementChanged (ElementChangedEventArgs<MyGridView> e)
		{
			base.OnElementChanged (e);
			//
			CollectionView = new DragGridView(Xamarin.Forms.Forms.Context){};
			CollectionView.DragMode = this.Element.DragMode;
			CollectionView.SetGravity(GravityFlags.Center);
			CollectionView.SetColumnWidth (Convert.ToInt32(Element.ItemWidth));
			CollectionView.StretchMode = StretchMode.StretchColumnWidth;

			//
			var metrics = Resources.DisplayMetrics;

			var spacing = (int)e.NewElement.ColumnSpacing;
			//var width = App.ScreenWidth;
			var itemWidth = (int)e.NewElement.ItemWidth;

			int noOfColumns = (int)App.ScreenWidth / (itemWidth + spacing);
			// If possible add another row without spacing (because the number of columns will be one less than the number of spacings)
			if (App.ScreenWidth - (noOfColumns * (itemWidth + spacing)) >= itemWidth)
				noOfColumns++;
			CollectionView.SetNumColumns (noOfColumns);
			//collectionView.SetPadding(Convert.ToInt32(Element.Padding.Left),Convert.ToInt32(Element.Padding.Top), Convert.ToInt32(Element.Padding.Right),Convert.ToInt32(Element.Padding.Bottom));

			CollectionView.SetBackgroundColor (Element.BackgroundColor.ToAndroid ());
			CollectionView.SetHorizontalSpacing (Convert.ToInt32(Element.RowSpacing));
			CollectionView.SetVerticalSpacing(Convert.ToInt32(Element.ColumnSpacing));
		
			CollectionView.Adapter = new MyGridAdapter (this);
		   
			this.SetNativeControl (CollectionView);
			//
		}

	}
	public class MyGridAdapter:BaseAdapter
	{

		MyGridViewRenderer _MyGridView=null;
		public MyGridAdapter(MyGridViewRenderer view)
		{
			_MyGridView = view;

			view.CollectionView.OnSwitchItemEvent += (int oldPos, int newPos) => {
				_MyGridView.Element.SwapItem(oldPos,newPos);
				NotifyDataSetChanged();
			};
			view.CollectionView.OnClickItemEvent += (int pos) => {
				if(_MyGridView.Element.DragMode)
				{
					if(pos !=0)
					_MyGridView.Element.DeleteLikeItem(pos);

				}else
				{
					_MyGridView.Element.InsertLikeItem(_MyGridView.Element.ItemsSource[pos]);
				}

			};
			//通知栏目已经改变
			view.Element.ItemsSource.CollectionChanged+= (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				//
				NotifyDataSetChanged();
				//
				_MyGridView.CollectionView.Post(()=>{
					_MyGridView.Element.HeightRequest = _MyGridView.CollectionView.LayoutParameters.Height/Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
				});
				
			};
		}
		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count {
			get {
				return _MyGridView.Element.ItemsSource.Count;
			}
		}

		//
		//
		public override global::Android.Views.View GetView (int position, global::Android.Views.View convertView, global::Android.Views.ViewGroup parent)
		{


			var item = _MyGridView.Element.ItemsSource [position];
			if (convertView == null) {
				var viewCellBinded = (_MyGridView.Element.ItemTemplate.CreateContent () as ViewCell);

				viewCellBinded.BindingContext = item;
				viewCellBinded.View.BackgroundColor = Color.Transparent;
				var view = RendererFactory.GetRenderer (viewCellBinded.View) as ButtonRenderer;// as global::Android.Views.ViewGroup;//.Button;

				//var drawable = _MyGridView.Context.GetDrawable (Resource.Drawable.switch_column_bg);
				var drawable = _MyGridView.Resources.GetDrawable (Resource.Drawable.switch_column_bg);
				view.SetBackgroundDrawable(drawable);
				// Platform.SetRenderer (viewCellBinded.View, view);

				view.ViewGroup.LayoutParameters = new  global::Android.Widget.GridView.LayoutParams (Convert.ToInt32 (_MyGridView.Element.ItemWidth * Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density),
					Convert.ToInt32 (_MyGridView.Element.ItemHeight * Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density));
				//
				return view.ViewGroup;
			} else {
				(convertView as ButtonRenderer).Element.BindingContext = item;


				return convertView;
			}
		}
		//
	}

}

