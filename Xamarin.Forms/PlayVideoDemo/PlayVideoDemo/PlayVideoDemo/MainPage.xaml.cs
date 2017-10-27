using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Forms;
using Xamarin.Forms;

namespace PlayVideoDemo
{
    public partial class MainPage : ContentPage
    {
        private IMediaManager _manager => CrossMediaManager.Current;

        public MainPage()
        {
            InitializeComponent();
            SetPlayProgressChangeEventHandler();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetVideoSource();
        }

        private void SetPlayProgressChangeEventHandler()
        {
            _manager.PlayingChanged += _manager_PlayingChanged;
        }

        private void SetVideoSource()
        {
            if (String.IsNullOrEmpty(VideoView.Source))
            {
                VideoView.Source = "https://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
                //set stop to prevent autoplay when Xamarin Form shows up in iOS
                //https://github.com/martijn00/XamarinMediaManager/issues/237
                _manager.PlaybackController.Stop(); 
            }
        }

        private void _manager_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("event invoked");
#endif
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressBar.Progress = e.Progress;
                Duration.Text = e.Duration.ToString(@"hh\:mm\:ss");
                Position.Text = e.Position.ToString(@"hh\:mm\:ss");
            });
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            if (!(sender is Button button))
            {
                return;
            }
            if (button.IsEnabled)
            {
                _manager.PlaybackController.Play();
                button.IsEnabled = false;
                PauseBtn.IsEnabled = true;
                StopBtn.IsEnabled = true;
            }
        }

        private void PauseClicked(object sender, EventArgs e)
        {
            if (!(sender is Button button))
            {
                return;
            }
            if (button.IsEnabled)
            {
                _manager.PlaybackController.Pause();
                button.IsEnabled = false;
                PlayBtn.IsEnabled = true;
                StopBtn.IsEnabled = true;
            }
        }

        private void StopClicked(object sender, EventArgs e)
        {
            if (!(sender is Button button))
            {
                return;
            }
            if (button.IsEnabled)
            {
                _manager.PlaybackController.Stop();
                button.IsEnabled = false;
                PlayBtn.IsEnabled = true;
                PauseBtn.IsEnabled = false;
            }
        }
    }
}
