using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class Column
	{
		public string Title{get;set;}
		[PrimaryKey]
		public int ID{get;set;}
		public int  CategoryID{get;set;}
	
		public override bool Equals(object obj)
		{
			Column e = obj as Column;
			return e.ID == ID;
		}
	}
}

