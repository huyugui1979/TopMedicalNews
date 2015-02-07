using System;
using System.Collections.Generic;
using XLabs.Forms.Mvvm;
using XLabs.Data;
using TopMedicalNews.Model;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{
	public class FontChose:ObservableObject
	{	
		public Font Font{get;set;}
		bool _Selected;
		public bool   Selected{get{ return _Selected; }set{ SetProperty(ref _Selected, value);}}
	}
	public class ChoseFontModel:BaseViewModel
	{
		public ChoseFontModel ()
		{
			FontNames = new List<FontChose> ();
			List<Font> fonts = Resolver.Resolve<IFontService> ().GetAllFont ();
			foreach (var c in fonts) {
				FontNames.Add (new FontChose{ Font = c, Selected = false });
			}
			SelectFontId = Resolver.Resolve<ISettings> ().GetValueOrDefault<int> ("SelectFontId",1);

		}
		public  int SelectFontId{ get{
				FontChose fc = FontNames.Find (r => r.Selected == true);	
				return fc.Font.ID;
			} set {
				//
				FontNames.ForEach (r => r.Selected = false);
				FontChose fc = FontNames.Find (r => r.Font.ID == value);
				fc.Selected = true;
			} }
		public  List<FontChose> FontNames{ get; set; }

	
	}
}

