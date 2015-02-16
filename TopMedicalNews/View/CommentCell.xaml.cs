using System;
using System.Collections.Generic;
using Xamarin.Forms;


#if __IOS__
using System.Drawing;

using Foundation;
using CoreGraphics;
using UIKit;
#endif
namespace TopMedicalNews
{
	public partial class CommentCellXaml : ContentView
	{
		public CommentCellXaml ()
		{
			InitializeComponent ();

		}

	}

	public  class CommentCell : ViewCell
	{
		public CommentCell ()
		{
			View = new CommentCellXaml ();

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
		}

		public static double TotalHeight = 0;

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			#if __IOS__

			if (Device.OS == TargetPlatform.iOS) {
				var text = (string)(BindingContext as CommentData).Content;

				NSString str = new NSString (text);

				UIStringAttributes attribs = new UIStringAttributes { Font =UIFont.BoldSystemFontOfSize (14f) };
				var ss = new NSMutableAttributedString(str);
				ss.AddAttribute(UIStringAttributeKey.Font, UIFont.PreferredBody, new NSRange(0, text.Length));
					var ctxt = new NSStringDrawingContext();
				var boundingRect = str.GetBoundingRect(new SizeF((float)App.ScreenWidth-20, float.MaxValue),
					NSStringDrawingOptions.UsesFontLeading | NSStringDrawingOptions.UsesLineFragmentOrigin,attribs, ctxt);
				Height = boundingRect.Height+38;
				TotalHeight += Height;
			}
			#endif


		}
	}
}

