using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class SetColumnPage : MyPage
	{
		public SetColumnPage ()
		{
			InitializeComponent ();
		
		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			StackLayout layout = this.FindByName<StackLayout> ("stack");
			var model = this.BindingContext as SelectColumnModel;
			foreach (var c in model.SelectColumns) {
				MyGridChildView view = new MyGridChildView ();
				view.BindingContext = c;
				layout.Children.Add (view);
			}
		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Send<object> (this, "GetLikeColumns");
			base.OnDisappearing ();
		}
	}
}

