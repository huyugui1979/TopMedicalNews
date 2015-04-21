using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Linq;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Ioc;


namespace TopMedicalNews
{
	public class MyCollectionModel:BaseViewModel
	{
		public MyCollectionModel ()
		{
		
			_NewsList = new ObservableCollection<Collection> ();
		}

		ObservableCollection<Collection> _NewsList;

		public ObservableCollection<Collection> NewsList{ get { return _NewsList; } set { SetProperty (ref _NewsList, value); } }

		public  async void Init ()
		{
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载收藏...");
			try {
				dialog.Show();
				var user = Resolver.Resolve<IUserService> ().GetLoginUser ();

				var list = await Resolver.Resolve<ICollectionService> ().ListCollect (user.UserName, user.UID);

				list.ForEach (c => _NewsList.Add (c));

			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
			} finally {
				dialog.Hide ();
			}
		}

		public ICommand GetMoreCommand { get { return new Command (async () => {

				var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载更多收藏...");
				try {

					dialog.Show ();
					var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
					//
					var time = _NewsList.Min (r => r.Add_Time);
					DateTime dt1 = new DateTime (1970, 1, 1);
					TimeSpan ts = time - dt1;

					//
					var ll = await Resolver.Resolve<ICollectionService> ().ListCollect (user.UserName, user.UID, (int)ts.TotalSeconds, 20);
				var difs = ll.ToList ().Except (_NewsList.ToList (), Equality<Collection>.CreateComparer (r => r.ID));
					foreach (var obj in difs) {
						_NewsList.Add (obj);
					}

				} catch (Exception e) {
					Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
				} finally {
					dialog.Hide ();
				}
			}); } }

		public ICommand GotoNewsDetailCommand { get { return new Command<Collection> (async (r) => {

			await Navigation.NavigateTo<NewsDetailModel> ( initialiser:(m, p) => {
			
					//News news = new News{ ID = r.ID, FromSource = r.Stem_From, PublishTime = r.Add_Time, Title = r.Title_Name };
					//Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceData<News> (news);
				(m as NewsDetailModel).Init (r.WZ_ID);

				});


			}); } }
	}
}

