using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using CoreGraphics;
using CoreGraphics;


namespace TopMedicalNews.iOS
{

	public class GridViewCell : UICollectionViewCell
	{

		public const string Key = "GridViewCell";

		private ViewCell _viewCell;
	
		private UIView _view;
		public ViewCell ViewCell {
			get {
				return _viewCell;
			}
			set {
				if (_viewCell == value) {
					return;
				}
				UpdateCell (value);
			}
		}
		[Export ("initWithFrame:")]
		public GridViewCell (CGRect frame) : base (frame)
		{
			// SelectedBackgroundView = new GridItemSelectedViewOverlay (frame);
			// this.BringSubviewToFront (SelectedBackgroundView);

		}
		public delegate void ClickItemEvent(int pos);
		public event ClickItemEvent OnClickItemEvent;
	
		private void UpdateCell (ViewCell cell)
		{
			if (_viewCell != null) {
			//this.viewCell.SendDisappearing ();
				_viewCell.PropertyChanged -= new PropertyChangedEventHandler (HandlePropertyChanged);
			}
			_viewCell = cell;
			_viewCell.PropertyChanged += new PropertyChangedEventHandler (HandlePropertyChanged);
			//this.viewCell.SendAppearing ();
			UpdateView ();
		}
		private void HandlePropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			UpdateView ();
		}

		private void UpdateView ()
		{
			if (_view != null) {
				_view.RemoveFromSuperview ();
			}
			_view = RendererFactory.GetRenderer (_viewCell.View).NativeView;
			_view.AutoresizingMask = UIViewAutoresizing.All;
			_view.ContentMode = UIViewContentMode.ScaleToFill;
			UIButton btton = (_view as ButtonRenderer).Control;
			btton.TouchUpInside += (object sender, System.EventArgs e) => {

				OnClickItemEvent((int)this.Tag);

			};
			btton.Layer.BorderWidth = 1;
			btton.Layer.CornerRadius = 5;
			btton.Layer.BorderColor = UIColor.Gray.CGColor;
			AddSubview (_view);
		
		}
//	
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CGRect frame = ContentView.Frame;
			frame.X = (Bounds.Width - frame.Width) / 2;
			frame.Y = (Bounds.Height - frame.Height) / 2;
			ViewCell.View.Layout (frame.ToRectangle ());
		
			_view.Frame = frame;
		
		}
	}


}

