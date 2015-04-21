using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;

using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public class NewsThemeModel:BaseViewModel
	{
		public NewsThemeModel ()
		{
			_NewsList = new ObservableCollection<News> ();
		}

		ObservableCollection<News> _NewsList;

		public ObservableCollection<News> NewsList{ get { return _NewsList; } set { SetProperty (ref _NewsList, value); } }

		public News MyNews{ get; set; }
	
		public async void Init (News news)
		{
			//
			MyNews = news;
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载新闻");

			try {
				dialog.Show ();
				var list = await Resolver.Resolve<INewsService> ().DownloadTopicNews (news.ID);
				foreach (var obj in list) {
					_NewsList.Add (obj);
				}
			} catch (MyException e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message);
			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
			} finally {
				dialog.Hide ();
			}
			//
		}

		public ICommand GotoNewsDetailCommand { get { return new Command<News> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> ( initialiser:(m, p) => {
				(m as NewsDetailModel).Init (r.ID);

				});


			}); } }

	}
}

