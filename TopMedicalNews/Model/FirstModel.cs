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

	public class FirstModel:ViewModel
	{
		List<Column> _LikeColumns;
		public  List<Column>  	LikeColumns { get { return _LikeColumns; } set { SetProperty (ref _LikeColumns, value); } }

		//
		List<News>   _FocusNews;
		//
		public  List<News>  FocusNews { get { return _FocusNews; } set { SetProperty (ref _FocusNews, value); } }

		//
		 List<News>   _SelectNews;
		public List<News>   SelectNews{ get { return _SelectNews; } set { SetProperty (ref _SelectNews, value); } }
		//
		News _SelectedFocusNews;

		public News  		SelectedFocusNews { get { return _SelectedFocusNews; } set { SetProperty (ref _SelectedFocusNews, value); } }
		//
	
		//

		public ICommand SelectColumnCommand{ get { return new Command<Column> (r => {

				FocusNews = Resolver.Resolve<INewsService> ().GetFocusNews (r.ID);
				SelectNews = Resolver.Resolve<INewsService> ().GetSelectNews (r.ID);
				SelectedFocusNews = FocusNews [0];
				
			}); } }

		public FirstModel ()
		{

		
			LikeColumns = Resolver.Resolve<IColumnService> ().GetLikeColumns ();
	
			FocusNews = Resolver.Resolve<INewsService> ().GetFocusNews (LikeColumns [0].CategoryID);
			SelectNews = Resolver.Resolve<INewsService> ().GetSelectNews (LikeColumns [0].CategoryID);
			SelectedFocusNews = FocusNews [0];
			//
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

		public ICommand GotoNewsDetailCommand { get { return new Command<int> (r => {
				Navigation.NavigateTo<NewsDetailModel> (null, true, (m, p) => {
					(m as NewsDetailModel).Init (r);
				});

			}); } }
	

	
	}
}

