using System;
using System.Collections.Generic;
using TopMedicalNews;
using TopMedicalNews.Model;
using XLabs.Ioc;


namespace TopMedicalNews
{
    public interface ICategoryService
    {
        List<Category> GetAllCategory();
    }
	public class CategoryService:ICategoryService
	{
		public List<Category> GetAllCategory()
		{
			return Resolver.Resolve<ISQLiteClient> ().GetAllData<Category> ();
		}

	}
}

