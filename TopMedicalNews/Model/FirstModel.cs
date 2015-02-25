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

	public class FirstModel:BaseViewModel
	{
		List<Column> _LikeColumns;

		public  List<Column>  	LikeColumns { get { return _LikeColumns; } set { SetProperty (ref _LikeColumns, value); } }

		//
		ObservableCollection<News> _FocusNews;
		//
		public  ObservableCollection<News>  FocusNews { get { return _FocusNews; } set { SetProperty (ref _FocusNews, value); } }

		//
		ObservableCollection<News> _SelectNews;

		public ObservableCollection<News>   SelectNews{ get { return _SelectNews; } set { SetProperty (ref _SelectNews, value); } }
		//
		News _SelectedFocusNews;

		public News  		SelectedFocusNews { get { return _SelectedFocusNews; } set { SetProperty (ref _SelectedFocusNews, value); } }
		//
	
		int _SelectedColumnIndex;

		public int SelectedColumnIndex {
			get{ return _SelectedColumnIndex; }
			set {
				SetProperty (ref _SelectedColumnIndex, value);	
				Task.Factory.StartNew (async () => {
					IsRefreshing = true;

					await Task.Delay (TimeSpan.FromSeconds (3));
					Xamarin.Forms.Device.BeginInvokeOnMainThread(()=>{
					ClearData ();

					LoadData ();
					IsRefreshing = false;});
				});

			}
		}


		void ClearData ()
		{
			SelectedFocusNews = null;
			FocusNews.Clear ();
			SelectNews.Clear ();

			GC.Collect ();
		}

	

		void LoadData ()
		{
		
			var newsList = Resolver.Resolve<INewsService> ().GetFocusNews (LikeColumns [SelectedColumnIndex].ID);
			foreach (var news in newsList) {
				FocusNews.Add (news);
			}
			newsList = (Resolver.Resolve<INewsService> ().GetSelectNews (LikeColumns [SelectedColumnIndex].ID));
			foreach (var news in newsList) {
				SelectNews.Add (news);
			}
			//
			if (FocusNews.Count == 0)
				FocusNewsNotEmpty = false;
			else {
				FocusNewsNotEmpty = true;
				SelectedFocusNews = FocusNews [0];
			}
			//
		}

		bool _FocusNewsNotEmpty = false;

		public bool FocusNewsNotEmpty {
			get{ return _FocusNewsNotEmpty; }
			set{ SetProperty (ref _FocusNewsNotEmpty, value); }
		}

		public FirstModel ()
		{

			FocusNews = new ObservableCollection<News> ();
			SelectNews = new ObservableCollection<News> ();

			LikeColumns = Resolver.Resolve<IColumnService> ().GetLikeColumns ();

		
			MessagingCenter.Subscribe<object> (this, "ClickLogin", sender => {
				//
				Navigation.NavigateTo<LoginModel> ();	
				//
			});

		}

		public ICommand SettingCommand { get { return new Command (r => {
				Navigation.NavigateTo<SettingModel> ();	
			}); } }

		public ICommand OrderColumnCommand { get { return new Command (r => {
				Navigation.NavigateTo<SelectColumnModel> ();
			}); } }

		bool _IsRefreshing = false;

		public bool IsRefreshing {
			get{ return _IsRefreshing; }
			set{ SetProperty (ref _IsRefreshing, value); }
		}

		bool _RequestMoring = false;

		public bool RequestMoring {
			get{ return _RequestMoring; }
			set{ SetProperty (ref _RequestMoring, value); }
		}

		public ICommand RequestMoreCommand {
			get {
				return new Command (async () => {
					RequestMoring = true;

					await Task.Delay (TimeSpan.FromSeconds (3));
					//
					if (_SelectNews.Count < 15)
						LoadData ();
					RequestMoring = false;

				});
			}
		}

		public ICommand RefreshCommand {
			get {
				return new Command (async () => {
					IsRefreshing = true;
					await Task.Delay (TimeSpan.FromSeconds (3));
					//
					if (_SelectNews.Count < 15)
						LoadData ();
					IsRefreshing = false;
					//
				});
			}
		}

		public ICommand GotoNewsDetailCommand { get { return new Command<News> (async (r) => {
	
				await Navigation.NavigateTo<NewsDetailModel> (null, true, (m, p) => {
					(m as NewsDetailModel).NewsID = r.ID;
				});

			    
			}); } }
	

	
	}
}

