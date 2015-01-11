using System;
#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{
	public class News
	{
		[PrimaryKey]
		public int   ID{ get; set; }
		public string Thumb{ get; set; }//缩写
		public string Title{get;set;}//标题
		public string Content{get;set;}//内容
		public DateTime PublishTime{ get; set; }//出版时间
		public string ImageUri{get;set;}//图像url
		public string FromSource{get;set;}//来源
		public int ViewerNum{ get; set; }//浏览数
		public int PosterNum{get;set;}//跟
		public int ColumnId{get;set;}//所属栏目
		public bool Focus{get;set;} //是否是重点
	}
}

