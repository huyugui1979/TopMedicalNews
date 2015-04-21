using System;
using System.Windows.Input;
using Xamarin.Forms;
using TopMedicalNews.Model;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Mvvm;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public class RegisterModel:BaseViewModel
	{
		public RegisterModel ()
		{
			MessagingCenter.Subscribe<Department> (this, "SelectDepartment", d => {
				selectDepartment = d;
				Department = d.ParentTitle + " - " + d.Title;
			});
			AgreeRule = false;
		}

		Department selectDepartment=null;
		string department = "选择科室";

		public string Department{ get { return department; } set { SetProperty (ref department, value); } }
		public string UserName{get;set;}
		public string Password{ get; set; }
		public string Phone{ get; set; }
		public bool   AgreeRule{ get; set; }
		public ICommand RegisterCommand {
		
			get {
				return new Command (async () => {
					if(System.Text.RegularExpressions.Regex.IsMatch(UserName,@"(^[\u4e00-\u9fa5a-zA-Z]{1}[\w]{1,29}$|^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$)")==false )
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("输入正确用户名,用户名不能以数字开头,不能包含系统关键字4-15个字母,数字(2-7个汉字),不支持特殊符号(除下划线)用来接收激活码");
						return ;
					}
					if(Password.Length<6)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("输入正确密码,密码限制6〜20位，支持数字,字母及特殊字符");
						return ;
					}
					if(Department == "选择科室")
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("请选择科室");
						return ;
					}
					if(System.Text.RegularExpressions.Regex.IsMatch(Phone,@"^[1]+[3,4,5,8]+\d{9}")==false)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("输入正确的手机号码");

						return ;
					}
					if(AgreeRule == false)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("请确认接受《爱爱医服务条款》");
						return;
					}
					var dialog =  Resolver.Resolve<IUserDialogService> ().Loading("正在注册...");
					try{
						dialog.Show();
						await Resolver.Resolve<IUserService> ().RegisterUser(UserName,Password,Phone,selectDepartment.ParentId,selectDepartment.Id);
						await Resolver.Resolve<IUserService> ().CheckUser(UserName,Password,1);
						await Resolver.Resolve<IUserDialogService>().ConfirmAsync("注册成功,转入登录界面");
						Navigation.PopToRoot();
					}catch(MyException e)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync(e.Message);

					}catch(Exception e)
					{
						Resolver.Resolve<IUserDialogService>().AlertAsync("网络连接错误");
					}finally
					{
						dialog.Hide();
					}
				});
			}
		}
		public ICommand ClickRuleCommand{ get { return new Command (() => {
			//
			Navigation.NavigateTo<RegisterRuleModel>(); 
			//
			}); } }
		public ICommand SelectDepartmentCommand{ get { return new Command (() => {

			Navigation.NavigateTo<SelectDepartmentModel> (initialiser:(m, v) => {

					(m as SelectDepartmentModel).Init ();
				});
			}); } }
	}
}

