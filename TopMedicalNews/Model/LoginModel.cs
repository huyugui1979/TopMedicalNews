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
			User = Resolver.Resolve<IUserService> ().GetLoginUser ();
		}
		public User User{get;set;}
		public ICommand LoginCommand{
			get{ 
				return new Command (() => {

					bool b = Resolver.Resolve<IUserService> ().CheckUser(User);
					if(b == false)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("登录失败");
					}else
					{
						MessagingCenter.Send<object> (this, "LoginSucceed");
						//

						//
						Navigation.GoBack();
						//
					}
				});
			}
		}
		public ICommand RegisterCommand{
			get{ 
				return new Command (() => {
					Navigation.NavigateTo<RegisterModel> ();
				});
			}
		}
		public ICommand ForgetCommand
		{
			get{ 
				return new Command (() => {
					Navigation.NavigateTo<ForgetModel> ();
				});
			}
		}
	}
}

