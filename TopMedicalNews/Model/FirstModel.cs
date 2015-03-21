using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using Refractored.Xam.Settings.Abstractions;
using System.Collections.ObjectModel;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using Xamarin.Forms;
using XLabs.Data;
using System.Windows.Input;
using XLabs.Forms.Behaviors;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{

	//	public class LikeColumnModel:ObservableObject
	//	{
	//		public Column Column { get; set; }
	//
	//		bool _Selected;
	//
	//		public bool Selected { get { return _Selected; } set { SetProperty (ref _Selected, value); } }
	//
	//	}
	public static class Equality<T>
	{
		public static IEqualityComparer<T> CreateComparer<V> (Func<T, V> keySelector)
		{
			return new CommonEqualityComparer<V> (keySelector);
		}

		public static IEqualityComparer<T> CreateComparer<V> (Func<T, V> keySelector, IEqualityComparer<V> comparer)
		{
			return new CommonEqualityComparer<V> (keySelector, comparer);
		}

		class CommonEqualityComparer<V> : IEqualityComparer<T>
		{
			private Func<T, V> keySelector;
			private IEqualityComparer<V> comparer;

			public CommonEqualityComparer (Func<T, V> keySelector, IEqualityComparer<V> comparer)
			{
				this.keySelector = keySelector;
				this.comparer = comparer;
			}

			public CommonEqualityComparer (Func<T, V> keySelector)
				: this (keySelector, EqualityComparer<V>.Default)
			{
			}

			public bool Equals (T x, T y)
			{
				return comparer.Equals (keySelector (x), keySelector (y));
			}

			public int GetHashCode (T obj)
			{
				return comparer.GetHashCode (keySelector (obj));
			}
		}
	}

	public class FirstModel:BaseViewModel
	{
		List<Column> _LikeColumns;

		public  List<Column>  	LikeColumns { get { return _LikeColumns; } set { SetProperty (ref _LikeColumns, value); } }



		public ICommand SettingCommand { get { return new Command (r => {
			Navigation.NavigateTo<SettingModel> ();	
		}); } }

		public ICommand OrderColumnCommand { get { return new Command (r => {
			Navigation.NavigateTo<SelectColumnModel> ();
		}); } }
		//


		//
		public FirstModel ()
		{

			//
			LikeColumns = Resolver.Resolve<ILikeColumnService> ().GetLikeColumns ();
			MessagingCenter.Subscribe<object> (this, "ClickLogin", sender => {
				//
				Navigation.NavigateTo<LoginModel> ();	
				//
			});
			MessagingCenter.Subscribe<object> (this, "GetLikeColumns", sender => {
				LikeColumns = Resolver.Resolve<ILikeColumnService> ().GetLikeColumns ();
			});

		}

	}
}

