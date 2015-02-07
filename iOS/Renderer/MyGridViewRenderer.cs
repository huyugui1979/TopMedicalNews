using System;
using Xamarin.Forms.Platform.iOS;
using System.Collections.Specialized;
using UIKit;
using CoreGraphics;
using System.ComponentModel;
using Foundation;
using Xamarin.Forms;
using CoreGraphics;
using System.Collections;
using System.Linq;

[assembly:ExportRenderer(typeof(TopMedicalNews.MyGridView), typeof(TopMedicalNews.iOS.MyGridViewRenderer))]
namespace TopMedicalNews.iOS
{
	public class MyGridViewRenderer: ViewRenderer<MyGridView,UICollectionView>
	{
		public MyGridViewRenderer ()
		{
		}
		/// Initializes a new instance of the <see cref="GridViewRenderer"/> class.
		/// </summary>
		GridCollectionView collectionView;
		protected override void OnElementChanged (ElementChangedEventArgs<MyGridView> e)
		{
			base.OnElementChanged (e);

			collectionView = new GridCollectionView (this);
			collectionView.CollectionView.AllowsMultipleSelection = false;
			collectionView.CollectionView.ContentInset = new UIEdgeInsets ((float)10, (float)10, (float)10, (float)10);

			collectionView.CollectionView.BackgroundColor = Element.BackgroundColor.ToUIColor ();
			collectionView.ItemSize = new CGSize ((float)Element.ItemWidth, (float)Element.ItemHeight);
			collectionView.RowSpacing = Element.RowSpacing;
			collectionView.ColumnSpacing = Element.ColumnSpacing;

			SetNativeControl (collectionView.CollectionView);

		}
	}
}

