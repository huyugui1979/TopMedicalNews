using System;
using XLabs.Forms.Mvvm;
using Xamarin.Forms;
using System.Windows.Input;
using Refractored.Xam.Settings.Abstractions;
using XLabs.Ioc;

namespace TopMedicalNews
{
	public class MasterModel:ViewModel
	{
		public MasterModel ()
		{

		}
		public ICommand MyReadingCommand {
			get { 
				return new Command (() => {

				});
			}
		}
		public ICommand MyCommentCommand {
			get { 
				return new Command (() => {

				});
			}
		}
		public ICommand MyCollectionCommand {
			get { 
				return new Command (() => {

				});
			}
		}
		public ICommand ModifyPasswordCommand {
			get { 
				return new Command (() => {

				});
			}
		}
		public ICommand LogoutCommand {
			get { 
				return new Command (() => {
					Resolver.Resolve<ISettings>().AddOrUpdateValue <int> ("LoginUserId", -1);
					MessagingCenter.Send<object> (this, "LogoutSucceed");

				});
			}
		}

	}
}

