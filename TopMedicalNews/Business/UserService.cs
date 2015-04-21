using System;
using TopMedicalNews.Model;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using System.Threading.Tasks;
using RestSharp;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using MyFormsLibCore.Ioc;

namespace TopMedicalNews
{
	public interface IUserService
	{
		Task  RegisterUser (string userName, string password, string phone, int dept1, int dept2);

		Task CheckUser (string userName, string password, int type);
	    void LogOut();
		User GetLoginUser ();
		Task Reset(string username,string password,string vcode);
		Task<string>  SendCode(string phone);
	    Task ModifyPassword(int uid,string oldpwd,string newpwd);
	}

	public class UserService:IUserService
	{
		public UserService ()
		{

		}

		public User GetLoginUser ()
		{
			string ui = CrossSettings.Current.GetValueOrDefault<string> ("UserInfo","");

			if (ui != "") {
		
				var ob = RestSharp.SimpleJson.DeserializeObject<User> (ui);

				return ob;
			} else
				return  null;
		}
		public void LogOut()
		{
			CrossSettings.Current.Remove("UserInfo");
		}
		public Task ModifyPassword(int uid,string oldpwd,string newpwd)
		{
			//http://iapp.iiyi.com/zlzs/v7/user/modpwd/?uid=4902960&oldpwd=123123&newpwd=12312311&os=3

			Dictionary<string,string> param = new Dictionary<string,string> ();
			param.Add ("uid", uid.ToString());
			param.Add ("oldpwd", oldpwd);
			param.Add ("newpwd", newpwd);
			if (Device.OS == TargetPlatform.iOS)
				param.Add ("os", "1");
			else
				param.Add ("os", "3");
			return Task.Factory.StartNew (() => {
				Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "zlzs/v7/user/modpwd/", param);
			});
		}
		public Task Reset(string username,string password,string vcode)
		{
			Dictionary<string,string> param = new Dictionary<string,string> ();
			param.Add ("username", username);
			param.Add ("password", password);
			param.Add ("vcode", vcode);
			if (Device.OS == TargetPlatform.iOS)
				param.Add ("os", "1");
			else
				param.Add ("os", "3");
			return Task.Factory.StartNew (() => {
				Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "zlzs/v5/user/reset/", param);
			});
		}

		public   Task<string>  SendCode(string phone)
		{
			Dictionary<string,string> param = new Dictionary<string,string> ();
			param.Add ("phone", phone);

			if (Device.OS == TargetPlatform.iOS)
				param.Add ("os", "1");
			else
				param.Add ("os", "3");
			return Task.Factory.StartNew (() => {
				string ss =	Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "zlzs/v7/user/sendcode/", param);

				return ss;
			});
		}
		public  Task  RegisterUser (string userName, string password, string phone, int dept1, int dept2)
		{


			Dictionary<string,string> param = new Dictionary<string,string> ();
			param.Add ("username", userName);
			param.Add ("password", password);
			param.Add ("phone", phone);
			param.Add ("dept1", dept1.ToString ());
			param.Add ("dept2", dept2.ToString ());
			param.Add ("checkphonev", "1");
			if (Device.OS == TargetPlatform.iOS)
				param.Add ("os", "1");
			else
				param.Add ("os", "3");
			return Task.Factory.StartNew (() => {
				Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "zlzs/v7/ext/register/", param);

				
			});
		}

		public 	Task CheckUser (string userName, string password, int type)
		{
		
			Dictionary<string,string> param = new Dictionary<string,string> ();
			param.Add ("username", userName);
			param.Add ("password", password);
			param.Add ("type", type.ToString ());
		
			return Task.Factory.StartNew (() => {
				var ss = Resolver.Resolve<IJsonService> ().ExecteQuery ("http://iapp.iiyi.com/", "yjtt/v1/user/login/", param);
				var obj = JsonHelper.Deserialize (ss) as IDictionary;
			
				if (obj ["res"].ToString () == "1") {
					var ui = obj ["userinfo"] as Dictionary<string,object>;
					User user = new User{ UserName = ui ["username"].ToString(), UID = int.Parse(ui ["uid"].ToString()), Email = ui ["email"].ToString(), Password = ui ["password"].ToString() };
					string s = RestSharp.SimpleJson.SerializeObject(user);
					CrossSettings.Current.AddOrUpdateValue<string> ("UserInfo",s);
					//
				} else
				{
					throw new MyException(obj["str"].ToString());
				}
			});
			//
		}
	
	}
}

