using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public interface ISoftService
	{
		Task  FeedBack (string question, string epg="",int type=0,string username="",int uid=0);
	}
	public class SoftService:ISoftService
	{
		public SoftService ()
		{
		}
		public  Task  FeedBack (string question,string epg="",int type=0,string username="",int uid=0)
		{

			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				if(username !="")
					param.Add ("username", username);
				if(uid !=0)
					param.Add("uid",uid.ToString());
				//
				param.Add("question",question);
				if(epg !="")
				{
					param.Add("epg",epg);
				}
				if(type !=-1)
				{
					param.Add("type",type.ToString());
				}
				Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/feedback/", param);


			});


		}
	}
}

