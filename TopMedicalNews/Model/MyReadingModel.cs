using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Linq;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Ioc;


namespace TopMedicalNews
{
	public class MyReadingModel:BaseViewModel
	{

		public MyReadingModel ()
		{

			_NewsList = new ObservableCollection<Reading> ();
		}

		ObservableCollection<Reading> _NewsList;

		public ObservableCollection<Reading> NewsList{ get { return _NewsList; } set { SetProperty (ref _NewsList, value); } }

		public News MyNews{ get; set; }

		public async void Init ()
		{
			//
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载阅读...");
			try {
				dialog.Show();
				var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
				var list = await Resolver.Resolve<IReadingService> ().ListRecord (user.UserName, user.UID);
				foreach (var obj in list) {
					_NewsList.Add (obj);
				}
			} catch (Exception e) {
				
			} finally {
				dialog.Hide ();
			}
			//
		}
		//

		//
		public ICommand GetMoreCommand { get { return new Command (async () => {

				var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载更多阅读");
				try {
					
					dialog.Show ();
					
					var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
					var reading = _NewsList.Last ();
					var list = await Resolver.Resolve<IReadingService> ().ListRecord (user.UserName, user.UID, reading.Wz_Time, 20);
					var difs = list.ToList ().Except (_NewsList.ToList (), Equality<Reading>.CreateComparer (r => r.Wz_Id));

					foreach (var obj in difs) {
						_NewsList.Add (obj);
					}
				} catch (Exception e) {
					Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
				} finally {
					dialog.Hide ();
				}
			}); } }
		//
		public ICommand GotoNewsDetailCommand { get { return new Command<Reading> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> ( initialiser:(m, p) => {
						(m as NewsDetailModel).Init (r.Wz_Id);
				});
				

			}); } }
	}
}

