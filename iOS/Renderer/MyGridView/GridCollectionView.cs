//using UIKit;
//using Foundation;
//using CoreGraphics;
//using ObjCRuntime;
//using System.Collections;
//using System.Linq;
//using Xamarin.Forms;
//using System;
//
//namespace TopMedicalNews.iOS
//{
//	/// <summary>
//	/// Class GridCollectionView.
//	/// </summary>
//	public class GridCollectionView : UICollectionViewController
//	{
//	
////		[Export ("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
////		public bool ShouldRecognizeSimultaneously (UIKit.UIGestureRecognizer gestureRecognizer, UIKit.UIGestureRecognizer otherGestureRecognizer)
////		{
////
////			return true;
////		}
//		/// <summary>
//		/// Gets or sets a value indicating whether [selection enable].
//		/// </summary>
//		/// <value><c>true</c> if [selection enable]; otherwise, <c>false</c>.</value>
////		public bool SelectionEnable {
////			get;
////			set;
////		}
////		public double RowSpacing {
////			get { 
////				return (double)(this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing;
////			}
////			set {
////				(this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing = (float)value;
////			}
////		}
////
//		/// <summary>
//		/// Gets or sets the column spacing.
//		/// </summary>
//		/// <value>The column spacing.</value>
////		public double ColumnSpacing {
////			get { 
////				return (double)(this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing;
////			}
////			set {
////				(this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing = (float)value;
////			}
////		}
//
//		/// <summary>
//		/// Gets or sets the size of the item.
//		/// </summary>
//		/// <value>The size of the item.</value>
////		public CGSize ItemSize {
////			get { 
////				return (this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).ItemSize;
////			}
////			set {
////				(this.CollectionView.CollectionViewLayout as UICollectionViewFlowLayout).ItemSize = value;
////			}
////		}
//		public GridCollectionView (/*MyGridViewRenderer render*/) : base ( new UICollectionViewFlowLayout () { })
//		{
//			//_render = render;
////			this.CollectionView.AutoresizingMask = UIViewAutoresizing.All;
////			this.CollectionView.ContentMode = UIViewContentMode.ScaleToFill;
////			this.CollectionView.RegisterClassForCell (typeof(GridViewCell), new NSString (GridViewCell.Key));
////			longGesture = new UILongPressGestureRecognizer(this, new Selector("HandleLongPressGesture:"));
////			panGesture = new  UIPanGestureRecognizer (this, new Selector ("HandlePanGesture:"));
////			panGesture.WeakDelegate = this;
////			longGesture.WeakDelegate = this;
////			this.CollectionView.AddGestureRecognizer(panGesture);
////			this.CollectionView.AddGestureRecognizer(longGesture);
//
//
//		}
////		UIImageView mockCell;
////		nfloat dx = 0;
////		nfloat dy = 0;
////		UIPanGestureRecognizer panGesture;
////		UILongPressGestureRecognizer longGesture;
////		public UIImage FromCell(GridViewCell cell)
////		{
////			CGRect canvasRect = cell.Bounds;
////			UIGraphics.BeginImageContextWithOptions (canvasRect.Size, false, 0.0f);
////
////			CGContext ctx = UIGraphics.GetCurrentContext ();
////			UIColor.White.SetColor ();
////			ctx.FillRect (canvasRect);
////			cell.Layer.RenderInContext (ctx);
////
////			UIImage newImage = UIGraphics.GetImageFromCurrentImageContext ();
////			UIGraphics.EndImageContext ();
////			return newImage;
////		}
//		//MyGridViewRenderer _render;
//
////		public override System.nint GetItemsCount (UICollectionView collectionView, System.nint section)
////		{
////			return 0;
////			//return   ((ICollection)_render.Element.ItemsSource).Count;
////		}
//
//
////		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
////		{
//
////			var item = _render.Element.ItemsSource.Cast<object> ().ElementAt (indexPath.Row);
////		   
////			var viewCellBinded = (_render.Element.ItemTemplate.CreateContent () as ViewCell);
////			if (viewCellBinded != null)
////			{
////
////				viewCellBinded.BindingContext = item;
////////		UIImageView mockCell;
////		nfloat dx = 0;
////		nfloat dy = 0;
////		UIPanGestureRecognizer panGesture;
////		UILongPressGestureRecognizer longGesture;
////				var collectionCell = collectionView.DequeueReusableCell (new NSString (GridViewCell.Key), indexPath) as GridViewCell;
////
////				if (collectionCell != null)
////				{
////				
////					collectionCell.ViewCell = viewCellBinded;
////
////					return collectionCell as UICollectionViewCell;
////				}
////				return null;
////			}
////			return null;	
////		}
//	
////		[Export("HandlePanGesture:")]
////		void HandlePanGestu////		UIImageView mockCell;
////		nfloat dx = 0;
////		nfloat dy = 0;
////		UIPanGestureRecognizer panGesture;
////		UILongPressGestureRecognizer longGesture;re(UIPanGestureRecognizer sender)
////		{
////
////			if (sender.State == UIGestureRecognizerState.Changed) {
////				if (mockCell != null) {
////					var p0 = sender.LocationInView (this.CollectionView);
////					NSIndexPath newPath = this.CollectionView.IndexPathForItemAtPoint (p0);
////					NSIndexPath pre = initPath;
////					if (newPath != null  && newPath.Equals(pre)==false   ) {
////						//
//////						object o = (this._render.Element.ItemsSource as IList) [initPath.Row];
//////
//////						(this._render.Element.ItemsSource as IList).RemoveAt (initPath.Row);
//////
//////						(this._render.Element.ItemsSource as IList).Insert(pre.Row,o);
////						initPath =newPath;
////					
////					   this.CollectionView.PerformBatchUpdates (() => {
////
////							this.CollectionView.MoveItem(pre,newPath);
////			
////						}, new UICompletionHandler ((b) => {
////							if(b == true)
////							{
////
////							}
////
////						}));
////
////					}
////					if (dx == 0)
////						dx = p0.X - mockCell.Center.X;
////					//
////					if (dy == 0)
////						dy////		UIImageView mockCell;
////		nfloat dx = 0;
////		nfloat dy = 0;
////		UIPanGestureRecognizer panGesture;
////		UILongPressGestureRecognizer longGesture; = p0.Y - mockCell.Center.Y;
////		
////					var p1 = new CGPoint (p0.X - dx, p0.Y - dy);
////					mockCell.Center = p1;
////
////				}
////			}
////			else if (sender.State == UIGestureRecognizerState.Ended)
////			{
////				//
////				if (mockCell != null) {
////					mockCell.RemoveFromSuperview ();
////
////					this.CollectionView.CellForItem (initPath).Hidden = false;
////				}
////				//
////			}
////
////		}
////		NSIndexPath initPath;
////		[Export("HandleLongPressGesture:")]
////		void HandleLongPressGesture(UILongPressGestureRecognizer sender)
////		{
////
////			switch (sender.State)
////			{
////			case UIGestureRecognizerState.Began:
////				var initialPinchPoint = sender.LocationInView (this.CollectionView);
////				initPath = this.CollectionView.IndexPathForItemAtPoint (initialPinchPoint);
////				var cellx = (GridViewCell)this.CollectionView.CellForItem (initPath);
////
////				if (cellx != null) {
////					//  CollectionView.DeleteItems(new NSIndexPath[] { tappedCellPath });
////					mockCell = new UIImageView (new CGRect (initialPinchPoint.X, initialPinchPoint.Y, cellx.Frame.Width, cellx.Frame.Height));
////					mockCell.Image = FromCell (cellx);
////
////					cellx.Highlighted = false;
////
////					if (cellx != null) {
////						cellx.Hidden = true;// ();
////					}
////			
////					mockCell.Transform = CGAffineTransform.MakeScale (1.1f, 1.1f);
////
////
////					mockCell.Center = cellx.Center;
////
////					this.CollectionView.AddSubview (mockCell);
////
////				}
////				// new UIAlertView(cellx.tableName.ToString(), "LP", null, "OK", null).Show();
////				break;
////
////			case UIGestureRecognizerState.Cancelled:
////			
////				break;
////			
////			case UIGestureRecognizerState.Ended:
////				//  new UIAlertView("GestureState", "Ended", null, "OK", null).Show();
////				//m	[Export("HandlePanGesture:")]
////				void HandlePanGesture(UIPanGestureRecognizer sender)
////				{
////
////					if (sender.State == UIGestureRecognizerState.Changed) {
////						if (mockCell != null) {
////							var p0 = sender.LocationInView (this.CollectionView);
////							NSIndexPath newPath = this.CollectionView.IndexPathForItemAtPoint (p0);
////							NSIndexPath pre = initPath;
////							ockCell.RemoveFromSuperview ();
////				break;
////			}
////		}
//	}
//}
//
using ObjCRuntime;
using CoreGraphics;
using Foundation;
using UIKit;
using System;

