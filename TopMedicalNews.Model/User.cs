using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class User
	{
		[PrimaryKey]
		public int    ID { get; set; }
		public string ImageUri{ get; set; }
		public string UserName{ get; set; }
		public string Password { get; set; }
		public User ()
		{

		}
	}
}

