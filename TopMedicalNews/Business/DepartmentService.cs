using System;
using RestSharp;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using TopMedicalNews.Model;
using XLabs.Ioc;
using System.Threading.Tasks;

namespace TopMedicalNews
{
public interface IDepartmentService
	{
		//void DownloadDepartment (CancellationToken token);
		List<Department> GetDepartments ();

		void Init ();
	}

	public class DepartmentService:IDepartmentService
	{
		public DepartmentService ()
		{

		}
		//

		public List<Department> GetDepartments ()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Department> ();
		}

		public  void Init ()
		{
			var client = new RestClient ();
			client.BaseUrl = new Uri ("http://iapp.iiyi.com/");
			//client.Authenticator = new HttpBasicAuthenticator("username", "password");
			var request = new RestRequest ();
			request.Resource = "yjtt/v1/user/getclass/";
			var res = client.Execute (request);
			//

			//
			var obj = JsonHelper.Deserialize (res.Content) as IDictionary;
			var dataList = obj ["data"] as List<object>;
			List<Department> deps = new List<Department> ();
			dataList.ForEach (r => {
				var d = r as Dictionary<string,object>;
				Department dep = new Department{ Id = int.Parse (d ["id"].ToString ()), ParentTitle = "", Title = d ["name"].ToString () };
				deps.Add (dep);
				var smallList = d ["small"] as List<object>;
				smallList.ForEach (s => {
					var sd = s as IDictionary;
					dep = new Department {
						Id = int.Parse (sd ["id"].ToString ()),
						ParentId = int.Parse (d ["id"].ToString ()),
						ParentTitle = d ["name"].ToString (),
						Title = sd ["name"].ToString ()
					};
					deps.Add (dep);
				});
			});
			Resolver.Resolve<ISQLiteClient> ().InsertOrReplaceAll (deps, typeof(Department));
				 

		}
	}

}

