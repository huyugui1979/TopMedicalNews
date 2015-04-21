using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyFormsLibCore.Control;

namespace TopMedicalNews
{
	public partial class SetttingPage : MyPage
	{
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			radio.SetBinding (BindableRadioGroup.SelectedIndexProperty, "SelectFont");
		}

		public SetttingPage ()
		{
			InitializeComponent ();

			//
			var cacheNum = this.FindByName<StackLayout> ("cacheNum");
			cacheNum.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				(this.BindingContext as SettingModel).ClearCache.Execute(null);
			}));

			//
			var verInfo = this.FindByName<StackLayout> ("verInfo");
			verInfo.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				(this.BindingContext as SettingModel).UpdateSystem.Execute(null);
			}));
			//
			var feedBack = this.FindByName<StackLayout> ("feedBack");
			feedBack.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				(this.BindingContext as SettingModel).FeedBackCmd.Execute(null);
			}));
			//
			var aboutUs = this.FindByName<StackLayout> ("aboutUs");
			aboutUs.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
				(this.BindingContext as SettingModel).AboutUsCmd.Execute(null);
			}));
		}

	}
}

