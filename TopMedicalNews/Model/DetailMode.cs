using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using TinyIoC;
using Refractored.Xam.Settings.Abstractions;
using System.Collections.ObjectModel;

namespace TopMedicalNews
{


	public class DetailMode:BaseModel
	{
		public List<Column> LikeColumn{ get; set; }
		public List<News>   FocusNews{ get; set; }
		public List<News>   SelectNews{ get; set; }
		//
		Column _SelectColumn=null;
		public Column 			SelectColumn{ get{ return _SelectColumn;
			} set{
				_SelectColumn = value;
				FocusNews = TinyIoCContainer.Current.Resolve<INewsService> ().GetFocusNews   (_SelectColumn.CategoryID);
				SelectNews = TinyIoCContainer.Current.Resolve<INewsService> ().GetSelectNews (_SelectColumn.CategoryID);
				this.OnPropertyChanged ("FocusNews");
				this.OnPropertyChanged ("SelectNews");
				//
			} 
		}
		public DetailMode ()
		{
			LikeColumn = TinyIoCContainer.Current.Resolve<IColumnService> ().GetLikeColumns();
			FocusNews = TinyIoCContainer.Current.Resolve<INewsService> ().GetFocusNews (LikeColumn [0].CategoryID);
			SelectNews = TinyIoCContainer.Current.Resolve<INewsService> ().GetSelectNews (LikeColumn [0].CategoryID);

		}

	
	}
}

