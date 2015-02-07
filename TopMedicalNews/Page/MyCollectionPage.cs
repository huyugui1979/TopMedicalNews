using System;
using Xamarin.Forms;

namespace TopMedicalNews
{
	public class DiscussionCell:ViewCell
	{
		public DiscussionCell ()
		{
			Label Name, Time, Thumb;
			Image UserImage;
			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = {
					(UserImage = new Image{ Source = "Portait.jpg" }),
					new StackLayout {
						Orientation = StackOrientation.Vertical,
						Children = {
							new StackLayout {
								Orientation = StackOrientation.Horizontal,
								Children = {
									(Name = new Label{ }),
									(Time = new Label{ })
								}
							},
							(Thumb = new Label{ })
						}
					}
				}
			};
			Name.SetBinding (Label.TextProperty, "");
			Time.SetBinding (Label.TextProperty, "");
			Thumb.SetValue (Label.TextProperty, "");
		}
	}

	public class MyCollectionPage:ContentPage
	{
		public MyCollectionPage ()
		{
			View list1, list2, entry1, button1, image1;
			Content = new ScrollView {
				Content = 
					new StackLayout {
					Children = {
						(list1 = new ListView {
							//ItemTemplate = new DataTemplate (typeof(NewsCell)),
							RowHeight = 100
						}),
						(list2 = new ListView {
							ItemTemplate = new DataTemplate (typeof(DiscussionCell)),
							RowHeight = 100
						}),
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								(entry1 = new Entry{ }),
								(button1 = new Button{ }),
								(image1 = new Image{ })
							}
						},

					}
				}

			};
		}
	}
}

