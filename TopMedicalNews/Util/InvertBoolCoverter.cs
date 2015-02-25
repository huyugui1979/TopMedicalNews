using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class InvertBoolCoverter:IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion

		public InvertBoolCoverter ()
		{
		}
	}
}

