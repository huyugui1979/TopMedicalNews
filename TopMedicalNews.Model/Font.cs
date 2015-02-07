using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class Font
	{
		public Font ()
		{
		}
		[PrimaryKey]
		public int ID{get;set;}
		public string Title{get;set;}
	}
}

