using System;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{
	public class ModifyPasswordModel:BaseViewModel
	{
		public ModifyPasswordModel ()
		{
		}

		public string OldPassword{ get; set; }

		public string NewPassword{ get; set; }

		public string RepPassword{ get; set; }

		public ICommand OkCommand {
			get { 
				return new Command (async () => {
					//
					if (NewPassword != RepPassword) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("前后密码不符");
						return;
					}
					var dialog =  Resolver.Resolve<IUserDialogService> ().Loading("正在修改密码");
					try {
						dialog.Show();
						var user = Resolver.Resolve<IUserService> ().GetLoginUser ();
						await Resolver.Resolve<IUserService> ().ModifyPassword (user.UID, OldPassword, NewPassword);
						await Resolver.Resolve<IUserDialogService> ().AlertAsync("更改密码成功");
						Navigation.PopToRoot();
					} catch (MyException e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message);
		
					} catch (Exception e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");

					}finally{
						dialog.Hide();
					}
					//
				});
			}
		}
	}
}
   
