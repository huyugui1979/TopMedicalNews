using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using Refractored.Xam.Settings;
using System.Linq;
using Newtonsoft.Json;


namespace TopMedicalNews
{
	public interface ILikeColumnService
	{
		void  SetLikeColumn (List<Column> columns);
		List<Column>  GetLikeColumns();
	}
	public class LikeColumnService:ILikeColumnService
	{
		public LikeColumnService ()
		{
		}
		public List<Column>  GetLikeColumns()
		{
			string ids =  CrossSettings.Current.GetValueOrDefault<string> ("LikeColumns", "");
			if (ids != ""  ) {
				var ob = RestSharp.SimpleJson.DeserializeObject<List<Column>> (ids);
		
				return ob;
			} else
				return  null;
			//return CrossSettings..GetValueOrDefault<List<Column>> ("LikeColumns", null);
		}

		//
		public void SetLikeColumn(List<Column> columns)
		{
			string s = RestSharp.SimpleJson.SerializeObject (columns);
			CrossSettings.Current.AddOrUpdateValue<string> ("LikeColumns",s);
		    
		}
		//
	}
}

