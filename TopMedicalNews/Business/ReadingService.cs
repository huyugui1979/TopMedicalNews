using System;
using XLabs.Ioc;
using TopMedicalNews.Model;
using System.Collections.Generic;

namespace TopMedicalNews
{
	public interface IReadingService
	{
		void AddReading(int userId,int NewsId);
		List<News> GetMyReading (int userId);
	}
	public class ReadingService:IReadingService
	{
		public ReadingService ()
		{
		}
		public void AddReading(int userId,int NewsId)
		{
			//
			Resolver.Resolve<ISQLiteClient> ().InsertData (new Reading{NewsID=NewsId,UserID=userId});
			//
		}
		public List<News> GetMyReading(int userId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> ("select * from Reading as a join News as b on a.NewsId=b.Id where a.UserID=?", new object[]{userId});

		}
	}
}

