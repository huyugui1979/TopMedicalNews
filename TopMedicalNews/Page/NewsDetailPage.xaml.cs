using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;
using System.Diagnostics;

namespace TopMedicalNews
{
	public partial class NewsDetailPage : MyPage
	{

		public NewsDetailPage ()
		{
		
			InitializeComponent ();
			//增
			InputComments.Completed += (object sender, EventArgs e) => {
				(this .BindingContext as NewsDetailModel).AddCommentCommand.Execute(null);
			};
		}


	}
}

