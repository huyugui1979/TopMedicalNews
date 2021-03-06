﻿using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Xamarin.Forms;


[assembly:ExportRenderer(typeof(TopMedicalNews.MyEntry), typeof(TopMedicalNews.Android.MyEntryRenderer))]

namespace TopMedicalNews.Android
{
	public class MyEntryRenderer:EntryRenderer
	{
		public MyEntryRenderer ()
		{

		}
		protected async override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged (e);
//			var handle = new FileImageSourceHandler();
//			var entry = e.NewElement as MyEntry;
//			var bitmap = await handle.LoadImageAsync(entry.Source,Forms.Context);
//			Drawable drawable = new BitmapDrawable(bitmap);
//			this.Control.SetCompoundDrawablesWithIntrinsicBounds (drawable, null, null, null);
			this.Control.SetBackgroundDrawable (null);
			this.Control.ImeOptions = global::Android.Views.InputMethods.ImeAction.Next;
			this.Control.SetSingleLine (true);
			this.Control.InputType = global::Android.Text.InputTypes.ClassText;;
		}
	}
}

