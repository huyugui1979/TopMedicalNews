using System;
using Xamarin.Forms;
using System.Globalization;

namespace TopMedicalNews
{
	public class StringToImageSourceConverter : IValueConverter
	{ 
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			return ImageSource.FromResource(value as string);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

