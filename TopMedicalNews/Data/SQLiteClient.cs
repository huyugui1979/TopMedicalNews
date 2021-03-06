﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.IO;
using TopMedicalNews.Model;
using Refractored.Xam.Settings;
using System.Collections;

#if __ANDROID__
using SQLite.Net.Platform.XamarinAndroid;
#else
using SQLite.Net.Platform.XamarinIOS;
#endif
using SQLite.Net;
using Refractored.Xam.Settings.Abstractions;
namespace TopMedicalNews
{
	public interface ISQLiteClient {
		/// <summary>
		/// 获取某个表内所有数据
		/// </summary>
		/// <returns>The all data.所有的数据</returns>
		/// <typeparam name="T">The 1st type parameter.某个类型</typeparam>
		List<T> GetAllData<T> () where T : new();
		/// <summary>
		/// 获取某个表内指定行的数据
		/// </summary>
		/// <returns>The all data.</returns>
		/// <param name="count">Count.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		List<T> GetAllData<T>(int count) where T : new();
		/// <summary>
		/// 插入一批数据
		/// </summary>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void InsertAllData<T> (List<T> data) ;

		/// <summary>
		/// 查询表内是否存在符合条件的数据
		/// </summary>
		/// <param name="expres">Expres.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		bool Exist<T>(Expression<Func<T, bool>> expres) where T : new();
		/// <summary>
		/// Inserts the data.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void InsertData<T> (T data);
		//

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void DeleteAllData<T>();
		/// <summary>
		/// Deletes the data.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void DeleteData<T> (int id);
		/// <summary>
		/// Deletes the data.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void DeleteData<T> (List<T> data);
		//
		void UpdateAllData<T> (List<T> obs);
		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <param name="o">O.</param>
		void UpdateData (object o);
		/// <summary>
		/// Gets the scalar.
		/// </summary>
		/// <returns>The scalar.</returns>
		/// <param name="sql">Sql.</param>
		/// <param name="param">Parameter.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		T GetScalar<T> (string sql,params object[] param);
		/// <summary>
		/// Gets all data获取所有符合条件的数据
		/// </summary>
		/// <returns>The all data.</returns>
		/// <param name="expres">Expres.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		List<T>  GetAllData<T> (Expression<Func<T,bool>> expres) where T:new();
		/// <summary>
		/// 获取获取所有符合条件的指定行数的数据
		/// </summary>
		/// <returns>The all data.</returns>
		/// <param name="expres">Expres.</param>
		/// <param name="count">Count.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		List<T> GetAllData<T>(Expression<Func<T, bool>> expres,int count) where T : new();
		/// <summary>
		/// 获取使用sql语句的数据
		/// </summary>
		/// <returns>The all data.</returns>
		/// <param name="sql">Sql.</param>
		/// <param name="args">Arguments.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		List<T> GetAllData<T>(string sql, params object[] args) where T : new();
		/// <summary>
		/// 清空数据库
		/// </summary>
		/// 
		T GetData<T> (Expression<Func<T, bool>> expres) where T : new();
		//
		T GetData<T> ()  where T : new();
		//
		//int Count<T> ()  where T : new();
	    void InsertOrReplaceAll(IEnumerable data,Type type);
		//
	   void InsertOrReplaceData<T> (T data);
		//
		void Init();
	}
	public class SQLiteClient:ISQLiteClient
	{

		private readonly SQLiteConnection _connection;

		public SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "Conferences.db3";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);

			var path = Path.Combine (documentsPath, sqliteFilename);
			#if __ANDROID__
			var platform = new SQLitePlatformAndroid ();
			#else 
			var platform = new  SQLitePlatformIOS();
			#endif
			var connection =new SQLiteConnection(
				platform,
				path.ToString(),true);
            
