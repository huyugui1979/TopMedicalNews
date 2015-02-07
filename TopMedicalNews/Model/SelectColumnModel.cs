using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using System.Linq;
using System.Collections.ObjectModel;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;


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
			LikeColumns = new ObservableCollection<Column> (Resolver.Resolve<IColumnService> ().GetLikeColumns ());

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
				var s=Resolver.Resolve<IColumnService> ().GetColumnByCategoryNotLike (c.ID);
				SelectColumns.Add (new SelectColumn{ Title = c.Title,Parent=this,CateId=c.ID, Columns =new ObservableCollection<Column>(s) });
			}
		}
		public void ChangeLikeColumnsOrder(int oldPos,int newPos)
		{
			Column c = LikeColumns [oldPos];
			LikeColumns.RemoveAt (oldPos);
			LikeColumns.Insert (newPos, c);
			for (int i = 0; i < LikeColumns.Count; i++)
				LikeColumns[i].LikeOrder = i;
			Resolver.Resolve<IColumnService> ().UpdateColumns (LikeColumns.ToList());
		}
		public void DeleteLikeColumn(int pos)
		{
			Column c = LikeColumns [pos];
			c.Like = false;
			LikeColumns.RemoveAt (pos);
			for (int i = 0; i < LikeColumns.Count; i++)
				LikeColumns[i].LikeOrder = i;
			Resolver.Resolve<IColumnService> ().UpdateColumns (new List<Column>{ c });
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Add (c);
		}
	
		public void InsertLikeColumn(Column c)
		{
			//
			c.Like = true;
			LikeColumns.Add (c);
			for (int i = 0; i < LikeColumns.Count; i++)
				LikeColumns[i].LikeOrder = i;
			Resolver.Resolve<IColumnService> ().UpdateColumns (LikeColumns.ToList());
			//
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Remove (c);
			//
		}
	}
}

