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
using Refractored.Xam.Settings.Abstractions;


namespace TopMedicalNews
{
	public interface IColumnService
	{
		List<Column> GetColumnByCategory (int categoryId);


		void Init ();

		void UpdateColumns (List<Column> columns);

		Column GetColumnById (int columnId);
	}

	public static class JsonHelper
	{
		public static object Deserialize (string json)
		{
			return ToObject (JToken.Parse (json));
		}

		private static object ToObject (JToken token)
		{
			if (token.Type == JTokenType.Object) {
				Dictionary<string, object> dict = new Dictionary<string, object> ();
				foreach (JProperty prop in token) {
					dict.Add (prop.Name, ToObject (prop.Value));
				}
				return dict;
			} else if (token.Type == JTokenType.Array) {
				List<object> list = new List<object> ();
				foreach (JToken value in token) {
					list.Add (ToObject (value));
				}
				return list;
			} else {
				return ((JValue)token).Value;
			}
		}
	}

	public class ColumnService:IColumnService
	{
		public ColumnService ()
		{

		}

		List<String> likeTitle = new List<string> (){ "头条", "医药资讯", "医改", "政策解读", "生物制药" };

		public void Init ()
		{
			//
			try {
				Resolver.Resolve<ISQLiteClient> ().DeleteAllData<Category> ();
				Resolver.Resolve<ISQLiteClient> ().DeleteAllData<Column> ();

				//
		
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/news/tags/", new Dictionary<string, string> ());


				//var obj = respon.DeserializeObject (response.Content);
				var dataList = JsonHelper.Deserialize (ss) as IDictionary;
		
		
				foreach (var key in dataList.Keys) {
					var dict = dataList [key] as IDictionary;
					Category c = new Category{ ID = int.Parse (dict ["id"] as string), Title = dict ["name"] as string };
					Resolver.Resolve<ISQLiteClient> ().InsertData<Category> (c);
			
					var child = dict ["subs"] as IList;
					foreach (var val in child) {
						var dict1 = val as IDictionary;
						Column co = new Column {
							ID = int.Parse (dict1 ["id"] as string),
							Title = dict1 ["name"] as string,
							CategoryID = c.ID
						};
						Resolver.Resolve<ISQLiteClient> ().InsertData<Column> (co);
						//
				
					}
				}
				//
				Column temp = new Column{ ID = 1, Title = "头条", CategoryID = 1 };
				Resolver.Resolve<ISQLiteClient> ().InsertData<Column> (temp);
//			//
				if (Resolver.Resolve<ILikeColumnService> ().GetLikeColumns () == null) {
					List<Column> columns = new List<Column> ();

					foreach (var c in likeTitle) {
						var co = Resolver.Resolve<ISQLiteClient> ().GetData<Column> (r => r.Title == c);
						columns.Add (co);
					}
					Resolver.Resolve<ILikeColumnService> ().SetLikeColumn (columns);
				} 
					
			} catch (Exception e) {
				//

				//
			}
					

		}
		public Column GetColumnById (int columnId) 
		{
			return Resolver.Resolve<ISQLiteClient> ().GetData<Column> (r => r.ID == columnId);
		}
		public List<Column> GetColumnByCategory (int categoryId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Column> (r => r.CategoryID == categoryId);
		}
		public void UpdateColumns (List<Column> columns)
		{
			Resolver.Resolve<ISQLiteClient> ().UpdateAllData<Column> (columns);

		}
	}
}

