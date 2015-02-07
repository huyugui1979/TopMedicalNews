using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using TopMedicalNews;
using System.Linq;
using XLabs.Ioc;


namespace TopMedicalNews
{
    public interface IColumnService
    {
         List<Column> GetColumnByCategory(int categoryId);
		 List<Column> GetLikeColumns();
		void UpdateColumns(List<Column> columns);
	    List<Column> GetColumnByCategoryNotLike(int categoryId);
    }
	public class ColumnService:IColumnService
	{
		public ColumnService ()
		{
		}
		public List<Column> GetColumnByCategoryNotLike(int categoryId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r=>r.CategoryID==categoryId && r.Like==false);
		}
		public List<Column> GetColumnByCategory(int categoryId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r=>r.CategoryID==categoryId);
		}
		public List<Column> GetLikeColumns()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r => r.Like == true);

		}
		public void UpdateColumns(List<Column> columns)
		{
			Resolver.Resolve<ISQLiteClient> ().UpdateAllData<Column> (columns);
		}
	}
}

