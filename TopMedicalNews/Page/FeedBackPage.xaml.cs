using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace TopMedicalNews
{
	public partial class FeedBackPage : ContentPage
	{
		public FeedBackPage ()
		{
			InitializeComponent ();
			var editor = this.FindByName<ExtendedEditor> ("editor");
			var address = this.FindByName<ExtendedEntry> ("address");
			editor.Focused += (object sender, FocusEventArgs e) => {
				Editor ed = (Editor)sender;
				ed.Text="";
			
			};
			address.Focused += (object sender, FocusEventArgs e) => {
				Entry ed = (Entry)sender;
				ed.Text="";
			};
		}
	}
}

