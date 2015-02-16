using System;
using Xamarin.Forms;
using System.Globalization;

namespace TopMedicalNews
{
	public class BoolToImageConvert:IValueConverter
	{
		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		public BoolToImageConvert ()
		{

		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool b = (bool)value;
			if (b == true) {
				return "收藏_pressed_btn";
			}
			else
				return "收藏_btn_";
		}
	}
}

