using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class ModifyPasswordModel:BaseViewModel
	{
		public ModifyPasswordModel ()
		{
		}
		public string OldPassword{get;set;}
		public string NewPassword{get;set;}
		public string RepPassword{get;set;}
		public ICommand LoginCommand{
			get{ 
				return new Command (() => {
					//

					//
				});
			}}
	}
}
   
