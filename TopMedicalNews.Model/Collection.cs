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
		[PrimaryKey]
		public int ID{ get; set; }
		public int UID{ get; set; }
		public string UserName{ get; set; }
		public int WZ_ID{ get; set; }
		public int View{ get; set; }
		public int Comment{ get; set; }
		public string Title_Name{ get; set; }
		public DateTime Add_Time{ get; set; }
		public string Post_Info{ get; set; }
		public string Imginfo{ get; set; }
		public string Stem_From{ get; set; }

	}
}

