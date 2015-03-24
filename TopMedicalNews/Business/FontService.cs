using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using XLabs.Ioc;
using Refractored.Xam.Settings;

namespace TopMedicalNews
{
	public interface IFontService{
		 List<MyFont> GetAllFont();
		 void SetSelectFont (int val);
		 int GetSelectFont();
		void Init();
	}
	public class FontService:IFontService
	{
		public FontService ()
		{
			List<MyFont> fonts = new List<MyFont>();
			fonts.Add (new MyFont{ Title = "小", ID = 1 });
			fonts.Add (new MyFont{ Title = "中", ID = 2 });
			fonts.Add (new MyFont{ Title = "大", ID = 3 });
			SetAllFont (fonts);

		}
		public void Init()
		{
			//
			SetSelectFont (1);
			//
		}
		public List<MyFont> GetAllFont()
		{
			string ids =  CrossSettings.Current.GetValueOrDefault<string> ("Fonts", null);
			if (ids != "") {
				var ob = RestSharp.SimpleJson.DeserializeObject<List<MyFont>> (ids);
				return ob;
			} else
				return  null;
		}
		public void SetSelectFont(int val)
		{
			CrossSettings.Current.AddOrUpdateValue<int> ("SelectFont", val);
		}
		public int GetSelectFont()
		{
			return CrossSettings.Current.GetValueOrDefault<int> ("SelectFont");
		}
		public void SetAllFont(List<MyFont> fonts)
		{
			string s = RestSharp.SimpleJson.SerializeObject (fonts);
			CrossSettings.Current.AddOrUpdateValue<string> ("Fonts",s);

		}
	}
}

