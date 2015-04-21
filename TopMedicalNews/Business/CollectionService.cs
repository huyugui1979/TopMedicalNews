using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public interface ICollectionService
	{
		Task<int>  AddCollect (string username, int uid, int wz_id, string title_name);

		Task  DelCollect (string username, int uid, int wz_id);
		//
		Task<List<Collection>> ListCollect (string username, int uid, int since = 0, int limit = 0);
		//
	}

	public class CollectionService:ICollectionService
	{
		public CollectionService ()
		{

		}

		public Task<int>  AddCollect (string username, int uid, int wz_id, string title_name)
		{	
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("wz_id", wz_id.ToString ());
				param.Add ("username", username);
				param.Add ("uid", uid.ToString ());
				param.Add ("title_name", title_name);
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/addcollect/", param);
				var obj = JsonHelper.Deserialize (ss) as IDictionary;
				return int.Parse (obj ["res"].ToString ());
				//
			});
		}

		public Task<List<Collection>> ListCollect (string username, int uid, int since = 0, int limit = 0)
		{
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				if (since != 0)
					param.Add ("since", since.ToString ());
				param.Add ("username", username);
				param.Add ("uid", uid.ToString ());
				if (limit != 0)
					param.Add ("limit", limit.ToString ());
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/listcollect/", param);

				var obj = JsonHelper.Deserialize (ss) as IDictionary;
				var list = obj["list"] as List<object>;
				List<Collection> lcs = new List<Collection>();
				list.ForEach(r =>{
					var dict = r as IDictionary;
					Collection c = new Collection{ID=int.Parse(dict["id"].ToString()),UserName=dict["username"].ToString(),
						UID=int.Parse(dict["uid"].ToString()),WZ_ID=int.Parse(dict["wz_id"].ToString()),
						View=int.Parse(dict["view"].ToString()),Comment=int.Parse(dict["comment"].ToString()),Title_Name=dict["title_name"].ToString(),Add_Time=DateTime.Parse(dict["add_time"].ToString()),Post_Info=dict["post_info"].ToString().Substring(0,50),
						Imginfo=dict["imginfo"].ToString(),Stem_From=dict["stem_from"].ToString()};
					lcs.Add(c);
				});
				return lcs;
				//
			});
		}

		public Task  DelCollect (string username, int uid, int wz_id)
		{	
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("wz_id", wz_id.ToString ());
				param.Add ("username", username);
				param.Add ("uid", uid.ToString ());
				//
				Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/delcollect/", param);
				//
			});
		}

	}
}

