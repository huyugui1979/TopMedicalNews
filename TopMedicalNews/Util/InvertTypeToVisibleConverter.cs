using System;
using System.Globalization;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class InvertTypeToVisibleConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			int v = (int)value;
			if (v == 1) {
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

