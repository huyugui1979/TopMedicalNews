using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Refractored.Xam.Settings.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using TopMedicalNews.Model;
using Refractored.Xam.Settings;
using System.Linq;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public class MyCommentModel:BaseViewModel
	{
		public MyCommentModel ()
		{
			_CommentsList = new ObservableCollection<Reading> ();
		}

		ObservableCollection<Reading> _CommentsList;

		public ObservableCollection<Reading> CommentsList{ get { return _CommentsList; } set { SetProperty (ref _CommentsList, value); } }

		public async void Init ()
		{
			//
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载评论...");
			try {
				dialog.Show ();
				var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
				var list = await Resolver.Resolve<ICommentService> ().ListMeComment (user.UserName, user.UID);
				foreach (var obj in list) {
					_CommentsList.Add (obj);
				}
			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
			} finally {
				dialog.Hide ();
			}

			//
		}

		public ICommand GetMoreCommand { get { return new Command (async () => {

				var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载更多评论...");
				try {
					
					dialog.Show ();
					var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
					//
					var time = _CommentsList.Min (r => r.Add_Time);
					DateTime dt1 = new DateTime (1970, 1, 1);
					TimeSpan ts = time - dt1;

					//
					var ll = await Resolver.Resolve<ICommentService> ().ListMeComment (user.UserName, user.UID, (int)ts.TotalSeconds, 20);
				var difs = ll.ToList ().Except (_CommentsList.ToList (), Equality<Reading>.CreateComparer (r => r.Wz_Id));
					foreach (var obj in difs) {
						_CommentsList.Add (obj);
					}
					
				} catch (Exception e) {
					Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
				} finally {
					dialog.Hide ();
				}
			}); } }

		public ICommand GotoNewsDetailCommand { get { return new Command<Reading> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> ( initialiser:(m, p) => {
					//var news = Resolver.Resolve<INewsService> ().GetNewById (r.NewsID);
				(m as NewsDetailModel).Init (r.Wz_Id);

				});


			}); } }
	}
}

