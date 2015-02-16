using System;
using Xamarin.Forms;
using System.Globalization;

namespace TopMedicalNews
{
	public class TypeToVisibleConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			int v = (int)value;
			if (v == 2) {
				return true;
			} else {
				return false;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}

