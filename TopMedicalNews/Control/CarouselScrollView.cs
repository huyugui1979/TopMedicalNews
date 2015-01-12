using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class CarouselScrollView:ScrollView
	{
		public CarouselScrollView ()
		{
		}
		public void UpdateSelectIndex(int index)
		{
			OnSelctItem (this, index);
		}
		public event EventHandler<int> OnSelctItem;
	}
}

