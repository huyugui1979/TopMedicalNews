using System;
using Xamarin.Forms;

namespace TopMedicalNews
{

	public class NewsCell:ViewCell
	{
		public NewsCell ()
		{
			//
			var image1 = new Image{WidthRequest=80*1.2,HeightRequest=60*1.2, Aspect= Aspect.Fill, HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.Center};
			var binding = new Binding("ImageUri", BindingMode.Default, new StringToImageSourceConverter());
			image1.SetBinding(Image.SourceProperty,binding);
			//
			var label1 = new Label{ HorizontalOptions=LayoutOptions.Start,VerticalOptions=LayoutOptions.Start};
			label1.SetBinding(Label.TextProperty,"Title");
			//
			var label2 = new Label{ LineBreakMode = LineBreakMode.WordWrap,HorizontalOptions=LayoutOptions.Start,VerticalOptions=LayoutOptions.StartAndExpand};
			label2.SetBinding(Label.TextProperty,"Thumb");
			//
			var label3 = new Label{HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.Center };
			var binding1 = new Binding("PublishTime", BindingMode.Default, new DateTimeFormaterConverter());
			label3.SetBinding(Label.TextProperty,binding1);
			//
			var label4 = new Label{ HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.Center};
			label4.SetBinding(Label.TextProperty,"FromSource");
			//
			var label5 = new Label{HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.Center};
			label5.SetBinding(Label.TextProperty,"ViewerNum");
			//
			var label6 = new Label{ HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.Center};
			label6.SetBinding(Label.TextProperty,"PosterNum");
			//
			this.View = new StackLayout 
			{
				Orientation = StackOrientation.Vertical,
				Spacing=0,
				Padding=new Thickness(5,5,5,5),
				Children = 
				{
					new StackLayout{

						Orientation= StackOrientation.Horizontal,
						HorizontalOptions=LayoutOptions.Center,

						Children={
							image1,
							new StackLayout{
								HorizontalOptions=LayoutOptions.FillAndExpand,
								Orientation= StackOrientation.Vertical,
								Spacing=10,
								Children={
									label1,
									label2,
									//
								}
							}
						}
					},
					new StackLayout{
					
						Orientation= StackOrientation.Horizontal,
						Padding = new Thickness(10,0,10,0),
						Spacing=5,
						Children = {
							label3,
							label4,
							label5,
							label6,
						}
					}

				}
				};
		}
	}
}

