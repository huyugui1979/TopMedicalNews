using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Acr.XamForms.UserDialogs;
using Refractored.Xam.Settings;
using System.Threading.Tasks;
using MyFormsLibCore.Ioc;
using MyFormsLibCore.Mvvm;

namespace TopMedicalNews
{
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage ()
		{
			InitializeComponent ();
		}
		protected override void OnAppearing ()
		{
			
			base.OnAppearing ();
			Init ();

		}
		public async void Init ()
		{
			//
			var dialog = Resolver.Resolve<IUserDialogService> ().Loading ("初始化...",null,"Cancel",false);
			try {
				dialog.Show ();
				var b = CrossSettings.Current.GetValueOrDefault<bool> ("MyInit", false);
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

