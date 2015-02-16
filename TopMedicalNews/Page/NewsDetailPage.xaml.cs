using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace TopMedicalNews
{
	public partial class NewsDetailPage : MyPage
	{
		public NewsDetailPage ()
		{
			InitializeComponent ();
			CommentCell.TotalHeight = 0;
			//
			var collectionImage = this.FindByName<Image>("collectionImage");

			collectionImage.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				var model = this.BindingContext as NewsDetailModel;
				model.AddNewsCollectionCmd.Execute(null);
			}));
		
			//
		}
		protected override void OnAppearing ()
		{

			base.OnAppearing ();
			var commentListView = this.FindByName<ListView> ("commentListView");
			commentListView.HeightRequest = CommentCell.TotalHeight;
		}
	
	}
}

