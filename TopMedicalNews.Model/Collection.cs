using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class Collection
	{
		[PrimaryKey,AutoIncrement]
		public int ID{ get; set; }
		public int UserID{ get; set; }
		public int NewsID{get;set;}
		public Collection ()
		{

		}
	}
}

