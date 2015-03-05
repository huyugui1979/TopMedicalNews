using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Threading.Tasks;
using XLabs.Ioc;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{
	public class MyReadingModel:BaseViewModel
	{

		public MyReadingModel ()
		{

			_NewsList = new ObservableCollection<News> ();
		}
		ObservableCollection<News> _NewsList;
		public ObservableCollection<News> NewsList{ get{ return _NewsList; } set{ SetProperty (ref _NewsList, value); } }
		public News MyNews{get;set;}
		public  void Init()
		{
			//
			int userId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1);
			var list = Resolver.Resolve<IReadingService> ().GetMyReading (userId);
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

