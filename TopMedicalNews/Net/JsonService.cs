using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using System.Collections;
using System.Threading;
using System.Linq;

namespace TopMedicalNews
{
	public interface IJsonService
	{
		string ExecteQuery (string host, string url, Dictionary<string,string> param);
	}

	public class JsonService:IJsonService
	{

		public JsonService ()
		{
		}

		public string ExecteQuery (string host, string url, Dictionary<string,string> param)
		{
			var client = new RestClient ();
			client.BaseUrl = new Uri (host);
			var request = new RestRequest ();
			request.Resource = url;
			param.Keys.ToList ().ForEach (k => {
				request.AddQueryParameter (k, param [k]);
			});
			client.Timeout = 3*1000*1000;
			var res = client.Execute (request);

			if (res.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var twilioException = new ApplicationException(message, res.ErrorException);
				throw twilioException;
			}

			object obj = SimpleJson.DeserializeObject (res.Content);
			var s = obj as JsonObject;

			var ft = obj.GetType ();
			if (s ["code"].ToString () != "200") {
				throw new MyException (s ["msg"].ToString ());
			}
			return s ["data"].ToString ();


		}
	}
}

