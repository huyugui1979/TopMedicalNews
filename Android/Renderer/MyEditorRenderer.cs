using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;


[assembly:ExportRenderer(typeof(TopMedicalNews.MyEditor), typeof(TopMedicalNews.Android.MyEditorRenderer))]
namespace TopMedicalNews.Android
{
	public class MyEditorRenderer:EditorRenderer
	{
		public MyEditorRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);
			this.Control.Hint = "输入你的反馈意见";
		}
	}
}

