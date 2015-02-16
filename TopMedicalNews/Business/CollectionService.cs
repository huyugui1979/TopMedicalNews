using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public interface ICollectionService
	{
		List<News> GetNewsCollectionByUser(int userId);
	    void AddNewsToCollection(int userId,int newsId);
		void DelNewsToCollection (int userId, int newsId);
	    bool ContainNewsInCollection(int newsId,int userId);
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
		public bool ContainNewsInCollection(int newsId,int userId)
		{
			bool b=  Resolver.Resolve<ISQLiteClient> ().Exist<Collection> (r => r.NewsID == newsId && r.UserID == userId);
			return b;
		}
		public void DelNewsToCollection (int userId, int newsId)
		{
			var obj = Resolver.Resolve<ISQLiteClient> ().GetData<Collection> (r => r.UserID == userId
			          && r.NewsID == newsId);
			Resolver.Resolve<ISQLiteClient> ().DeleteData<Collection> (obj.ID);
		}
		public void AddNewsToCollection(int userId,int NewsId)
		{
			Resolver.Resolve<ISQLiteClient> ().InsertData (new Collection{NewsID=NewsId,UserID=userId});
		}
	}
}

