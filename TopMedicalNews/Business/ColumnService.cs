using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using TopMedicalNews;
using TinyIoC;



namespace TopMedicalNews
{
    public interface IColumnService
    {
         List<Column> GetColumnByCategory(int categoryId);
		 List<Column> GetLikeColumns();
    }
	public class ColumnService:IColumnService
	{
		public ColumnService ()
		{
		}
		public List<Column> GetColumnByCategory(int categoryId)
		{
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<Column> (r=>r.CategoryID==categoryId);
		}
		public List<Column> GetLikeColumns()
		{
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<Column> (r => r.Like == true);
		}
	}
}

