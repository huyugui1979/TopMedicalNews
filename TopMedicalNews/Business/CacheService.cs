using System;
using System.Threading.Tasks;
using System.Threading;
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;
using TopMedicalNews.Model;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public interface ICacheService
	{
		Task ClearCache();

	}
	public class CacheService:ICacheService
	{
		public CacheService ()
		{
		}
		public Task ClearCache()
		{
			return Task.Factory.StartNew (() => {
				FileUtil.CleanCache ();
				Resolver.Resolve<ISQLiteClient>().DeleteAllData<News>();
				Resolver.Resolve<ISQLiteClient>().DeleteAllData<Reading>();
				Resolver.Resolve<ISQLiteClient>().DeleteAllData<Collection>();
				Resolver.Resolve<ISQLiteClient>().DeleteAllData<Comment>();
			
			});
		}
	}
}

