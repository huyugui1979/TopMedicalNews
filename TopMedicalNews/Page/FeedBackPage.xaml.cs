using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class FeedBackPage : MyPage
	{
		public FeedBackPage ()
		{
			InitializeComponent ();

		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			FeedBackModel vm = BindingContext as FeedBackModel;
			if (vm != null) {

				foreach (var type in vm.FeedBackTypeValues)
				{
					typePicker.Items.Add(type);
				}
			}
		}
	}
}

