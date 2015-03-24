using System;
using System.Collections.Generic;



#if __ANDROID__ || __IOS__
using SQLite.Net.Attributes;
using SQLite.Net;
using XLabs.Data;
#else
using ServiceStack.DataAnnotations;
#endif
namespace TopMedicalNews.Model
{

	public class News:ObservableObject
	{
		[PrimaryKey]
		public int   ID{ get; set; }

		public int   ThemeID{ get; set; } //是某个主题
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
		public string Type{ get; set; }//新闻类型
		public string Tag{ get; set; }//标签
		public int RankTime{ get; set; }
		public bool Download{get;set;}

		bool collect;
		public bool Collect{ get { return collect; } set { SetProperty (ref collect, value); } }
		public override string ToString ()
		{
			return Title;
		}
	}
}

