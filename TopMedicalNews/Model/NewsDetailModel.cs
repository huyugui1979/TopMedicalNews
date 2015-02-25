using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TopMedicalNews
{
	public class CommentData
	{
		public string UserName{ get; set; }

		public string ImageUri{ get; set; }

		public DateTime PublishTime{ get; set; }

		public string Content{ get; set; }
	}

	public class NewsDetailModel:BaseViewModel
	{
		public NewsDetailModel ()
		{
			_Comments = new ObservableCollection<CommentData> ();
		}

		public ICommand AddNewsCollectionCmd {
			get { 
				return new Command (() => {
					int userId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1);
					if (userId == -1) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("请登录系统");
						return;
					}
					if (_IsCollection == false) {
						Resolver.Resolve<ICollectionService> ().AddNewsToCollection (userId, News.ID);
						IsCollection = true;
					} else {
						Resolver.Resolve<ICollectionService> ().DelNewsToCollection (userId, News.ID);
						IsCollection = false;
					}
				});
			}
		}

		bool _IsCollection;

		public bool IsCollection {
			get { 
				int userId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1);
				if (userId == -1) {
					_IsCollection = false;

				} else {
					_IsCollection = Resolver.Resolve<ICollectionService> ().ContainNewsInCollection (userId, News.ID);
				}
				return _IsCollection;
			}set {
				SetProperty (ref _IsCollection, value);
			}
		}

		News _News;

		public News News { get { return _News; } set { SetProperty (ref _News, value); } }

		ObservableCollection<CommentData> _Comments;

		public ObservableCollection<CommentData> Comments {
			get{ return _Comments; }
			set{ SetProperty (ref _Comments, value); }
		}

		//		public override void OnAppearing ()
		//		{
		//
		//			base.OnAppearing ();
		//		}
		public int NewsID{ get; set; }

		public  async void Init ()
		{
			IsBusy = true;
			await Task.Delay (TimeSpan.FromSeconds (1));
			await Task.Factory.StartNew (() => {
				News = Resolver.Resolve<INewsService> ().GetNewById (NewsID);
				var list = Resolver.Resolve<ICommentService> ().GetCommentsByNewsId (NewsID);
				foreach (var c in list) {
					_Comments.Add (c);
				}
			});
			IsBusy = false;

		}
	

	}
}

