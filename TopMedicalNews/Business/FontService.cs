using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public interface IFontService{
		 List<MyFont> GetAllFont();
		MyFont GetFontById (int id);
	}
	public class FontService:IFontService
	{
		public FontService ()
		{

		}
		public List<MyFont> GetAllFont()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<MyFont> ();
		}
		public MyFont GetFontById(int id)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetData<MyFont> (r => r.ID == id);

		}
	}
}

