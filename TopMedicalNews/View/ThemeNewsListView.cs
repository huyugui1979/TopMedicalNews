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
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			_stack.Children.Add (HeadImage);
			HeadImage.WidthRequest = App.ScreenWidth;

			HeadImage.HeightRequest = App.ScreenWidth*0.6;
		}
		public Image        HeadImage{ get; set;
			
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

						if((item as News).ImageUri == "")
								view.FindByName<Image>("image").IsVisible=false;
						

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

