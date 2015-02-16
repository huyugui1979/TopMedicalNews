using System;
using System.Threading.Tasks;
using System.Threading;
using XLabs.Ioc;
using Acr.XamForms.UserDialogs;

namespace TopMedicalNews
{
	public interface ICacheService
	{
		Task ClearCache(Action<int> action);
	}
	public class CacheService:ICacheService
	{
		public CacheService ()
		{
		}
		public Task ClearCache(Action<int> action)
		{
			return Task.Factory.StartNew (() => {

				for(int i=0;i<100;i++)
				{
					Thread.Sleep(100);

					action(i);
				}
			});
		}
	}
}

