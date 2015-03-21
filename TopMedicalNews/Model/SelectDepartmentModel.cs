using System;
using System.Collections.ObjectModel;
using TopMedicalNews.Model;
using System.Collections.Generic;
using XLabs.Ioc;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Refractored.Xam.Settings;


namespace TopMedicalNews
{
	public class Grouping<K, T> : ObservableCollection<T> {
		public K Key { get; private set; }

		public Grouping ( K key, IEnumerable<T> items ) {
			Key = key;
			foreach ( var item in items )
				this.Items.Add( item );
		}
	}
	public class SelectDepartmentModel:BaseViewModel
	{
		public SelectDepartmentModel ()
		{
		}
		ObservableCollection<Department>  departments;
		public ObservableCollection<Department>  Departments{
			get { return departments; }
			set {
				SetProperty(ref departments,value);
			}

		}

		private ObservableCollection <Grouping<string,Department >> departmentGrouped;

		public ObservableCollection<Grouping<string, Department>> DepartmentGrouped {
			get { return departmentGrouped; } 
			set { 
				SetProperty(ref departmentGrouped,value);
			}	
		}
		public ICommand SelectDepartmentCommand{get{ return new Command<Department> ((dep) => {
			//CrossSettings.Current.AddOrUpdateValue<string>("SelectDepartment",RestSharp.SimpleJson.SerializeObject (dep));
			MessagingCenter.Send<Department>(dep,"SelectDepartment");
			Navigation.GoBack();
		});
			}}
		public void Init()
		{
			//
			var list = Resolver.Resolve<IDepartmentService> ().GetDepartments ();
			departments = new ObservableCollection<Department> (list);
			var sorted =
				from dep in departments
				orderby  dep.Id where dep.ParentTitle !=""
				group dep by dep.ParentTitle 
				into contactGroup
				select new Grouping<string, Department> ( contactGroup.Key, contactGroup );
			departmentGrouped = new ObservableCollection<Grouping<string, Department>> ( sorted );
		}
	}
}

