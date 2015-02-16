using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{
	public class CommentData
	{
		public string UserName{ get; set; }

		public string ImageUri{ get; set; }

		public DateTime PublishTime{ get; set; }

		public string Content{ get; set; }
	}

	public class NewsDetailModel:ViewModel
	{
		public NewsDetailModel ()
		{

		}

		public ICommand AddNewsCollectionCmd {
			get { 
				return new Command (() => {
					int userId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("LoginUserId", -1);
					if (userId == -1) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("请登录系统");
						return ;
					}
					if (_IsCollection == false) {
							Resolver.Resolve<ICollectionService> ().AddNewsToCollection (userId, News.ID);
							IsCollection=true;
					}else{
						Resolver.Resolve<ICollectionService> ().DelNewsToCollection (userId, News.ID);
						IsCollection=false;
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

		public News News{ get; set; }

		public List<CommentData> Comments{ get; set; }

		public void Init (int NewsId)
		{
			News = Resolver.Resolve<INewsService> ().GetNewById (NewsId);
			Comments = Resolver.Resolve<ICommentService> ().GetCommentsByNewsId (NewsId);

		}
	

	}
}

