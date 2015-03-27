using System;
using Refractored.Xam.Settings;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;
using XLabs.Forms.Mvvm;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TopMedicalNews
{
	public class WelcomeModel:BaseViewModel
	{
		public WelcomeModel ()
		{
			
		}

		public async void Init ()
		{
			
			//
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("初始化...");
			try {
				dialog.Show ();
				if (CrossSettings.Current.GetValueOrDefault<bool> ("MyInit", false) == false) {
					Resolver.Resolve<ISQLiteClient> ().Init ();
					await Resolver.Resolve<IColumnService> ().Init ();
					await Resolver.Resolve<IDepartmentService> ().Init ();
					Resolver.Resolve<ISQLiteClient> ().Init ();
					Resolver.Resolve<IFontService> ().Init ();
					CrossSettings.Current.AddOrUpdateValue<bool> ("MyInit", true);
				}
				await Task.Delay(1000);
				App.Current.MainPage = ViewFactory.CreatePage<
						MainModel,Page> () as Page;
			} catch (MyException e) {
				await Resolver.Resolve<IUserDialogService> ().AlertAsync (e.Message);
			} finally {
				dialog.Hide ();
			}

		}
	}
}

