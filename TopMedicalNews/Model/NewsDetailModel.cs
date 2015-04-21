using System;
using TopMedicalNews.Model;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Refractored.Xam.Settings;
using System.Threading;
using System.Linq;
using MyFormsLibCore.Ioc;

#if __ANDROID__
using CN.Sharesdk.Framework;
using CN.Sharesdk.Onekeyshare;
using TopMedicalNews.Android;
#endif
namespace TopMedicalNews
{


	public class NewsDetailModel:BaseViewModel
	{
		public NewsDetailModel ()
		{
			_Comments = new ObservableCollection<Comment> ();
			MessagingCenter.Subscribe<object> (this, "PullUp", obj => {
				this.GetMoreCommand.Execute(null);
			});
		}
		//

		//

		public ICommand GetMoreCommand { get { return new Command (async () => {

			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("加载更多评论");
			try {

				dialog.Show ();

				var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
				var reading = _Comments.Last ();
				var list = await Resolver.Resolve<ICommentService> ().DownloadComment(News.ID);
				var difs = list.ToList ().Except (_Comments.ToList (), Equality<Comment>.CreateComparer (r =>r.ID));

				foreach (var obj in difs) {
					_Comments.Add (obj);
				}
			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
			} finally {
				dialog.Hide ();
			}
		}); } }
		//

		public ICommand AddCommentCommand {
			get { 
				return new Command (async () => {
					//
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("增加评论...");

					try {
						dialog.Show();
						var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
						if (user == null) {
							Navigation.NavigateTo<LoginModel> ();
						
						} else {
							await Resolver.Resolve<ICommentService> ().AddComment (user.UserName, user.UID, News.ID, MyComment);
							var list = await Resolver.Resolve<ICommentService> ().DownloadComment (News.ID);
							_Comments.Clear ();
							foreach (var c in list) {
								_Comments.Add (c);
							}
						}
					} catch (Exception e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
					}finally{
						dialog.Hide();
					}
					//
				});
			}
		}

		public ICommand ShareCmd {
			get { 
				return new Command (async () => {
//					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("准备分享...");
//					dialog.Show();
//					await Task.Factory.StartNew(()=>{
//					MessagingCenter.Send<object>(this,"Share");
//					});
//					dialog.Hide();
					#if __ANDROID__
					showShare ();
					#endif
				});
			}
		}
		#if __ANDROID__
		private void showShare ()
		{
			ShareSDK.InitSDK (Forms.Context);
			OnekeyShare oks = new OnekeyShare ();
			//oks.DisableSSOWhenAuthorize(); 
			oks.SetNotification (Resource.Drawable.ic_launcher, "医界头条");
			// title标题，印象笔记、邮箱、信息、微信、人人网和QQ空间使用
			oks.SetTitle (_News.Title);
			// text是分享文本，所有平台都需要这个字段
			oks.Text = _News.Thumb;
			// imagePath是图片的本地路径，Linked-In以外的平台都支持此参数
			var path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
//
//
			Image image;

			oks.SetImagePath(path+"/Share.png");//确保SDcard下面存在此张图片
		
			// url仅在微信（包括好友和朋友圈）中使用
			oks.SetUrl("http://app.iiyi.com/yijietoutiao.html");
			//
			oks.Show (Forms.Context);
			//

		}
		#endif
		public ICommand AddNewsCollectionCmd {
			get { 
				return new Command (async () => {
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在收藏...");

					try {
						dialog.Show();
						var userId = Resolver.Resolve<IUserService> ().GetLoginUser ();
						if (userId == null) {
							//Resolver.Resolve<IUserDialogService> ().AlertAsync ("请登录系统");
							Navigation.NavigateTo<LoginModel> ();
							return;
						}
						if (News.Collect == false) {
							await Resolver.Resolve<ICollectionService> ().AddCollect (userId.UserName, userId.UID, News.ID, News.Title);
							News.Collect = true;

						} else {
							await Resolver.Resolve<ICollectionService> ().DelCollect (userId.UserName, userId.UID, News.ID);
							News.Collect = false;

						}
					} catch (Exception e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
					}finally
					{
						dialog.Hide();
					}
				});
			}
		}

		//		bool _IsCollection;
		//
		//		public bool IsCollection {
		//			get {
		//				var userId = Resolver.Resolve<IUserService> ().GetLoginUser ();
		//				if (userId == null) {
		//					_IsCollection = false;
		//
		//				} else {
		//					int id = userId.UID;
		//					int id1 = News.ID;
		//					_IsCollection = Resolver.Resolve<ICollectionService> ().ContainNewsInCollection (userId.UID, News.ID);
		//				}
		//				return _IsCollection;
		//			}set {
		//				SetProperty (ref _IsCollection, value);
		//			}
		//		}

		News _News;

		public News News { get { return _News; } set { SetProperty (ref _News, value); } }

		ObservableCollection<Comment> _Comments;

		public ObservableCollection<Comment> Comments {
			get{ return _Comments; }
			set{ SetProperty (ref _Comments, value); }
		}
		//
		string myComment;

		public string MyComment{ get { return myComment; } set { SetProperty (ref myComment, value); } }
		//
		HtmlWebViewSource source;

		public HtmlWebViewSource Source{ get { return source; } set { SetProperty (ref source, value); } }

		public  async void Init (int  news_id)
		{
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在下载...");

			try {

				dialog.Show();
				var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
				//
				if (user != null) {
					News = await Resolver.Resolve<INewsService> ().DownloadDetail (news_id, user.UserName, user.UID);
					await Resolver.Resolve<IReadingService> ().AddRecord (user.UserName, user.UID, News.ID, News.Title);

				} else {
					News = await Resolver.Resolve<INewsService> ().DownloadDetail (news_id);
				}

				var list = await Resolver.Resolve<ICommentService> ().DownloadComment (News.ID);
				foreach (var c in list) {
					_Comments.Add (c);
				}
				Source = new HtmlWebViewSource{ Html = News.Content };
			} catch (Exception e) {
				Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
			} finally {
				dialog.Hide ();
			}
			//
		}
	

	}
}

