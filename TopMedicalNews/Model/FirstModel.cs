using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using Refractored.Xam.Settings.Abstractions;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;

using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Acr.XamForms.UserDialogs;
using MyFormsLibCore.Ioc;

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
		ObservableCollection<Column> _LikeColumns;

		public  ObservableCollection<Column>  	LikeColumns { get { return _LikeColumns; } set { SetProperty (ref _LikeColumns, value); } }



		public ICommand SettingCommand { get { return new Command (r => {
			Navigation.NavigateTo<SettingModel> ();	
			}); } }

		public ICommand OrderColumnCommand { get { return new Command (r => {
			Navigation.NavigateTo<SelectColumnModel> ();
			}); } }
		//
	
		int _SelectIndex=0;


	public int SelectIndex{get{ return _SelectIndex; }set{ SetProperty (ref _SelectIndex, value); }}
		//
		public ICommand SelectColumnCommand {
			get {
				return new Command<int> (n => {
					//
					SelectIndex=n;
					//
				});
			}
			
		}
		//
		public FirstModel ()
		{

			//
			LikeColumns = new ObservableCollection<Column> ();
			//
			Resolver.Resolve<ILikeColumnService> ().GetLikeColumns ().ForEach (
				c => LikeColumns.Add (c)
			);
			//

			MessagingCenter.Subscribe<object> (this, "ClickLogin", sender => {
				//
				Navigation.NavigateTo<LoginModel> ();	
				//
			});
			MessagingCenter.Subscribe<object,Tuple<int,int>> (this, "ChangeLikeColumnsOrder", (obj, t) => {
				//
				LikeColumns.Move(t.Item1,t.Item2);

				//

			});
			MessagingCenter.Subscribe<object,int> (this, "DeleteLikeColumn",(obj,pos) => {
				//
				LikeColumns.RemoveAt(pos);

				//

			});
			MessagingCenter.Subscribe<object,Column> (this, "InsertLikeColumn", (obj,c) => {
				//
				LikeColumns.Add(c);

				//

			});
			MessagingCenter.Subscribe<object> (this, "setcolumn", obj => {
				SelectIndex=0;
			});
//			MessagingCenter.Subscribe<object> (this, "GetLikeColumns", sender => {
//				LikeColumns = Resolver.Resolve<ILikeColumnService> ().GetLikeColumns ();
//			});

		}

	}
}

