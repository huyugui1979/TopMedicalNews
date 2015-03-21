using System;
using System.Collections.Specialized;
using Xamarin.Forms;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class MyCommentListView:BaseCustomListView
	{
		public MyCommentListView ()
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

						if ((item as Reading).Imginfo == "")
							view.FindByName<Image> ("image").IsVisible = false;
						view.GestureRecognizers.Add (new TapGestureRecognizer (v => {
							(v.ParentView.BindingContext as MyCommentModel).GotoNewsDetailCommand.Execute (item);

						}));
						var bindableObject = view as BindableObject;
						if (bindableObject != null)
							bindableObject.BindingContext = item;
						_stack.Children.Insert(0,view);
						_Button.IsVisible=true;
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

