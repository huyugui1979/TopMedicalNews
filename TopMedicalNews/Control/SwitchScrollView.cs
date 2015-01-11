using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class SwitchScrollView:ScrollView
	{
		public SwitchScrollView ()
		{

		}
		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create<SwitchScrollView, int> (
				carousel => carousel.SelectedIndex,
				0,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) => {

				}
			);

		public int SelectedIndex {
			get {
				return (int)GetValue (SelectedIndexProperty);
			}
			set {
				SetValue (SelectedIndexProperty, value);
			}
		}
	}
}

