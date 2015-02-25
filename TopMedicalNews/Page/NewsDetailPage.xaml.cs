using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;
using System.Diagnostics;

namespace TopMedicalNews
{
	public partial class NewsDetailPage : MyPage
	{
		Stopwatch s=null;
		public NewsDetailPage ()
		{

			s = new Stopwatch ();
			s.Reset ();
			s.Restart ();
			InitializeComponent ();
			#if __IOS__
			CommentCell.TotalHeight = 0;
			#endif
			//
			var collectionImage = this.FindByName<Image>("collectionImage");

			collectionImage.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				var model = this.BindingContext as NewsDetailModel;
				model.AddNewsCollectionCmd.Execute(null);
			}));
			//

			//
		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

		}
		protected override void OnDisappearing ()
		{

			base.OnDisappearing ();

			var collectionImage = this.FindByName<Image>("collectionImage");
			collectionImage = null;
		}
		 
		protected override void OnAppearing ()
		{
			ScrollView sc;

			(this.BindingContext as NewsDetailModel).Init ();
			//
			#if __IOS__
			var commentListView = this.FindByName<ListView> ("commentListView");
			commentListView.HeightRequest = CommentCell.TotalHeight;
			#else

			#endif
		}

	
	}
}

