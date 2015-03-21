using System;
using XLabs.Forms.Mvvm;
using Xamarin.Forms;
using System.Windows.Input;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{
	public class ForgetModel:ViewModel
	{
		public ForgetModel ()
		{
		}
		private string _recvVcode;
		public ICommand GetVCodeCommand {
			get { 
				return new Command (async () => {
					if(System.Text.RegularExpressions.Regex.IsMatch(Phone,@"^[1]+[3,4,5,8]+\d{9}")==false)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("输入正确的手机号码");

						return ;
					}
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在发送验证码到你手机...");
					try {
						dialog.Show();
						_recvVcode= await Resolver.Resolve<IUserService> ().SendCode (Phone);
						await Resolver.Resolve<IUserDialogService> ().AlertAsync ("验证码已发送到你手机,请检查"); 
					}catch(MyException e)
					{
						Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message);
					}
					catch (Exception e) {
						//
						Resolver.Resolve<IUserDialogService> ().AlertAsync ("连接网络失败");
						//
					}finally
					{
						dialog.Hide();
					}
				});
			}
		}

		public string UserName{ get; set; }

		public string Phone{ get; set; }

		public string Password{ get; set; }

		public string VCode{ get; set; }

		public ICommand ResetCommand {
			get { 
				return new Command (async () => {
					var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("正在重设密码...");

					try {
						dialog.Show ();
						await Resolver.Resolve<IUserService> ().Reset (UserName, Password, VCode);
						await Resolver.Resolve<IUserDialogService> ().AlertAsync ("更改成功"); 
						Navigation.PopToRoot ();
					} catch (MyException e) {
						Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message); 
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

