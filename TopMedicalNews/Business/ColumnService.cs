using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using TopMedicalNews;
using System.Linq;
using XLabs.Ioc;
using RestSharp;
using RestSharp.Deserializers;
using Newtonsoft.Json.Linq;
using System.Collections;


namespace TopMedicalNews
{
    public interface IColumnService
    {
        List<Column> GetColumnByCategory(int categoryId);
		List<Column> GetLikeColumns();
		void Init();
		void UpdateColumns(List<Column> columns);
	    List<Column> GetColumnByCategoryNotLike(int categoryId);
    }
	public static class JsonHelper
	{
		public static object Deserialize(string json)
		{
			return ToObject(JToken.Parse(json));
		}

		private static object ToObject(JToken token)
		{
			if (token.Type == JTokenType.Object)
			{
				Dictionary<string, object> dict = new Dictionary<string, object>();
				foreach (JProperty prop in token)
				{
					dict.Add(prop.Name, ToObject(prop.Value));
				}
				return dict;
			}
			else if (token.Type == JTokenType.Array)
			{
				List<object> list = new List<object>();
				foreach (JToken value in token)
				{
					list.Add(ToObject(value));
				}
				return list;
			}
			else
			{
				return ((JValue)token).Value;
			}
		}
	}

	public class ColumnService:IColumnService
	{
		public ColumnService ()
		{

		}
		List<String> likeTitle = new List<string>(){"头条","医药资讯","医改","政策解读","生物制药"};

		public void Init()
		{
			//
			Resolver.Resolve<ISQLiteClient> ().DeleteAllData<Category> ();
			Resolver.Resolve<ISQLiteClient> ().DeleteAllData<Column> ();

			//
			var client = new RestClient();
			client.BaseUrl = new Uri("http://iapp.iiyi.com/");
			//client.Authenticator = new HttpBasicAuthenticator("username", "password");
			var request = new RestRequest();
			request.Resource = "yjtt/v1/news/tags/";
			IRestResponse response = client.Execute(request);

			//var obj = respon.DeserializeObject (response.Content);
			var obj = JsonHelper.Deserialize (response.Content) as IDictionary;
			var dataList = obj ["data"] as IDictionary;
		
			foreach(var key in dataList.Keys)
			{
				var dict = dataList [key] as IDictionary;
				Category c = new Category{ ID = int.Parse(dict["id"] as string), Title = dict ["name"] as string };
				Resolver.Resolve<ISQLiteClient> ().InsertData<Category> (c);
				var child = dict ["subs"] as IList;
				foreach (var val in child) {
					var dict1 =  val as IDictionary;
					Column co = new Column{ ID = int.Parse(dict1["id"] as string), Title = dict1 ["name"] as string,CategoryID=c.ID };
					Resolver.Resolve<ISQLiteClient> ().InsertData<Column> (co);
					if (likeTitle.Contains (co.Title)) {
						co.Like = true;
						co.LikeOrder = likeTitle.IndexOf (co.Title);
					}
				}
			}

			//

			//
			//To deserialize into a simple variable, use the <Dictionary<string, string>> type

		}

		public List<Column> GetColumnByCategoryNotLike(int categoryId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r=>r.CategoryID==categoryId && r.Like==false);
		}
		public List<Column> GetColumnByCategory(int categoryId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r=>r.CategoryID==categoryId);
		}
		public List<Column> GetLikeColumns()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r => r.Like == true);

		}
		public void UpdateColumns(List<Column> columns)
		{
			Resolver.Resolve<ISQLiteClient> ().UpdateAllData<Column> (columns);
		}
	}
}

