using System;
using Xamarin.Forms;
using System.Collections;

namespace TopMedicalNews
{
	public class MyGridView:View
	{
		public MyGridView ()
		{

		}
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create ("ItemsSource",
			typeof(IEnumerable), 
			typeof(MyGridView), 
			null, BindingMode.OneWay, 
			null, null, null, null);

		public IEnumerable ItemsSource {
			get {
				return (IEnumerable)base.GetValue (MyGridView.ItemsSourceProperty);
			}
			set {
				base.SetValue (MyGridView.ItemsSourceProperty, value);
			}
		}

		public DataTemplate ItemTemplate {
			get;
			set;
		}
		public double RowSpacing {
			get;
			set;
		}


		public double ColumnSpacing {
			get;
			set;
		}
		public double ItemWidth {
			get;
			set;
		}
			
		public double ItemHeight {
			get;
			set;
		}
		//
		public void DeleteItem(object Item)
		{
			(this.ItemsSource as IList).Remove (Item);

		}
		//
	}
}

