using System;
using XLabs.Ioc;
using TopMedicalNews.Model;
using System.Collections.Generic;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{
	public interface ILikeColumnService
	{
		void  SetLikeColumn (List<Column> columns);
		 List<Column>  GetLikeColumns();
	}
	public class LikeColumnService:ILikeColumnService
	{
		public LikeColumnService ()
		{
		}
		public List<Column>  GetLikeColumns()
		{
			return Resolver.Resolve<ISettings> ().GetValueOrDefault<List<Column>> ("LikeColumns", null);
		}

		public void SetLikeColumn(List<Column> columns)
		{
			Resolver.Resolve<ISettings> ().AddOrUpdateValue<List<Column>> ("LikeColumns",columns);
		
		}
	}
}

