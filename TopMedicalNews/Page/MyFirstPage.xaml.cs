using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Behaviors;
using System.Threading.Tasks;
using XLabs.Forms.Controls;
using TopMedicalNews.Model;
using System.Linq;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;


namespace TopMedicalNews
{
	public partial class MyFirstPage : MyPage
	{
		public MyFirstPage ()
		{

			InitializeComponent ();

		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			//SelectedColumnIndex="{Binding SelectedColumnIndex}"
			columnView.SetBinding (ColumnView.SelectedColumnIndexProperty, "SelectedColumnIndex");
		}

	}
}

