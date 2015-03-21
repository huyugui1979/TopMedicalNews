using System;
using SQLite.Net.Attributes;

namespace TopMedicalNews.Model
{
	public class Reading
	{
		public Reading ()
		{
		}
		public string Title_Name{get;set;}
		public int Wz_Time{ get; set; }
		public int View{ get; set; }
		public string Imginfo{get;set;}
		public int Comment{get;set;}
		public int Wz_Id{get;set;}
		public  DateTime Add_Time{get;set;}
		public string Post_Info{get;set;}


	}
}

