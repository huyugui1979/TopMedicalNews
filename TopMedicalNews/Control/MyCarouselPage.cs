using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using XLabs.Forms.Mvvm;
using System.Linq;
using TopMedicalNews.Model;
using System.Collections.ObjectModel;


namespace TopMedicalNews
{
	public class MyCarouselPage:CarouselPage
	{
		public MyCarouselPage ()
		{

		}

		public static readonly BindableProperty ColumnsProperty =
			BindableProperty.Create<MyCarouselPage,List<Column>> (
				view => view.Columns, null, propertyChanged: (bindableObject, oldValue, newValue) => {
					(bindableObject as MyCarouselPage).OnPropertyChanged ("Columns");
			});

		public List<Column>  Columns {
			get {
				return (List<Column>)GetValue (ColumnsProperty);
			}
			set {
				SetValue (ColumnsProperty, value);
			}
		}
		//
	}
}

