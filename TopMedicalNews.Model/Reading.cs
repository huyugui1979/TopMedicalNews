using System;
using SQLite.Net.Attributes;

namespace TopMedicalNews.Model
{
	public class Reading
	{
		public Reading ()
		{
		}
		[PrimaryKey,AutoIncrement]
		public int ID{ get; set; }
		public int UserID{get;set;}
		public int NewsID{get;set;}
	}
}

