using System;
using XLabs.Ioc;
using TopMedicalNews.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace TopMedicalNews
{
	public interface IReadingService
	{
		Task AddRecord (string username, int uid, int wz_id, string title_name);
	  Task<List<Reading>> ListRecord (string username, int uid, int since = 0, int limit = 0);
	}
	public class ReadingService:IReadingService
	{

		public Task AddRecord(string username,int uid,int wz_id,string title_name)
		{
			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("wz_id", wz_id.ToString());
				param.Add ("username", username);
				param.Add ("uid", uid.ToString());
				param.Add ("title_name", title_name);
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/addrecord/", param);
				//

			});
		}
		public Task<List<Reading>> ListRecord (string username, int uid, int since = 0, int limit = 0)
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
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/listrecord/", param);

				var obj = JsonHelper.Deserialize (ss) as IDictionary;
				var list = obj["list"] as List<object>;
				List<Reading> lcs = new List<Reading>();
				list.ForEach(r =>{
					var dict = r as IDictionary;
					Reading c = new Reading{Wz_Id
						 =int.Parse(dict["wz_id"].ToString()),
						Wz_Time=int.Parse(dict["wz_time"].ToString()),
						View=int.Parse(dict["view"].ToString()),Comment=int.Parse(dict["comment"].ToString()),Title_Name=dict["title_name"].ToString(),
						Add_Time=DateTime.Parse(dict["add_time"].ToString()),
						Post_Info=(dict["post_info"].ToString().Length>50)?dict["post_info"].ToString().Substring(0,50):"",
						Imginfo=dict["imginfo"].ToString()};
					lcs.Add(c);
				});
				return lcs;
				//
			});
		}

	}
}

