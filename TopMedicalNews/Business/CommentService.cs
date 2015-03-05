using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public class CommentData
	{
		public string UserName{ get; set; }

		public string ImageUri{ get; set; }

		public DateTime PublishTime{ get; set; }

		public string Content{ get; set; }
		public int    NewsID{get;set;}
	}
	public interface ICommentService
	{
		 List<CommentData> GetCommentsByNewsId(int NewsId);
		 List<CommentData> GetMyComments (int userId);
	}
	public class CommentService:ICommentService
	{
		public CommentService ()
		{

		}
		public List<CommentData> GetMyComments(int userId)
		{
			//
			var list = Resolver.Resolve<ISQLiteClient> ().GetAllData<CommentData>("select * from comment as a join user as b on a.UserID=b.ID where a.UserId=?", new object[]{userId});
			return list;
			//
		}

		public List<CommentData> GetCommentsByNewsId(int NewsId)
		{
			var list = Resolver.Resolve<ISQLiteClient> ().GetAllData<CommentData>("select * from comment as a join user as b on a.UserID=b.ID where a.NewsId=?", new object[]{NewsId});
			return list;
		}
	}
}

