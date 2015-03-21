using System;
using XLabs.Forms.Mvvm;
using TopMedicalNews.Model;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;
using System.Threading.Tasks;

namespace TopMedicalNews
{
	public class LoginModel:ViewModel
	{
		public LoginModel ()
		{

		}

		public string UserName{ get; set; }

		public string Password{ get; set; }

		public ICommand LoginCommand {
			get { 
				return new Command (async () => {
					var dialog =  Resolver.Resolve<IUserDialogService> ().Loading("正在登录...");
					try {
						dialog.Show();
						await Resolver.Resolve<IUserService> ().CheckUser (UserName, Password, 1);
						MessagingCenter.Send<object> (this, "LoginSucceed");
						Navigation.PopToRoot ();
						//
					} catch (MyException e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message);
					} catch (Exception e) {
						//
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("网络连接错误");
						//
					}finally{
						dialog.Hide();
					}
				});
			}
		}

		public ICommand RegisterCommand {
			get { 
				return new Command (() => {
					Navigation.NavigateTo<RegisterModel> ();
				});
			}
		}

		public ICommand ForgetCommand {
			get { 
				return new Command (() => {
					Navigation.NavigateTo<ForgetModel> ();
				});
			}
		}
	}
}

