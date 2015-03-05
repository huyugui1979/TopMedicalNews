using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class MyCommentModel:BaseViewModel
	{
		public MyCommentModel ()
		{
		}
		ObservableCollection<CommentData> _CommentsList;
		public ObservableCollection<CommentData> CommentsList{ get{ return _CommentsList; } set{ SetProperty (ref _CommentsList, value); } }
		public async void Init()
		{
			//

			IsBusy = true;
			await Task.Delay (TimeSpan.FromSeconds (3));

			int userId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1);
			var list = Resolver.Resolve<ICommentService> ().GetMyComments (userId);
			foreach (var obj in list) {
				_CommentsList.Add (obj);
			}
			IsBusy = false;
			//
		}
		public ICommand GotoNewsDetailCommand { get { return new Command<CommentData> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> (null, true, (m, p) => {
				var news = Resolver.Resolve<INewsService> ().GetNewById(r.NewsID);
				(m as NewsDetailModel).Init(news);

			});


		}); } }
	}
}

