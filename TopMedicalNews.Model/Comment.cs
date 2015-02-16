using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class Comment
	{
		public int  UserID{ get; set; }
		public int  NewsID{ get; set; }
		[PrimaryKey,AutoIncrement]
		public int    ID{ get; set; }
		public DateTime PublishTime{ get; set; }
		public string Content{ get; set; }
		public Comment ()
		{
		}
	}
}

