//using System;
//using Xamarin.Forms.Platform.iOS;
//using System.Collections.Specialized;
//using UIKit;
//using CoreGraphics;
//using System.ComponentModel;
//using Foundation;
//using Xamarin.Forms;
//using CoreGraphics;
//using System.Collections;
//using System.Linq;
//
//[assembly:ExportRenderer(typeof(TopMedicalNews.MyGridView), typeof(TopMedicalNews.iOS.MyGridViewRenderer))]
//namespace TopMedicalNews.iOS
//{
////	public class MyGridViewRenderer: ViewRenderer<MyGridView,UICollectionView>
////	{
////		public MyGridViewRenderer ()
////		{
////		}
////		/// Initializes a new instance of the <see cref="GridViewRenderer"/> class.
////		/// </summary>
////		public GridCollectionView collectionView=null;
////		protected override void OnElementChanged (ElementChangedEventArgs<MyGridView> e)
////		{
////			base.OnElementChanged (e);
////
////				collectionView = new GridCollectionView (/*this*/);
//////				collectionView.CollectionView.AllowsMultipleSelection = false;
//////				collectionView.CollectionView.ContentInset = new UIEdgeInsets ((float)0, (float)0, (float)0, (float)0);
//////
//////				collectionView.CollectionView.BackgroundColor = Element.BackgroundColor.ToUIColor ();
//////				collectionView.ItemSize = new CGSize ((float)Element.ItemWidth, (float)Element.ItemHeight);
//////				collectionView.RowSpacing = Element.RowSpacing;
//////				collectionView.ColumnSpacing = Element.ColumnSpacing;
//////				e.NewElement. TopMedicalNews.iOSHeightRequest = collectionView.CollectionView.CollectionViewLayout.CollectionViewContentSize.Height;
//////
////
////		   SetNativeControl (collectionView.CollectionView);
////[assembly:ExportRenderer(typeof(TopMedicalNews.MyGridView), typeof(TopMedicalNews.iOS.MyGridViewRenderer))]
////
////		}
////	}
//}
//
using Xamarin.Forms;

[assembly:ExportRenderer (typeof(TopMedicalNews.MyGridView), typeof(TopMedicalNews.iOS.GridViewRenderer))]
namespace  TopMedicalNews.iOS
{
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Linq;

	using Foundation;
	using UIKit;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.iOS;

	/// <summary>
	/// Class GridViewRenderer.
	/// </summary>
	public class GridViewRenderer: ViewRenderer<MyGridView,GridCollectionView>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GridViewRenderer"/> class.
		/// </summary>
		public GridViewRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<MyGridView> e)
		{
			base.OnElementChanged (e);

			if (this.Control != null)
				this.Subviews [0].RemoveFromSuperview ();
			var collectionView = new GridCollectionView (new CoreGraphics.CGRect (0, 0, App.ScreenWidth, App.ScreenHeight));

			collectionView.AllowsMultipleSelection = false;
//			collectionView.SelectionEnable = e.NewElement.SelectionEnabled;
			//set padding
			collectionView.ContentInset = new UIEdgeInsets ((float)0, (float)0, (float)0, (float)0);
			collectionView.ScrollEnabled = false;
			collectionView.BackgroundColor = Element.BackgroundColor.ToUIColor ();
			collectionView.ItemSize = new CoreGraphics.CGSize ((float)Element.ItemWidth, (float)Element.ItemHeight);
			collectionView.RowSpacing = Element.RowSpacing;
			collectionView.ColumnSpacing = Element.ColumnSpacing;
		
			collectionView.Source = DataSource;
			//collectionView.Delegate = this.GridViewDelegate;
		    
			SetNativeControl (collectionView);

			var s = collectionView.CollectionViewLayout.CollectionViewContentSize;
			Element.HeightRequest = s.Height;
			Element.ItemsSource.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs ee) => {

				this.Control.ReloadData ();
				var ss = collectionView.CollectionViewLayout.CollectionViewContentSize;
				Element.HeightRequest = ss.Height;
			};
				

		}

		private GridDataSource _dataSource;

		private GridDataSource DataSource {
			get {
				return _dataSource ??
				(_dataSource =
						new GridDataSource (GetCell, RowsInSection, ItemSelected));
			}
		}

		private GridViewDelegate _gridViewDelegate;

	
		private GridViewDelegate GridViewDelegate {
			get {
				return _gridViewDelegate ??
				(_gridViewDelegate =
						new GridViewDelegate (ItemSelected));
			}
		}

		public int RowsInSection (UICollectionView collectionView, nint section)
		{
			return ((ICollection)Element.ItemsSource).Count;
		}


		public void ItemSelected (UICollectionView tableView, NSIndexPath indexPath)
		{
			var item = Element.ItemsSource.Cast<object> ().ElementAt (indexPath.Row);
			//Element.InvokeItemSelectedEvent(this, item);

		}

		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var item = Element.ItemsSource.Cast<object> ().ElementAt (indexPath.Row);
			var viewCellBinded = (Element.ItemTemplate.CreateContent () as ViewCell);
			if (viewCellBinded != null) {
				viewCellBinded.BindingContext = item;

				return GetCell (collectionView, viewCellBinded, indexPath);
			}

			return null;
		}

		void HandleClick (int pos)
		{
			if (Element.DragMode) {
				Element.DeleteLikeItem (pos);

			} else {
				Element.InsertLikeItem (Element.ItemsSource [pos]);
			}
		}

		protected virtual UICollectionViewCell GetCell (UICollectionView collectionView, ViewCell item, NSIndexPath indexPath)
		{
			var collectionCell = collectionView.DequeueReusableCell (new NSString (GridViewCell.Key), indexPath) as GridViewCell;

			if (collectionCell != null) {
				//
				collectionCell.Tag = indexPath.Row;
				collectionCell.OnClickItemEvent -= HandleClick;
				collectionCell.OnClickItemEvent += HandleClick;
				//
				collectionCell.ViewCell = item;

				return collectionCell as UICollectionViewCell;
			}

			return null;
		}
	}
}