			return connection;
		}
		void AddTestData()
		{
//			List<Category> cate = new List<Category> ();
//			cate.Add (new Category{ Title = "内科", ID = 1 });
//			cate.Add (new Category{ Title = "外科", ID = 2 });
//			cate.Add (new Category{ Title = "妇产科", ID = 3 });
//			cate.Add (new Category{ Title = "儿科", ID = 4 });
//			cate.Add (new Category{ Title = "五官科", ID = 5 });
//			cate.Add (new Category{ Title = "肿瘤科", ID = 6 });
//			cate.Add (new Category{ Title = "传染科", ID = 7 });
//			cate.Add (new Category{ Title = "其他", ID = 8 });
//			cate.Add (new Category{ Title = "末分类", ID = 9 });
//
//			_connection.InsertAll (cate);
//
//			//
//			List<Column> column = new List<Column> ();
//			column.Add (new Column{ Title = "生物制药", CategoryID=9});
//			column.Add (new Column{ Title = "医药制药",CategoryID=9});
//			column.Add (new Column{ Title = "医改", ID = 3 ,Like=true,LikeOrder=3,CategoryID=9});
//			column.Add (new Column{ Title = "政策解读", ID = 4,Like=true,LikeOrder=2,CategoryID=9});
//			column.Add (new Column{ Title = "头条", ID = 42,Like=true,LikeOrder=1,CategoryID=9});
//
//			column.Add (new Column{ Title = "呼吸内科", Like=true,ID = 5,LikeOrder=6,CategoryID=1});
//			column.Add (new Column{ Title = "消化内科", Like=true,ID = 6,LikeOrder=7,CategoryID=1 });
//			column.Add (new Column{ Title = "神经内科", Like=true,ID = 7,LikeOrder=8,CategoryID=1 });
//			column.Add (new Column{ Title = "心血管内科", Like=true,LikeOrder=9,ID = 8,CategoryID=1});
//			column.Add (new Column{ Title = "肾内科", Like=true,LikeOrder=10,ID = 9 ,CategoryID=1});
//			column.Add (new Column{ Title = "血液内科", Like=true,LikeOrder=11,ID = 10,CategoryID=1 });
//			column.Add (new Column{ Title = "免疫科",Like=true, LikeOrder=12,ID = 11 ,CategoryID=1});
//			column.Add (new Column{ Title = "内化泌科",Like=true,LikeOrder=13, ID = 12,CategoryID=1 });
//			column.Add (new Column{ Title = "普通外科",Like=true, LikeOrder=14,ID = 13,CategoryID=2});
//			column.Add (new Column{ Title = "心胸外科",Like=true,LikeOrder=15, ID = 14 ,CategoryID=2});
//			column.Add (new Column{ Title = "神经外科", ID = 15 ,CategoryID=2});
//			column.Add (new Column{ Title = "泌尿外科", ID = 16,CategoryID=2 });
//			column.Add (new Column{ Title = "肝胆外科", ID = 17 ,CategoryID=2});
//			column.Add (new Column{ Title = "肛肠外科", ID = 18 ,CategoryID=2});
//			column.Add (new Column{ Title = "烧伤科", ID = 19 ,CategoryID=2});
//			column.Add (new Column{ Title = "骨外科", ID = 20 ,CategoryID=2});
//			column.Add (new Column{ Title = "妇科", ID = 21 ,CategoryID=3});
//			column.Add (new Column{ Title = "产科", ID = 22 ,CategoryID=3});
//			column.Add (new Column{ Title = "计划生育", ID = 23,CategoryID=3});
//			column.Add (new Column{ Title = "儿科综合", ID = 24 ,CategoryID=4});
//			column.Add (new Column{ Title = "小儿内科", ID = 25 ,CategoryID=4});
//			column.Add (new Column{ Title = "新生儿科", ID = 26 ,CategoryID=4});
//			column.Add (new Column{ Title = "耳鼻喉科", ID = 27 ,CategoryID=5});
//			column.Add (new Column{ Title = "眼科", ID = 28,CategoryID=5});
//			column.Add (new Column{ Title = "口腔科", ID = 29 ,CategoryID=5});
//			column.Add (new Column{ Title = "肿瘤内科", ID = 30 ,CategoryID=6});
//			column.Add (new Column{ Title = "肿瘤外科", ID = 31 ,CategoryID=6});
//			column.Add (new Column{ Title = "肿瘤妇科", ID = 32 ,CategoryID=6});
//			column.Add (new Column{ Title = "肿瘤综科", ID = 33 ,CategoryID=6});
//			column.Add (new Column{ Title = "肝病科", ID = 34 ,CategoryID=7});
//			column.Add (new Column{ Title = "爱滋科", ID = 35 ,CategoryID=7});
//			column.Add (new Column{ Title = "结核病", ID = 36,CategoryID=7});
//			column.Add (new Column{ Title = "寄生虫", ID = 37 ,CategoryID=7});
//			column.Add (new Column{ Title = "中医科", ID = 38 ,CategoryID=8});
//			column.Add (new Column{ Title = "精神科", ID = 39 ,CategoryID=8});
//			column.Add (new Column{ Title = "皮肤科", ID = 40 ,CategoryID=8});
//			column.Add (new Column{ Title = "全科", ID = 41 ,CategoryID=8});
//
//			_connection.InsertAll (column);
//			//
//			List<News> news = new List<News> ();
//			news.Add (new News{ ID=1, Focus=false,Type= "news", ColumnId=2, Tag="全球资讯-医药", Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png",FromSource="医脉通", PosterNum = 999, ViewerNum=1000, PublishTime = DateTime.Parse( "2012-03-01") });
//			news.Add(new News { ID = 2, ColumnId = 2,Type= "topic", Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期,基本药物目录已经进入了三年调整周期,基本药物目录已经进入了三年调整周期", FromSource="医脉通",PosterNum = 999, ViewerNum=1000,PublishTime = DateTime.Parse( "2012-03-01")  });
//			news.Add(new News { ID = 3, ColumnId = 2, Focus=false,Type= "news", Tag="全球资讯-医药", Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期,", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", FromSource="医脉通",PosterNum = 999, ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 4, ColumnId =2, Focus=false,Type= "news", Title = "基本药物目录拟调整：", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png",FromSource="医脉通",PosterNum = 999, ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 5, ColumnId = 2,  Focus=false,Type= "news",Tag="全球资讯-医药", Title = "基本药物目录拟调整：", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 6, ColumnId = 1,Type= 1, Tag="全球资讯-医药", Title = "基本药物目录拟调整：", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png",  PosterNum = 999, FromSource="医脉通",ViewerNum=1000,PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 7, Focus=true,Type= 1, Tag="全球资讯-医药", ColumnId =2, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999,FromSource="医脉通", ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 8, Focus=true,Type= 1, Tag="全球资讯-医药", ColumnId = 2, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999,FromSource="医脉通", ViewerNum=1000, PublishTime= DateTime.Parse( "2012-03-01")  });
//			news.Add(new News { ID = 9, Focus=true,Type= 1, Tag="全球资讯-医药", ColumnId = 9,  Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 10, Focus=true,Type= 1, Tag="全球资讯-医药", ColumnId = 9, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999,FromSource="医脉通", ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 11, ColumnId =1,ThemeID=2, Type= 1, Tag="全球资讯-医药",Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png",PosterNum = 999, ViewerNum=1000,FromSource="医脉通", PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 12, ColumnId =1, ThemeID=2,Type= 1, Tag="全球资讯-医药",Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, ViewerNum=1000, FromSource="医脉通",PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 13,  Focus=true,Type= 1, Tag="全球资讯-医药",ColumnId = 3, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 14, Focus=true,Type= 1,Tag="全球资讯-医药",  ColumnId = 3, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 15, ColumnId = 3, Type= 1, Tag="全球资讯-医药",Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, ViewerNum=1000,FromSource="医脉通", PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 16, Focus=true,Type= 1,Tag="全球资讯-医药",  ColumnId = 3, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 17, Focus=true,Type= 1,Tag="全球资讯-医药",  ColumnId = 4, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999, FromSource="医脉通",ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
//			news.Add(new News { ID = 18, Focus=true,Type= 1, Tag="全球资讯-医药", ColumnId = 4, Title = "基本药物目录拟调整", Content = "基本药物目录已经进入了三年调整周期。\n\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露，目前正在考虑对现有目录进行梳理，以确定类似鱼精蛋白的药品，并通过遴选定点生产企业保证临床供应。\n\n卫生部医管司医院运行监管处处长钟东波表示，目前看来，支付方式的改革相对较好，同时解决了医院和医生两个主体的激励问题。", Thumb = "基本药物目录已经进入了三年调整周期", ImageUri = "http://www.gcleasing.com.cn/JSJX/ueditor/jsp/upload/image/20141103/1414986043292032744.png", PosterNum = 999,FromSource="医脉通", ViewerNum=1000, PublishTime = DateTime.Parse("2012-03-01") });
			//_connection.InsertAll (news);
//			//
//			List<User> users = new List<User> ();
//			users.Add(new User{UserName="王二",ID=2,ImageUri="portait.png"});
//			_connection.InsertAll (users);
//			//
//			List<Comment> comments = new List<Comment> ();
//			comments.Add(new Comment { ID = 1,NewsID=1,UserID=2, Content = "基本药物目录", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 1,NewsID=1,UserID=1, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 2,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 3,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 4,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周遥遥遥摇遥。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 5,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 6,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 7,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 8,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 9,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 10,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 11,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID =12,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 13,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 14,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID =15,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 16,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 17,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 18,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 19,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID =20,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 21,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 22,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 23,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 24,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID =25,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 26,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 27,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露\n基本药物目录已经进入了三年调整周期。\n在10月12日举行的卫生部新闻发布会上，卫生部药政司司长郑宏透露", PublishTime = DateTime.Parse("2012-03-01") });
//			comments.Add(new Comment { ID = 28,NewsID=1,UserID=2, Content = "基本药物目录已经进入了三年调整周期。\n遥遥遥", PublishTime = DateTime.Parse("2012-03-01") });
//
//			//
//			_connection.InsertAll (comments);
//			//
//			List<TopMedicalNews.Model.MyFont> fonts = new List<TopMedicalNews.Model.MyFont> ();
//			fonts.Add (new TopMedicalNews.Model.MyFont{Title="小",ID=1 });
//			fonts.Add (new TopMedicalNews.Model.MyFont{Title="中",ID=2 });
//			fonts.Add (new TopMedicalNews.Model.MyFont{Title="大",ID=3 });
//			//
//
//			_connection.InsertAll (fonts);
		}
			
		public  SQLiteClient ()
		{
            _connection = GetConnection();

			//
			 
		}

		public void Init()
		{
			_connection.CreateTable<News>();
			_connection.CreateTable<Column>();
			_connection.CreateTable<Comment> ();
			_connection.CreateTable<Collection> ();
			_connection.CreateTable<Category>();
			_connection.CreateTable<Reading> ();
			_connection.CreateTable<Department> ();
		}

