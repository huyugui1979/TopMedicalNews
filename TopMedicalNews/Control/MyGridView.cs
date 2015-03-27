using System;
using Xamarin.Forms;
using System.Collections;
using TopMedicalNews.Model;
using System.Collections.ObjectModel;

namespace TopMedicalNews
{
	public class MyGridView:View
	{
		public MyGridView ()
		{

			if (Device.OS == TargetPlatform.iOS)  {
				ItemWidth = 60;
				ItemHeight = 30;
			} else {
				ItemWidth = 80;
				ItemHeight = 40;
			}
		}

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<MyGridView,ObservableCollection<Column>>(
			v=>v.ItemsSource,null
			);

		public ObservableCollection<Column> ItemsSource {
			get {
				return (ObservableCollection<Column> )base.GetValue (MyGridView.ItemsSourceProperty);
			}
			set {
				base.SetValue (MyGridView.ItemsSourceProperty, value);

			}
		}
		public bool DragMode{
			get;set;
		}

		public void SwapItem(int oldPos,int newPos)
		{
			(this.BindingContext as SelectColumnModel).ChangeLikeColumnsOrder (oldPos, newPos);
		}
		public void DeleteLikeItem(int pos)
		{
			
			(this.BindingContext as SelectColumnModel).DeleteLikeColumn (pos);
		}
		public void InsertLikeItem(Column c)
		{
			(this.BindingContext as SelectColumn).Parent.InsertLikeColumn(c );
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
		public bool SelectionEnabled {
			get;
			set;
		}
		//
		//
	}
}

