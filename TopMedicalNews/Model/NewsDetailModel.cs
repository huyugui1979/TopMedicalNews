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
   public class CommentData{
		public string UserName{ get; set; }
		public string ImageUri{get;set;}
		public DateTime PublishTime{get;set;}
		public string Content{get;set;}
	}
	public class NewsDetailModel:ViewModel
	{
		public NewsDetailModel ()
		{

		}
		public ICommand AddNewsCollectionCmd{
			get{ 
				return new Command (() => {
					int userId = Resolver.Resolve<ISettings>().GetValueOrDefault<int>("LoginUserId",-1);
					if(userId == -1)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("请登录系统");
					}else
					{
						Resolver.Resolve<ICollectionService> ().AddNewsToCollection(userId,News.ID);
						Resolver.Resolve<IUserDialogService>().AlertAsync("收藏成功");
					}
				});
			}
		}
		public News News{get;set;}
		public List<CommentData> Comments{get;set;}
		public void Init(int NewsId)
		{
			News = Resolver.Resolve<INewsService> ().GetNewById (NewsId);
			Comments = Resolver.Resolve<ICommentService> ().GetCommentsByNewsId (NewsId);

		}

	}
}

