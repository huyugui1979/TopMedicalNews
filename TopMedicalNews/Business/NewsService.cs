using System;
using System.Collections.Generic;
using TopMedicalNews;
using TopMedicalNews.Model;
using Refractored.Xam.Settings.Abstractions;
using XLabs.Ioc;

namespace TopMedicalNews
{
    public interface INewsService
    {
         List<News> GetNewsByColumn(int columnId);
		 List<News> GetFocusNews (int categoryId);
		 List<News> GetSelectNews(int columnId);
		 List<News> GetNewsByTheme (int newsId);
		 News  GetNewById(int Id);

    }
	public class NewsService:INewsService
	{
		public NewsService ()
		{

		}
//		public async List<News> DownloadNews(int columnId)
//		{
//			//
//
//			//
//		}

		public List<News> GetCollectonNews(int userId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> ("select * from Collection as a join News as b on b.ID=a.NewsID where a.UserId={0}",userId);
		}
		public List<News> GetNewsByColumn(int columnId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r=>r.ColumnId==columnId);

		}
		public News  GetNewById(int Id)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetData<News> (r => r.ID == Id);
		}
		public List<News> GetFocusNews(int columnId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r=>r.ColumnId==columnId && r.Focus==true);
		}
		public List<News> GetSelectNews(int columnId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r => r.ColumnId == columnId && r.ThemeID == 0);
		}
		public List<News> GetNewsByTheme (int newsId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r => r.ThemeID == newsId);
		}

	}
}

