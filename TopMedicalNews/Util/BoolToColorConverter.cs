using System;
using Xamarin.Forms;
using System.Globalization;

namespace TopMedicalNews
{
	public class BoolToColorConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			bool v = (bool)value;
			if (v == false) {
				return Color.Black;
			} else {
				return Color.Red;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}