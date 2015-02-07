using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public interface IFontService{
		 List<Font> GetAllFont();
		 Font GetFontById (int id);
	}
	public class FontService:IFontService
	{
		public FontService ()
		{

		}
		public List<Font> GetAllFont()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Font> ();
		}
		public Font GetFontById(int id)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetData<Font> (r => r.ID == id);

		}
	}
}

