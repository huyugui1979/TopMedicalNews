using System;
using TopMedicalNews.Model;
using XLabs.Ioc;
using Refractored.Xam.Settings.Abstractions;

namespace TopMedicalNews
{
	public interface IUserService
	{
		bool CheckUser(User user);
	    User GetLoginUser();
	}
	public class UserService:IUserService
	{
		public UserService ()
		{
		}
		public  User GetLoginUser()
		{
			int userId=Resolver.Resolve<ISettings> ().GetValueOrDefault ("LoginUserId", -1);
			if (userId != -1) {
				return 	Resolver.Resolve<ISQLiteClient> ().GetData<User> (r => r.ID == userId);
			} else {
				return new User ();
			}
		}
		public 	bool CheckUser(User user)
		{
			if (user.Password == "123" && user.UserName == "tom") {
				user.ID = 1;
				Resolver.Resolve<ISettings>().AddOrUpdateValue<int>("LoginUserId",1);
				//Resolver.Resolve<ISQLiteClient> ().InsertData<User> (user);
				return true;
			} else
				return false;
		}
	}
}

