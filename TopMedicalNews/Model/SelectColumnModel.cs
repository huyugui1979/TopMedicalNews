using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using System.Linq;
using System.Collections.ObjectModel;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using Xamarin.Forms;


namespace TopMedicalNews
{
	public class SelectColumn{
		public int CateId{ get; set; }
		public string Title{get;set;}
		public ObservableCollection<Column> Columns{get;set;}
		public SelectColumnModel Parent{get;set;}
	}
	public class SelectColumnModel:ViewModel
	{
		public ObservableCollection<Column>  LikeColumns{ get; set;}

		public List<SelectColumn> SelectColumns{get;set;}
		public SelectColumnModel ()
		{
			List<Column> columns = Resolver.Resolve<ILikeColumnService> ().GetLikeColumns ();
			//

			//
			LikeColumns = new ObservableCollection<Column> (columns);


			SelectColumns = new List<TopMedicalNews.SelectColumn> ();
			LoadSelectColumns ();
			//
		}

		void LoadSelectColumns()
		{
			List<Category> categorys = Resolver.Resolve<ICategoryService> ().GetAllCategory ();
			//
			SelectColumns.Clear ();
			foreach (var c in categorys) {
				var s=Resolver.Resolve<IColumnService> ().GetColumnByCategory (c.ID);
				var cs = LikeColumns.Intersect (s, Equality<Column>.CreateComparer (r => r.ID)).ToList();
				for (int i = 0; i < cs.Count; i++) {
					s.RemoveAll(cc=>cc.ID == cs[i].ID);
				}
				SelectColumns.Add (new SelectColumn{ Title = c.Title,Parent=this,CateId=c.ID, Columns =new ObservableCollection<Column>(s) });
			}

		}
		public void ChangeLikeColumnsOrder(int oldPos,int newPos)
		{
			Column c = LikeColumns [oldPos];
			LikeColumns.RemoveAt (oldPos);
			LikeColumns.Insert (newPos, c);
			//
			MessagingCenter.Send<object,Tuple<int,int>>(this,"ChangeLikeColumnsOrder",Tuple.Create<int,int>(oldPos,newPos));
			//
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
		}
		public void DeleteLikeColumn(int pos)
		{
			Column c = LikeColumns [pos];

			LikeColumns.RemoveAt (pos);
			MessagingCenter.Send<object,int>(this,"DeleteLikeColumn",pos);
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Add (c);
		}
	
		public void InsertLikeColumn(Column c)
		{
			//
			LikeColumns.Add (c);
			//
			MessagingCenter.Send<object,Column>(this,"InsertLikeColumn",c);
			//
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
			//
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Remove (c);
			//
		}
	}
}

