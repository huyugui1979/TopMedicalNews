using System;
using System.Collections.Specialized;
using Xamarin.Forms;
using TopMedicalNews.Model;

namespace TopMedicalNews
{
	public class NewsListView:BaseCustomListView
	{
		public NewsListView ()
		{
		}
		public DataTemplate NewsItemTemplate {
			get;
			set;
		}
		public DataTemplate ThemeItemTemplate {
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
						View view=null;
						var temp = item as News;
						if(temp.Type == "news")
						{
							view = (View)NewsItemTemplate.CreateContent ();
							view.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
								(v.ParentView.BindingContext as MyNewsListModel).GotoNewsDetailCommand.Execute(item);

							}));
						}
						else
						{
							view =(View)ThemeItemTemplate.CreateContent();
							view.GestureRecognizers.Add(new TapGestureRecognizer(v=>{
								(v.ParentView.BindingContext as MyNewsListModel).GotoThemeCommand.Execute(item);

							}));
						}
						//设置图像
						var image = view.FindByName<Image>("image");
						if((item as News).ImageUri == "")
						{
							image.Source= ImageSource.FromResource("Icon.png");
							image.IsVisible=false;
						}
						else
							image.IsVisible=true;
						var bindableObject = view as BindableObject;

						if (bindableObject != null)
							bindableObject.BindingContext = item;
						Device.BeginInvokeOnMainThread(()=>{
							_stack.Children.Add (view);});
						//

						//
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