namespace TopMedicalNews.iOS
{

	public class GridCollectionView : UICollectionView
	{

		[Export ("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
		public bool ShouldRecognizeSimultaneously (UIKit.UIGestureRecognizer gestureRecognizer, UIKit.UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}

		public bool SelectionEnable {
			get;
			set;
		}

		UIImageView mockCell;
		nfloat dx = 0;
		nfloat dy = 0;
		UIPanGestureRecognizer panGesture;
		UILongPressGestureRecognizer longGesture;
		UITapGestureRecognizer tapGesture;
		public UIImage FromCell (GridViewCell cell)
		{
			CGRect canvasRect = cell.Bounds;
			UIGraphics.BeginImageContextWithOptions (canvasRect.Size, false, 0.0f);
		
			CGContext ctx = UIGraphics.GetCurrentContext ();
			UIColor.White.SetColor ();
			ctx.FillRect (canvasRect);
			cell.Layer.RenderInContext (ctx);
		
			UIImage newImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return newImage;
		}

		[Export ("HandlePanGesture:")]
		void HandlePanGesture (UIPanGestureRecognizer sender)
		{
			if (sender.State == UIGestureRecognizerState.Changed) {
				if (mockCell != null) {
					var p0 = sender.LocationInView (this);
					NSIndexPath newPath = IndexPathForItemAtPoint (p0);
					NSIndexPath pre = initPath;
					if (newPath != null && newPath.Equals (pre) == false) {
						//
						//						object o = (this._render.Element.ItemsSource as IList) [initPath.Row];
						//
						//						(this._render.Element.ItemsSource as IList).RemoveAt (initPath.Row);
						//
						//						(this._render.Element.ItemsSource as IList).Insert(pre.Row,o);
						initPath = newPath;
						this.PerformBatchUpdates (() => {
							MoveItem (pre, newPath);
					
						}, new UICompletionHandler ((b) => {
						}));
					}
					if (dx == 0)
						dx = p0.X - mockCell.Center.X;
					if (dy == 0)
						dy = p0.Y - mockCell.Center.Y;
					var p1 = new CGPoint (p0.X - dx, p0.Y - dy);
					mockCell.Center = p1;
				}
			} else if (sender.State == UIGestureRecognizerState.Ended) {
				//
				if (mockCell != null) {
					mockCell.RemoveFromSuperview ();
					CellForItem (initPath).Hidden = false;
				}
			}
		
		}
		public GridCollectionView () : this (default(CGRect))
		{
		}
		NSIndexPath initPath;


		[Export ("HandleLongPressGesture:")]
		void HandleLongPressGesture (UILongPressGestureRecognizer sender)
		{
			switch (sender.State) {
			case UIGestureRecognizerState.Began:
				var initialPinchPoint = sender.LocationInView (this);
				initPath = IndexPathForItemAtPoint (initialPinchPoint);
				var cellx = (GridViewCell)CellForItem (initPath);
		
				if (cellx != null) {
					//  CollectionView.DeleteItems(new NSIndexPath[] { tappedCellPath });
					mockCell = new UIImageView (new CGRect (initialPinchPoint.X, initialPinchPoint.Y, cellx.Frame.Width, cellx.Frame.Height));
					mockCell.Image = FromCell (cellx);
					cellx.Highlighted = false;
					if (cellx != null) {
						cellx.Hidden = true;// ();
					}
					mockCell.Transform = CGAffineTransform.MakeScale (1.1f, 1.1f);
					mockCell.Center = cellx.Center;
					AddSubview (mockCell);
				}
				break;
			case UIGestureRecognizerState.Cancelled:
					
				break;
					
			case UIGestureRecognizerState.Ended:

				if (sender.State == UIGestureRecognizerState.Changed) {
					if (mockCell != null) {
						var p0 = sender.LocationInView (this);
						NSIndexPath newPath = IndexPathForItemAtPoint (p0);
						NSIndexPath pre = initPath;
						mockCell.RemoveFromSuperview ();
					}	
				}
				break;
			}
		}
		public GridCollectionView (CGRect frm) : base (frm, new UICollectionViewFlowLayout () { })
		{
			AutoresizingMask = UIViewAutoresizing.All;
			ContentMode = UIViewContentMode.ScaleToFill;
			RegisterClassForCell (typeof(GridViewCell), new NSString (GridViewCell.Key));
			longGesture = new UILongPressGestureRecognizer (this, new Selector ("HandleLongPressGesture:"));
			panGesture = new  UIPanGestureRecognizer (this, new Selector ("HandlePanGesture:"));
			panGesture.WeakDelegate = this;
			longGesture.WeakDelegate = this;

			AddGestureRecognizer (panGesture);
			AddGestureRecognizer (longGesture);
		}

//		public override UICollectionViewCell CellForItem (NSIndexPath indexPath)
//		{
//			return base.CellForItem (indexPath);
//		}
		//
		public override void Draw (CGRect rect)
		{
			CollectionViewLayout.InvalidateLayout ();

			base.Draw (rect);
		}

	
		public double RowSpacing {
			get { 
				return (double)(CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing;
			}
			set {
				(CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing = (float)value;
			}
		}

	
		public double ColumnSpacing {
			get { 
				return (double)(CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing;
			}
			set {
				(CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing = (float)value;
			}
		}
		public CGSize ItemSize {
			get { 
				return (CollectionViewLayout as UICollectionViewFlowLayout).ItemSize;
			}
			set {
				(CollectionViewLayout as UICollectionViewFlowLayout).ItemSize = value;
			}
		}
	}
}

