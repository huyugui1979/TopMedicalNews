using System;
using Xamarin.Forms;
using System.Globalization;

namespace TopMedicalNews
{
	public class DateTimeFormaterConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			DateTime dt = (DateTime)value;
			return string.Format ("{0}-{1:D2}-{2:D2}", dt.Year, dt.Month, dt.Day);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

