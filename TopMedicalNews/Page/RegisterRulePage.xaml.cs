using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TopMedicalNews
{
	public partial class RegisterRulePage : MyPage
	{
		public RegisterRulePage ()
		{
			InitializeComponent ();
			//

		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var htmlSource = new HtmlWebViewSource ();
			htmlSource.Html = (this.BindingContext as RegisterRuleModel).Rule;
				webView.Source = htmlSource;
		}
	}
}

