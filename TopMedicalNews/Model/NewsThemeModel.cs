using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using XLabs.Ioc;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class NewsThemeModel:BaseViewModel
	{
		public NewsThemeModel ()
		{
			_NewsList = new ObservableCollection<News> ();
		}
		ObservableCollection<News> _NewsList;
		public ObservableCollection<News> NewsList{ get{ return _NewsList; } set{ SetProperty (ref _NewsList, value); } }
		public News MyNews{get;set;}
		public async void Init(News news)
		{
			//
			MyNews = news;
			IsBusy = true;
			await Task.Delay (TimeSpan.FromSeconds (3));
			IsBusy = false;
			var list = Resolver.Resolve<INewsService> ().GetNewsByTheme (news.ID);
			foreach (var obj in list) {
				_NewsList.Add (obj);
			}
			//
		}
		public ICommand GotoNewsDetailCommand { get { return new Command<News> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> (null, true, (m, p) => {
				(m as NewsDetailModel).Init(r);

			});


		}); } }

	}
}

