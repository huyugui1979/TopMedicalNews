using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public interface ICollectionService
	{
		List<News> GetNewsCollectionByUser(int userId);
	    void AddNewsToCollection(int userId,int NewsId);
	}
	public class CollectionService:ICollectionService
	{
		public CollectionService ()
		{
		}
		public List<News> GetNewsCollectionByUser(int userId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News>("select * from collection as a join news as b on a.newsid =b.id");
		}
		public void AddNewsToCollection(int userId,int NewsId)
		{
			Resolver.Resolve<ISQLiteClient> ().InsertData (new Collection{NewsID=NewsId,UserID=userId });
		}
	}
}

