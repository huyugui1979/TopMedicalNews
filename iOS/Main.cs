using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Collections.Specialized;
using System.Windows.Input;

namespace TopMedicalNews.iOS
{
	public class LinkerPleaseInclude
	{
		public void Include(UIButton uiButton)
		{
			uiButton.TouchUpInside += (s, e) =>
				uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
		}

		public void Include(UIBarButtonItem barButton)
		{
			barButton.Clicked += (s, e) =>
				barButton.Title = barButton.Title + "";
		}

		public void Include(UITextField textField)
		{
			textField.Text = textField.Text + "";
			textField.EditingChanged += (sender, args) => { textField.Text = ""; };
		}

		public void Include(UITextView textView)
		{
			textView.Text = textView.Text + "";
			textView.Changed += (sender, args) => { textView.Text = ""; };
		}

		public void Include(UILabel label)
		{
			label.Text = label.Text + "";
		}

		public void Include(UIImageView imageView)
		{
			imageView.Image = new UIImage(imageView.Image.CGImage);
		}

	

		public void Include(UISlider slider)
		{
			slider.Value = slider.Value + 1;
			slider.ValueChanged += (sender, args) => { slider.Value = 1; };
		}

		public void Include(UISwitch sw)
		{
			sw.On = !sw.On;
			sw.ValueChanged += (sender, args) => { sw.On = false; };
		}

		public void Include(INotifyCollectionChanged changed)
		{
			changed.CollectionChanged += (s,e) => { var test = string.Format("{0}{1}{2}{3}{4}", e.Action,e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex); } ;
		}

		public void Include(ICommand command)
		{
			command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
		}
	}
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
 			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

