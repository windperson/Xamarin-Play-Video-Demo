using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
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

        private void SetPlayProgressChangeEventHandler()
        {
            _manager.PlayingChanged += _manager_PlayingChanged;
        }

        private void _manager_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {
#if DEBUG
			System.Diagnostics.Debug.WriteLine("event invoked");
#endif
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressBar.Progress = e.Progress;
                Info.Text = $"Duration {e.Duration.Seconds} seconds, Position: {e.Position}";
            });
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            _manager.PlaybackController.Play();
        }

        private void PauseClicked(object sender, EventArgs e)
        {
            _manager.PlaybackController.Pause();
        }

        private void StopClicked(object sender, EventArgs e)
        {
            _manager.PlaybackController.Stop();
        }
    }
}
