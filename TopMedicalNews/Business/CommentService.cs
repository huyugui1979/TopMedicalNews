using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;
using System.Threading.Tasks;
using System.Threading;
using RestSharp;
using System.Collections;

namespace TopMedicalNews
{

	public interface ICommentService
	{
//		List<Comment> GetCommentsByNewsId (int NewsId);
//
//		List<Comment> GetMyComments (int userId);

		Task<List<Comment>> DownloadComment (int newsId);

	    Task  AddComment(string username,int uid,int wz_id,string content);
		Task<List<Reading>> ListMeComment(string username,int uid,int since=0,int limit=0);
	}

	public class CommentService:ICommentService
	{
		public CommentService ()
		{

		}
		public  Task  AddComment(string username,int uid,int wz_id,string content)
		{
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("wz_id", wz_id.ToString());
				param.Add ("username", username);
				param.Add ("uid", uid.ToString());
				param.Add ("content", content);
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/addcomment/", param);
				//

			});

		}
		public Task<List<Reading>> ListMeComment(string username,int uid,int since=0,int limit=0)
		{
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("username", username);
				param.Add ("uid", uid.ToString());
				if(since !=0)
				param.Add ("since", since.ToString());
				if(limit !=0)
				param.Add ("limit", limit.ToString());
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/listmecomment/", param);
//				var obj = JsonHelper.Deserialize (ss) as IDictionary;
//				//
//				var list = obj ["list"] as List<object>;
//				List<Comment> comments = new List<Comment> ();
//				list.ForEach (m => {
//					var dict = m as Dictionary<string,object>;
//					string imageUrl = "http://u.iiyibbs.com/u/avatar.php?uid="+dict ["uid"].ToString()+"&size=small";
//
//					Comment comment = new Comment {ID = int.Parse (dict ["id"].ToString()), UserID = int.Parse (dict ["uid"].ToString()), UserName = dict ["username"].ToString(), Content = dict ["comment_info"].ToString(),
//						PublishTime = DateTime.Parse (dict ["add_time"].ToString()),ImageUrl=imageUrl, NewsID = int.Parse (dict ["wz_id"].ToString())
//					};
//
//					comments.Add (comment);
//					//
//				});
				var obj = JsonHelper.Deserialize (ss) as IDictionary;
				var list = obj["list"] as List<object>;
				List<Reading> lcs = new List<Reading>();
				list.ForEach(r =>{
					var dict = r as IDictionary;
					Reading c = new Reading{Wz_Id
						=int.Parse(dict["wz_id"].ToString()),
						View=int.Parse(dict["view"].ToString()),Comment=int.Parse(dict["comment"].ToString()),Title_Name=dict["title_name"].ToString(),
						Add_Time=DateTime.Parse(dict["add_time"].ToString()),
						Post_Info=dict["post_info"].ToString().Substring(0,50),
						Imginfo=dict["imginfo"].ToString()};
					lcs.Add(c);
				});
				//Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceAll (comments, typeof(Comment));
				return lcs;
			});
		}
		public  Task<List<Comment>> DownloadComment (int newsId)
		{
			//
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("wz_id", newsId.ToString ());
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/listcomment/", param);
				var obj = JsonHelper.Deserialize (ss) as IDictionary;

				var list = obj ["list"] as List<object>;
				List<Comment> comments = new List<Comment> ();
				list.ForEach (m => {
					var dict = m as Dictionary<string,object>;
					string imageUrl = "http://u.iiyibbs.com/u/avatar.php?uid="+dict ["uid"].ToString()+"&size=small";

					Comment comment = new Comment {ID = int.Parse (dict ["id"].ToString()), UserID = int.Parse (dict ["uid"].ToString()), UserName = dict ["username"].ToString(), Content = dict ["comment_info"].ToString(),
						PublishTime = DateTime.Parse (dict ["add_time"].ToString()),ImageUrl=imageUrl, NewsID = int.Parse (dict ["wz_id"].ToString())
					};
					
					comments.Add (comment);
					//
				});
				//Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceAll (comments, typeof(Comment));
				return comments;
			});
			//
		}

		public List<Comment> GetMyComments (int userId)
		{
			//
			var list = Resolver.Resolve<ISQLiteClient> ().GetAllData<Comment> (r => r.UserID == userId);
			return list;
			//
		}

		public List<Comment> GetCommentsByNewsId (int NewsId)
		{
			var list = Resolver.Resolve<ISQLiteClient> ().GetAllData<Comment> (r => r.NewsID == NewsId);
			return list;
		}
	}
}

