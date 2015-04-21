using System.Windows.Input;
using Xamarin.Forms;
using MyFormsLibCore.Ioc;
using MyFormsLibCore;
using Acr.XamForms.Mobile;


namespace TopMedicalNews
{
	public class AboutUsModel:BaseViewModel
	{
		public AboutUsModel ()
		{
		}
		public ICommand GoHomeCommand
		{
			get { 
				return new Command( () => {
					//					var device = Resolver.Resolve<IDevice>();
					#if __ANDROID__
					var uri =  global::Android.Net.Uri.Parse("http://www.iiyi.com");
					var intent = new global::Android.Content.Intent(global::Android.Content.Intent.ActionView,uri);
					intent.AddFlags(global::Android.Content.ActivityFlags.NewTask);
					global::Android.App.Application.Context.StartActivity(intent);
					//Forms.Context.StartActivity(intent);
				
					#endif
					//					device.PhoneService.DialNumber(number);
				});
			}
		}
		public ICommand AddWeiChatCommand{
			get { 
				return new Command<string> ( (number) => {
//					var device = Resolver.Resolve<IDevice>();
//					device.PhoneService.DialNumber(number);
				});
			}
		}
		public ICommand CallPhoneCommand {
			get { 
				return new Command ( () => {
					var device = Resolver.Resolve<IPhoneService>();
					device.Call("医界头条","0756-7770983");
				});
			}
		}
	}
}

