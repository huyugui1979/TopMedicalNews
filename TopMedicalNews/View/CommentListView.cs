using System;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class CommentListView:BaseCustomListView
	{
		public CommentListView ()
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

