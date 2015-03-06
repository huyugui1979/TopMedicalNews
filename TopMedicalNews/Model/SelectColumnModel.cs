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
			LikeColumns = new ObservableCollection<Column> (Resolver.Resolve<ILikeColumnService>().GetLikeColumns ());

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
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
		}
		public void DeleteLikeColumn(int pos)
		{
			Column c = LikeColumns [pos];

			LikeColumns.RemoveAt (pos);
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Add (c);
		}
	
		public void InsertLikeColumn(Column c)
		{
			//
			LikeColumns.Add (c);
			Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (LikeColumns.ToList());
			//
			SelectColumns.Find (r => r.CateId == c.CategoryID).Columns.Remove (c);
			//
		}
	}
}

