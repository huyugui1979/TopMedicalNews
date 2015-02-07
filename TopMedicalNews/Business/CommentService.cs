using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;

namespace TopMedicalNews
{

	public interface ICommentService
	{
		List<CommentData> GetCommentsByNewsId(int NewsId);
	}
	public class CommentService:ICommentService
	{
		public CommentService ()
		{
		}
		public List<CommentData> GetCommentsByNewsId(int NewsId)
		{
			var list = Resolver.Resolve<ISQLiteClient> ().GetAllData<CommentData>("select * from comment as a join user as b on a.UserID=b.ID where a.NewsId=?", new object[]{NewsId});
			return list;
		}
	}
}

