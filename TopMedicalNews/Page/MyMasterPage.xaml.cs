using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class MyMasterPage : ContentPage
	{
		public MyMasterPage ()
		{
			InitializeComponent ();
			var selectPictureImage = this.FindByName<Image> ("selectPictureImage");
			selectPictureImage.GestureRecognizers.Add(new TapGestureRecognizer(v=>{

				var model = this.BindingContext as MasterModel;
				model.SelectPicCommand.Execute(null);
				//
			}));
		}
	}
}

