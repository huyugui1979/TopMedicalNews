using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.XamForms.UserDialogs;
using System.Threading.Tasks;
using MyFormsLibCore.Ioc;


namespace TopMedicalNews
{
	public class MyNewsListModel:BaseViewModel
	{
		public MyNewsListModel ()
		{
			FocusNews = new ObservableCollection<News> ();
			SelectNews = new ObservableCollection<News> ();
			//
			MessagingCenter.Subscribe<Column> (this, "ColumnSelect", async (column) => {
				if (_column.ID == column.ID) {

					List<News> newsList1 = null, newsList2 = null;

					if (SelectNews.Count == 0) {
							newsList1 = Resolver.Resolve<INewsService> ().GetNews (column.ID);

						var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在加载新闻...");
						dialog.Show();
						await Task.Factory.StartNew(()=>{
						newsList1.ForEach (
							nn => SelectNews.Add (nn)
							);});
						dialog.Hide();
						if (newsList1.Count == 0) {
							UpdateNews ();
						}

					}  
					if (FocusNews.Count == 0) {
						newsList2 = Resolver.Resolve<INewsService> ().GetFocusNews (column.ID);

						newsList2.ForEach (
							nn => FocusNews.Add (nn)
						);
						if (FocusNews.Count > 0)
						{
							SelectedFocusNews = FocusNews [0];
							HaveSelectedFocusNews=true;
						}
					}
					
				
				}

			});
			//
		}

		Column _column;

		public  void Init (Column column)
		{
			_column = column;
		}
		//
		ObservableCollection<News> _FocusNews;
		//
		public  ObservableCollection<News>  FocusNews { get { return _FocusNews; } set { SetProperty (ref _FocusNews, value); } }

		//
		ObservableCollection<News> _SelectNews;

		public ObservableCollection<News>   SelectNews{ get { return _SelectNews; } set { SetProperty (ref _SelectNews, value); } }
		//
		News _SelectedFocusNews;

		public News  		SelectedFocusNews { get { return _SelectedFocusNews; } set { SetProperty (ref _SelectedFocusNews, value); } }
		//
		bool _HaveSelectedFocusNews=false;
		public  bool HaveSelectedFocusNews{ get { return _HaveSelectedFocusNews; } set { SetProperty (ref _HaveSelectedFocusNews, value); } }
		async void UpdateNews ()
		{
				try {

				IsRefreshing = true;

				System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime (new System.DateTime (1970, 1, 1));
				int time = (int)(DateTime.Now - startTime).TotalSeconds;
				List<News> news = await Resolver.Resolve<INewsService> ().DownloadNews (_column.ID, time);
				//
				var list1 = news.Where (n => n.Focus == false).Except (SelectNews.ToList (), Equality<News>.CreateComparer (r => r.ID)).ToList ();
				var list2 = news.Where (n => n.Focus == true).Except (FocusNews.ToList (), Equality<News>.CreateComparer (r => r.ID)).ToList ();
				list2.Where (r => r.Focus == true).ToList ().ForEach (n => {

					FocusNews.Add (n);

				});
				list1.Where (r => r.Focus == false).ToList ().ForEach (n => {
					SelectNews.Add (n);
				});
				if (FocusNews.Count > 0)
				{
					SelectedFocusNews = FocusNews [0];
					HaveSelectedFocusNews=true;
				}
					
			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
			} finally {
				IsRefreshing = false;
			
			}
		}




		bool _IsRefreshing = false;

		public bool IsRefreshing {
			get{ return _IsRefreshing; }
			set{ SetProperty (ref _IsRefreshing, value); }
		}

		bool _RequestMoring = false;

		public bool RequestMoring {
			get{ return _RequestMoring; }
			set{ SetProperty (ref _RequestMoring, value); }
		}

		public ICommand RequestMoreCommand {
			get {
				return new Command (async () => {
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载更多新闻...");
					try {
						dialog.Show ();
						RequestMoring = true;
						//

						int time = SelectNews.Last ().RankTime;
						List<News> news = await Resolver.Resolve<INewsService> ().DownloadNews (_column.ID, time);
						//
						if(news.Count == 0)
						{
							dialog.Hide();
							Resolver.Resolve<IUserDialogService> ().Toast("已经没有更多新闻了",1);
							return;
						}
						Device.BeginInvokeOnMainThread (() => {
							news.Where (r => r.Focus == false).ToList ().ForEach (r => {
								SelectNews.Add (r);
				
							});
						});
						dialog.Hide ();
					} catch (Exception e) {
						dialog.Hide ();
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
					} finally {
						RequestMoring = false;


					}
					//
					//
				});
			}
		}

		public ICommand RefreshCommand {
			get {
				return new Command (() => {
					UpdateNews ();
					//
				});
			}
		}

		public ICommand SelectFocusNewsCommand {

			get {

				return new Command (async () => {
					if (SelectedFocusNews.Type == "topic")
						Navigation.NavigateTo<NewsThemeModel>(initialiser:(m, p) => {
							(m as NewsThemeModel).Init (SelectedFocusNews);

						});
					else
						Navigation.NavigateTo<NewsDetailModel> (initialiser:(m, p) => {
							(m as NewsDetailModel).Init (SelectedFocusNews.ID);

						});


				});
			} 
		}

		public ICommand GotoThemeCommand {
			get {
				return new Command<News> (async (r) => {

					await Navigation.NavigateTo<NewsThemeModel> ( initialiser:(m, p) => {
						(m as NewsThemeModel).Init (r);

					});


				});
			} 
		}

		public ICommand GotoNewsDetailCommand { get { return new Command<News> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> ( initialiser:(m, p) => {
					(m as NewsDetailModel).Init (r.ID);

				});


			}); } }
		//

	}
}

