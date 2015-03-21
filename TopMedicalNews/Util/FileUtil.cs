using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace TopMedicalNews
{
	public class FileUtil
	{
		public static double GetCacheFileSize ()
		{
			var path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var pi = Directory.CreateDirectory (path).Parent;
			DirectoryInfo di = new DirectoryInfo (path);
			double size1 = di.EnumerateFiles ("*", SearchOption.AllDirectories).Sum (fi => fi.Length) / (1024.0 * 1024.0);
			DirectoryInfo di1 = new DirectoryInfo (pi.ToString () + "/cache/");
			double size2 = di1.EnumerateFiles ("*", SearchOption.AllDirectories).Sum (fi => fi.Length) / (1024.0 * 1024.0);
			return size1 + size2;

		}
		public static void CleanCache ()
		{

			var path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			DirectoryInfo di2 = new DirectoryInfo(path + "/.config/.isolated-storage/ImageLoaderCache/");
			foreach(System.IO.FileInfo file in di2.GetFiles()) file.Delete();
			foreach(System.IO.DirectoryInfo subDirectory in di2.GetDirectories()) subDirectory.Delete(true);
			var pi = Directory.CreateDirectory (path).Parent;
			DirectoryInfo di1 = new DirectoryInfo (pi.ToString () + "/cache/");
			foreach(System.IO.FileInfo file in di1.GetFiles()) file.Delete();
			foreach(System.IO.DirectoryInfo subDirectory in di1.GetDirectories()) subDirectory.Delete(true);

		}

	}
}

