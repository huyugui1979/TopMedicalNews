using System;
using System.Collections.Generic;
using TopMedicalNews.Model;
using TinyIoC;

namespace TopMedicalNews
{
	public class SelectColumnModel:BaseModel
	{
		public List<Column>  LikeColumns{ get; set;}
		public SelectColumnModel ()
		{
			LikeColumns = TinyIoCContainer.Current.Resolve<IColumnService> ().GetLikeColumns();
		}
	}
}

