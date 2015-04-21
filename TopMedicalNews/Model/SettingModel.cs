using System;
using Xamarin.Forms;
using Refractored.Xam.Settings.Abstractions;
using Acr.XamForms.UserDialogs;
using System.Threading.Tasks;
using System.Collections.Generic;
using TopMedicalNews.Model;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public class SettingModel:BaseViewModel
	{
		public SettingModel ()
		{
			_VerInfo = "最新系统";
			_CacheNum =  "缓存"+FileUtil.GetCacheFileSize ().ToString("0.00")+"M";
			_FeedBack = "输入你对本系统的意见，谢谢";
			//int fontId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("SelectFontId",1);
			Fonts = Resolver.Resolve<IFontService> ().GetAllFont ();
			SelectFont = Resolver.Resolve<IFontService> ().GetSelectFont ();
		}

		public List<MyFont> Fonts{ get; set; }

		int _SelectFont;

		public 	 int SelectFont { 
			get { return _SelectFont; } 
			set {
				SetProperty (ref _SelectFont, value);
				Resolver.Resolve<IFontService> ().SetSelectFont (value);

			}
		}
		//
		String _CacheNum;

		public string CacheNum {
			get{ return _CacheNum; }
			set{ SetProperty (ref _CacheNum, value); }
		}
		//
		String _VerInfo;

		public string VerInfo {
			get{ return _VerInfo; }
			set{ SetProperty (ref _VerInfo, value); }
		}
		//
		String _FeedBack;

		public string FeedBack {
			get{ return _FeedBack; }
			set{ SetProperty (ref _FeedBack, value); }
		}

		//
	

		public Command ClearCache {
			get {
				return  new Command (
					async () => {
						//
						//await Resolver.Resolve<ICacheService>().ClearCache();
						var cancelled = false;
						var dlg = Resolver.Resolve<IUserDialogService> ().Loading ("清除缓存中...");
						dlg.Show ();
						await Resolver.Resolve<ICacheService> ().ClearCache ();
						dlg.Hide();
						CacheNum = "缓存"+FileUtil.GetCacheFileSize ().ToString("0.00")+"M";
					});
			}
		}
		//
		private Command _updateSystem;

		public Command UpdateSystem {
			get {
				return  new Command (
					async () => {
						var dialogService = Resolver.Resolve<IUserDialogService> ();
						await dialogService.ConfirmAsync ("你的系统是最新的");
						//
					}
				);
			}
		}
		//
		private Command _feedBackCmd;

		public Command FeedBackCmd {
			get {
				return _feedBackCmd ?? (_feedBackCmd = new Command (
					() => Navigation.NavigateTo<FeedBackModel>(),
					() => true));
			}
		}
		//
		private Command _AboutUsCmd;

		public Command AboutUsCmd {
			get {
				return _AboutUsCmd ?? (_AboutUsCmd = new Command (
					() => Navigation.NavigateTo<AboutUsModel> (),
					() => true));
			}
		}

	}
}

