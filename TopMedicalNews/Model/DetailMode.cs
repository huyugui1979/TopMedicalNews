using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using TinyIoC;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{


	public class DetailMode:BaseModel
	{
		public List<Column> LikeColumn{ get; set; }
		public List<News>   FocusNews{ get; set; }

		public DetailMode ()
		{
			//	
			LikeColumn = TinyIoCContainer.Current.Resolve<IColumnService> ().GetLikeColumns();
			FocusNews = TinyIoCContainer.Current.Resolve<INewsService> ().GetFocusNews (LikeColumn [0].CategoryID);
			//
			//

		}
	}
}

