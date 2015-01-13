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
	public class MyGridViewRenderer:ViewRenderer<MyGridView,GridView>
	{
		public MyGridViewRenderer ()
		{
		}
		public global::Android.Widget.GridView CollectionView;
		protected override void OnElementChanged (ElementChangedEventArgs<MyGridView> e)
		{
			base.OnElementChanged (e);
			//
			CollectionView = new global::Android.Widget.GridView (Xamarin.Forms.Forms.Context);
			CollectionView.SetGravity(GravityFlags.Center);
			CollectionView.SetColumnWidth (Convert.ToInt32(Element.ItemWidth));
			CollectionView.StretchMode = StretchMode.StretchColumnWidth;

			var metrics = Resources.DisplayMetrics;

			var spacing = (int)e.NewElement.ColumnSpacing;
			//var width = App.ScreenWidth;
			var itemWidth = (int)e.NewElement.ItemWidth;

			int noOfColumns = App.ScreenWidth / (itemWidth + spacing);
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
				return (_MyGridView.Element.ItemsSource as IList).Count;
			}
		}


		public override global::Android.Views.View GetView (int position, global::Android.Views.View convertView, global::Android.Views.ViewGroup parent)
		{

			var item = (_MyGridView.Element.ItemsSource as IList)[position] as Column;
			if(convertView == null)
			{
			var inflater = LayoutInflater.From(Xamarin.Forms.Forms.Context);

				 convertView = inflater.Inflate(Resource.Layout.ListItem, parent, false);
				convertView.Tag = new Java.Lang.Integer (position);
				convertView.LayoutParameters = new  global::Android.Widget.GridView.LayoutParams 
				(Convert.ToInt32(_MyGridView.Element.ItemWidth*Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density), 
				Convert.ToInt32(_MyGridView.Element.ItemHeight*Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density));
				global::Android.Widget.Button button = (global::Android.Widget.Button)convertView.FindViewById (Resource.Id.myButton);
				button.LongClick += (object sender, global::Android.Views.View.LongClickEventArgs e) => {
					//
					for (int i = 0; i < _MyGridView.CollectionView.ChildCount; i++) {
						var c = _MyGridView.CollectionView.GetChildAt (i);
						global::Android.Widget.ImageView b = (global::Android.Widget.ImageView)c.FindViewById (Resource.Id.imageView);
						b.Visibility= ViewStates.Visible;
					}
				};
				global::Android.Widget.ImageView imageView = (global::Android.Widget.ImageView)convertView.FindViewById (Resource.Id.imageView);
				imageView.Click+= (object sender, EventArgs e) => {
					global::Android.Views.View  view = (sender as global::Android.Views.View).Parent as global::Android.Views.View ;
					Java.Lang.Integer index = view.Tag as Java.Lang.Integer;
					var it =(_MyGridView.Element.ItemsSource as IList)[index.IntValue()];
					_MyGridView.Element.DeleteItem(it);
					NotifyDataSetChanged();
				};
			}

			var name = convertView.FindViewById<TextView>(Resource.Id.myButton);
			name.Text = item.Title;
			return convertView;
		}
		//
	}

}

