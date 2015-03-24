using System;
using System.Collections.Generic;
using TopMedicalNews;
using TopMedicalNews.Model;
using Refractored.Xam.Settings.Abstractions;
using XLabs.Ioc;
using RestSharp;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TopMedicalNews
{
	public interface INewsService
	{
		//		List<News> GetNewsByColumn (int columnId);
		//
		//		List<News> GetNewsByTheme (int newsId);
		//
		//		News  GetNewById (int Id);
		//
		List<News> GetNews (int columnId);

		List<News> GetFocusNews (int columnId);

		Task<List<News>> DownloadTopicNews (int id);

		Task<List<News>> DownloadNews (int columnId, int time);

		//void UpdateNews (News news);

		Task<News> DownloadDetail (int newsId, string username = "", int uid = 0);
	}

	public class NewsService:INewsService
	{
		public NewsService ()
		{

		}

		public  Task<News> DownloadDetail (int newsId, string username = "", int uid = 0)
		{

			return Task.Factory.StartNew (() => {
				Dictionary<string,string> param = new Dictionary<string,string> ();
				param.Add ("id", newsId.ToString ());
				if (username != "")
					param.Add ("username", username);
				if (uid != 0)
					param.Add ("uid", uid.ToString ());
				//
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/news/detail/", param);
			
				var dataList = JsonHelper.Deserialize (ss) as IDictionary;
			
				var list = dataList ["multi"] as List<object>;

				string head = "<head><style>div {text-indent:2em;line-height:1.5;margin:5px}</style></head>";
				string content = head + "<body >";

				News news = new News ();
				list.ForEach (n => {
					var m = n as IDictionary;

					if (m ["type"].ToString () == "text") {
						content += "<div align=\"justify\">" + m ["content"].ToString () + "</div>";

					}
					if (m ["type"].ToString () == "image") {
						//
						int imgWidth = (int)App.ScreenWidth;//Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.WidthPixels-30;
						int imgHeight = int.Parse (m ["height"].ToString ()) * imgWidth / int.Parse (m ["width"].ToString ());

						content += "<img src=\"" + m ["content"].ToString () + "\" width=\"" + imgWidth.ToString () + "\" height=\"" + imgHeight.ToString () + "\"/>";
						//
					}

				});

				news.Content = content + "</body>";
				news.Title = dataList ["title_name"].ToString ();
				news.ID = newsId;
				TimeSpan span = new TimeSpan (0, 0, int.Parse (dataList ["wz_time"].ToString ()));
				news.PublishTime = new DateTime (1970, 1, 1) + span;
				news.FromSource = dataList ["stem_from"].ToString ();
				news.Collect = dataList ["collect"].ToString () == "1" ? true : false;
				//Resolver.Resolve<INewsService> ().UpdateNews (news);
				return news;
			});


		}

		public  Task<List<News>> DownloadTopicNews (int id)
		{
			return Task.Factory.StartNew (() => {

				Dictionary<string,string> param = new Dictionary<string,string> ();

				param.Add ("id", id.ToString ());
//				

				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/news/topic/", param);
				var obj = JsonHelper.Deserialize (ss) as IDictionary;


				var newsContent = obj ["list"] as List<object>;
				//apicide
				List<News> news = new List<News> ();
				if (newsContent.Count > 0) {
					foreach (var o in newsContent) {
						var r = o as IDictionary;
						var n = new News {
							ID = int.Parse (r ["wz_id"].ToString ()),
							//ColumnId = columnId,
							Focus = false,
							Title = r ["title_name"].ToString (),
							Thumb = (r ["post_info"].ToString ().Length > 50) ? r ["post_info"].ToString ().Substring (0, 50) + "..." : "",
							ImageUri = r ["imginfo"].ToString (),
							FromSource = r ["stem_from"].ToString (),

							PosterNum = int.Parse (r ["comment"].ToString ()),
							ViewerNum = int.Parse (r ["view"].ToString ()),
							RankTime = int.Parse (r ["wz_time"].ToString ()),
							PublishTime = DateTime.Parse (r ["add_time"].ToString ())
						};
						news.Add (n);
					}
					//Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceAll (news, typeof(News));
					//news.Where(r=>r.Focus==true).ToList().ForEach(r=>System.Diagnostics.Debug.WriteLine(r));
				}
				return news;
			});
		}

		public  Task<List<News>> DownloadNews (int columnId, int time)
		{
			//

			return Task.Factory.StartNew (() => {

				Dictionary<string,string> param = new Dictionary<string,string> ();


				param.Add ("tag", columnId.ToString ());

				param.Add ("since", time.ToString ());
				param.Add ("limit", "20");

				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/news/list/", param);
				var obj = JsonHelper.Deserialize (ss) as IDictionary;

				var hotsContent = obj ["hots"] as List<object>;
				var newsContent = obj ["list"] as List<object>;
				//apicide
				List<News> news = new List<News> ();
				if (newsContent.Count > 0) {
					foreach (var o in newsContent) {
						var r = o as IDictionary;
						var n = new News {
							ID = int.Parse (r ["id"].ToString ()),
							ColumnId = columnId,
							Focus = false,
							Title = r ["title"].ToString (),
							Thumb = r ["description"].ToString (),
							ImageUri = r ["image"].ToString (),
							FromSource = r ["source"].ToString (),
							Type = r ["type"].ToString (),
							PosterNum = int.Parse (r ["comment"].ToString ()),
							ViewerNum = int.Parse (r ["view"].ToString ()),
							RankTime = int.Parse (r ["rank_time"].ToString ()),
							PublishTime = DateTime.Parse (r ["pubdate"].ToString ())
						};
						news.Add (n);

					}
					foreach (var o in hotsContent) {
						var r = o as IDictionary;
			
						var	n = new News {
							ID = int.Parse (r ["id"].ToString ()),
							ColumnId = columnId,
							RankTime = int.Parse (r ["rank_time"].ToString ()),
							Focus = true,
							Title = r ["title"].ToString (),
							Thumb = r ["description"].ToString (),
							ImageUri = r ["image"].ToString (),
							FromSource = r ["source"].ToString (),
							Type = r ["type"].ToString (),
							PosterNum = int.Parse (r ["comment"].ToString ()),
							ViewerNum = int.Parse (r ["view"].ToString ()),
							PublishTime = DateTime.Parse (r ["pubdate"].ToString ())
						};
						news.Add (n);
						

					}
					//Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceAll (news, typeof(News));
					//news.Where(r=>r.Focus==true).ToList().ForEach(r=>System.Diagnostics.Debug.WriteLine(r));
				}
				return news;
			});
			//
		}

		public void UpdateNews (News news)
		{
			Resolver.Resolve<ISQLiteClient> ().UpdateData (news);
		}

		public List<News> GetCollectonNews (int userId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> ("select * from Collection as a join News as b on b.ID=a.NewsID where a.UserId={0}", userId);
		}

		public List<News> GetNewsByColumn (int columnId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r => r.ColumnId == columnId);

		}

		public News  GetNewById (int Id)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetData<News> (r => r.ID == Id);
		}

		public List<News> GetFocusNews (int columnId)
		{
		
			
			var list2 = Resolver.Resolve<ISQLiteClient> ().
				GetAllData<News> (r => r.ColumnId == columnId && r.Focus == true).OrderByDescending (r => r.RankTime).Take (5).ToList ();
			return list2;
		}

		public List<News> GetNews (int columnId)
		{
			//
			//System.DateTime startTime1 = TimeZone.CurrentTimeZone.ToLocalTime (new System.DateTime (1970, 1, 1));
			//int res1 = (int)(DateTime.Now.AddDays (0 - days) - startTime1).TotalSeconds;
			//
			var list1 = Resolver.Resolve<ISQLiteClient> ().
				GetAllData<News> (r => r.ColumnId == columnId && r.Focus == false).OrderByDescending (r => r.RankTime).Take (20).ToList ();
			//
		

			return list1;
		}


		public List<News> GetNewsByTheme (int newsId)
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<News> (r => r.ThemeID == newsId);
		}

	}
}

