using System;
using System.Collections.Generic;
using TopMedicalNews;
using TopMedicalNews.Model;
using TinyIoC;


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
			return TinyIoCContainer.Current.Resolve<ISQLiteClient> ().GetAllData<Category> ();
		}

	}
}

