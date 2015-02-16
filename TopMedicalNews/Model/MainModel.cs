using System;
using XLabs.Forms.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using XLabs.Platform.Device;
using System.Threading.Tasks;

namespace TopMedicalNews
{
	public class MainModel:ViewModel
	{
		public MainModel ()
		{
			if (_mediaPicker != null)
			{
				return;
			}

			var device = Resolver.Resolve<IDevice>();

			////RM: hack for working on windows phone? 
			_mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
		}
		private async Task SelectPicture()
		{


			ImageSource = null;
			try
			{
				var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
					{
						DefaultCamera = CameraDevice.Front,
						MaxPixelDimension = 400
					});
				ImageSource = ImageSource.FromStream(() => mediaFile.Source);
			}
			catch (System.Exception ex)
			{
				Status = ex.Message;
			}
		}
		private IMediaPicker _mediaPicker;
		public ICommand SelfTakPictureCommand
		{
			//
			get{ 
				return new Command (async () => {
					await TakePicture();
				});
			}
			//
		}
		public ICommand SelectPictureCommand
		{
			//
			get{ 
				return new Command (async () => {
					await SelectPicture();
				});
			}
			//
		}
		string _status;
		public string Status
		{
			get { return _status; }
			private set { SetProperty(ref _status, value); }
		}
		ImageSource _imageSource;
		public ImageSource ImageSource
		{
			get
			{
				return _imageSource;
			}
			set
			{
				SetProperty(ref _imageSource, value);
			}
		}

		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		private async Task<MediaFile> TakePicture()
		{


			ImageSource = null;

			return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						Status = t.Exception.InnerException.ToString();
					}
					else if (t.IsCanceled)
					{
						Status = "Canceled";
					}
					else
					{
						var mediaFile = t.Result;
						ImageSource = ImageSource.FromStream(() => mediaFile.Source);
						//
						return mediaFile;
					}
					return null;
				}, _scheduler);
		}
	}
}

