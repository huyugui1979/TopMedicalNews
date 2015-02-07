using System;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using System.Linq.Expressions;

namespace TopMedicalNews
{
	public class MyEntry:Entry
	{
		public MyEntry ()
		{
		}
		public static readonly BindableProperty SourceProperty = BindableProperty.Create<MyEntry, ImageSource>((Expression<Func<MyEntry, ImageSource>>)(w => w.Source), (ImageSource)null, BindingMode.OneWay,
			(BindableProperty.ValidateValueDelegate<ImageSource>)null, 
			(BindableProperty.BindingPropertyChangedDelegate<ImageSource>)((bindable, oldvalue, newvalue) => ((VisualElement)bindable).ToString()), 
			(BindableProperty.BindingPropertyChangingDelegate<ImageSource>)null,
			(BindableProperty.CoerceValueDelegate<ImageSource>)null);

		[TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
	}
}

