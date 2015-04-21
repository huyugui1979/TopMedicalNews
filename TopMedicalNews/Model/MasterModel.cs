using System;
using Xamarin.Forms;
using System.Windows.Input;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using Refractored.Xam.Settings;
using MyFormsLibCore.Mvvm;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public class MasterModel:ViewModel
	{
		public MasterModel ()
		{

		}
		public ICommand SelectPicCommand{
			get{ 
				return new Command(()=>{

					MessagingCenter.Send<object> (this, "TakePicture");

				});
			}
		}
		public ICommand MyReadingCommand {
			get { 
				return new Command (() => {
					MessagingCenter.Send<object> (this, "IsPresented");
					Navigation.NavigateTo<MyReadingModel>(initialiser: (m, v) => {
						(m as MyReadingModel).Init();
					});
				});
			}
		}
		public ICommand MyCommentCommand {
			get { 
				return new Command (() => {
					MessagingCenter.Send<object> (this, "IsPresented");
					Navigation.NavigateTo<MyCommentModel> (initialiser:(m, v) => {
						(m as MyCommentModel).Init();
					});
				});
			}
		}
		public ICommand MyCollectionCommand {
			get { 
				return new Command (() => {
					MessagingCenter.Send<object> (this, "IsPresented");
					Navigation.NavigateTo<MyCollectionModel> ( initialiser:(m, v) => {
						(m as MyCollectionModel).Init();
					});
				});
			}
		}
		public ICommand ModifyPasswordCommand {
			get { 
				return new Command (() => {
					MessagingCenter.Send<object> (this, "IsPresented");
					Navigation.NavigateTo<ModifyPasswordModel>();
				
				});
			}
		}
		public ICommand LogoutCommand {
			get { 
				return new Command (() => {
					Resolver.Resolve<IUserService>().LogOut();
					MessagingCenter.Send<object> (this, "LogoutSucceed");
					MessagingCenter.Send<object> (this, "IsPresented");

				});
			}
		}

	}
}

