using System;
using SQLite.Net.Attributes;

namespace TopMedicalNews.Model
{
	public class Department
	{
		[PrimaryKey]
		public int Id{get;set;}
		public int ParentId{get;set;}
		public string ParentTitle{ get; set; }
		public string Title{get;set;}
	}
}