//		public int Count<T> ()
//		{
//			return _connection.Table<T> ().Count();
//		}
		public T GetScalar<T> (string sql,params object[] param)
		{


			return  _connection.ExecuteScalar<T> (sql, param);
		
		}
		public  bool Exist<T>(Expression<Func<T,bool>> expres) where T:new()
		{

				return   _connection.Table<T>().Where(expres).Count() > 0 ? true : false;

		}
		public  void UpdateData(object o)
		{
			 _connection.Update (o);
		}
		public void UpdateAllData<T>(List<T> obs)
		{
			_connection.UpdateAll (obs);
		}
		public  void DeleteAllData<T>()
		{


				 _connection.DeleteAll<T> ();

		}
		public  void DeleteData<T> (int id)
		{

			_connection.Delete<T> (id);

		}
		public  void DeleteData<T> (List<T> data)
		{

			foreach (T o in data) {
				_connection.Delete (o);
			}
				
		}
		public  List<T> GetAllData<T>(Expression<Func<T, bool>> expres,int count) where T : new()
		{
			
			return  new List<T> (_connection.Table<T> ().Where (expres).Take (count));
			
		}
		public  List<T> GetAllData<T> (Expression<Func<T,bool>> expres) where T:new()
		{

			return  new List<T>(_connection.Table<T> ().Where (expres));

		}
		public  List<T> GetAllData<T>(int count) where T : new()
		{

			return  new List<T> (_connection.Table<T> ().Take (count));

		}
		public List<T> GetAllData<T> () where T : new()
		{
			return  new List<T>(_connection.Table<T> ());
			
		}
		public  List<T> GetAllData<T>(string sql, params object[] args) where T : new()
		{
			
				return  _connection.Query<T> (sql, args);
			
		}

		public  void InsertAllData<T> (List<T> data)
		{
				 _connection.InsertAll (data);
			
		}
		public void InsertOrReplaceAll(IEnumerable data,Type type)
		{
			_connection.InsertOrReplaceAll (data, type);
		}
		public T GetData<T>(Expression<Func<T, bool>> expres) where T : new()
		{
			
				return  _connection.Table<T> ().Where (expres).FirstOrDefault ();
			

		}
		public T GetData<T>()  where T : new()
		{
			
				return  _connection.Table<T> ().FirstOrDefault ();
			
		}
		public void InsertOrReplaceData<T> (T data)
		{

			_connection.InsertOrReplace (data);

		}
		public void InsertData<T> (T data)
		{

			_connection.Insert (data);
			
		}
		
	}
}

