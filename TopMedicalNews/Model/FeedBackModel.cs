using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{
	public class FeedBackModel:BaseViewModel
	{
		public FeedBackModel ()
		{
			FeedBackTypeValues = new List<string>{ "其他", "投拆举报", "错误汇报", "广告查询", "站务合作", "热点问题", "移动版", "医界头条" };
			FeedBackType = 0;

		}

		public int FeedBackType{ get; set; }

		public string Question{ get; set; }

		public string Epg{ get; set; }

		public List<string> FeedBackTypeValues{ get; set; }

		public ICommand SendCommand {
			get { 
				return new Command (async () => {
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在提交...");
					try {
						dialog.Show ();
						var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
						if (user == null)
							Resolver.Resolve<ISoftService> ().FeedBack (Question, Epg, FeedBackType);
						else
							Resolver.Resolve<ISoftService> ().FeedBack (Question, Epg, FeedBackType, user.UserName, user.UID);
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("谢谢你的提交");
						Navigation.PopToRoot ();
					} catch (Exception e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
					} finally {
						dialog.Hide ();
					}
				});
			}

		}
	}
}

