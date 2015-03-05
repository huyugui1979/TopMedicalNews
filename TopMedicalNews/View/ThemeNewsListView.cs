using System;
using System.Collections.Specialized;
using Xamarin.Forms;

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
						view.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
							(v.ParentView.BindingContext as NewsThemeModel).GotoNewsDetailCommand.Execute(item);

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

