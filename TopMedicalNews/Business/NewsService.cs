using System;
using System.Collections.Generic;
using TopMedicalNews;
using TopMedicalNews.Model;
using TinyIoC;
using Refractored.Xam.Settings.Abstractions;


namespace TopMedicalNews
{
    public interface INewsService
    {
         List<News> GetNewsByColumn(int columnId);
		 List<News> GetFocusNews (int categoryId);
		 List<News> GetSelectNews(int columnId);
    }
	public class NewsService:INewsService
	{
		public NewsService ()
		{

		}
		public List<News> GetNewsByColumn(int columnId)
		{
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<News> (r=>r.ColumnId==columnId);

		}

		public List<News> GetFocusNews(int columnId)
		{
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<News> (r=>r.ColumnId==columnId && r.Focus==true);
		}
		public List<News> GetSelectNews(int columnId)
		{
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<News> (r => r.ColumnId == columnId);
		}

	}
}

