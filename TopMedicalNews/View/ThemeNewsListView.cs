using System;
using System.Collections.Specialized;
using Xamarin.Forms;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class ThemeNewsListView:BaseCustomListView
	{
		public ThemeNewsListView ()
		{

		}


		public DataTemplate ItemTemplate {
			get;
			set;

		}
		protected override void ItemsSourceChanged ()
		{
			base.ItemsSourceChanged ();
			(ItemsSource as INotifyCollectionChanged).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => {
				//_stack.Children.Clear();

				switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems) {
						View view = null;

						view = (View)ItemTemplate.CreateContent ();
						if(item is Collection)
						{
							if((item as Collection).Imginfo == "")
								view.FindByName<Image>("image").IsVisible=false;
						}
						if(item is Reading)
						{
							if((item as Reading).Imginfo == "")
								view.FindByName<Image>("image").IsVisible=false;
						}

						view.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
							if(v.ParentView.BindingContext is NewsThemeModel)
								(v.ParentView.BindingContext as NewsThemeModel).GotoNewsDetailCommand.Execute(item);
							else if(v.ParentView.BindingContext is MyCollectionModel)
								(v.ParentView.BindingContext as MyCollectionModel).GotoNewsDetailCommand.Execute(item);
							else if(v.ParentView.BindingContext is MyReadingModel)
								(v.ParentView.BindingContext as MyReadingModel).GotoNewsDetailCommand.Execute(item);

						}));
						var bindableObject = view as BindableObject;
						if (bindableObject != null)
							bindableObject.BindingContext = item;
						_stack.Children.Add (view);

					}
					break;
				case NotifyCollectionChangedAction.Reset:
					_stack.Children.Clear ();
					break;

				}	
			};
		}
	}
}

