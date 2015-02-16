using System;
using XLabs.Forms.Mvvm;
using Xamarin.Forms;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using System.Threading.Tasks;
using System.Collections.Generic;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class SettingModel:BaseViewModel
	{
		public SettingModel ()
		{
			_VerInfo =  "最新系统";
			_CacheNum = "缓存100M";
			_FeedBack = "输入你对本系统的意见，谢谢";
			//int fontId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("SelectFontId",1);
			Fonts = Resolver.Resolve<IFontService> ().GetAllFont ();

		}
		public List<MyFont> Fonts{ get; set; }
	    String  _FontName;
		public 	 string FontName { 
			get { return _FontName; } 
			set { SetProperty (ref _FontName, value);
			}
		}
		//
		String _CacheNum;
		public string CacheNum{
			get{ return _CacheNum; }
			set{ SetProperty (ref _CacheNum, value); }
		}
		//
		String _VerInfo;
		public string VerInfo{
			get{ return _VerInfo; }
			set{ SetProperty (ref _VerInfo, value); }
		}
		//
		String _FeedBack;
		public string FeedBack{
			get{ return _FeedBack; }
			set{ SetProperty (ref _FeedBack, value); }
		}

		//
		private Command _clearCache;
		public Command ClearCache 
		{
			get
			{
				return _clearCache ?? (_clearCache = new Command(
					 async () =>{
						//
						//await Resolver.Resolve<ICacheService>().ClearCache();
						var cancelled = false;
						var dialogService = Resolver.Resolve<IUserDialogService>();
						using (var dlg = dialogService.Progress("清除中")) {
						await Resolver.Resolve<ICacheService>().ClearCache(
									(i)=>{
										dlg.PercentComplete =i;
									}
								);
						}
						//
					},
					() => true));
			}
		}
		//
		private Command _updateSystem;
		public Command UpdateSystem 
		{
			get
			{
				return _clearCache ?? (_updateSystem = new Command(
					async () =>{
						var dialogService = Resolver.Resolve<IUserDialogService>();
						await dialogService.ConfirmAsync("你的系统是最新的");
						//
					},
					() => true));
			}
		}
		//
		private Command _feedBackCmd;
		public Command FeedBackCmd 
		{
			get
			{
				return _feedBackCmd ?? (_feedBackCmd = new Command(
					() => Navigation.NavigateTo<FeedBackModel>(null,true),
					() => true));
			}
		}
		//
		private Command _AboutUsCmd;
		public Command AboutUsCmd 
		{
			get
			{
				return _AboutUsCmd ?? (_AboutUsCmd = new Command(
					() => Navigation.NavigateTo<AboutUsModel>(null,true),
					() => true));
			}
		}

	}
}

