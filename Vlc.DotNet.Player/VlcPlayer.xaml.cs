using System.Windows;
using System.Windows.Controls;
using Vlc.DotNet.Forms;
#pragma warning disable 67

namespace Vlc.DotNet.Player
{
    public enum PlayState
    {
        None,
        Playing,
        Paused,
        Stopped,
        Error,
    }

    /// <summary>
    /// Interaction logic for VlcPlayer.xaml
    /// </summary>
    public partial class VlcPlayer : UserControl
    {
        public VlcControl MediaPlayer => PlayerControl.MediaPlayer;

        public delegate void MediaEndedHandler();
        public event MediaEndedHandler MediaEnded;

        public delegate void EncounteredErrorHandler(string message);
        public event EncounteredErrorHandler EncounteredError;

        private static readonly DependencyProperty VideoPathValue = DependencyProperty.Register("VideoPath", typeof(string), typeof(VlcPlayer), new FrameworkPropertyMetadata(null));
        public string VideoPath
        {
            get { return (string)GetValue(VideoPathValue); }
            set
            {
                SetValue(VideoPathValue, value);
            }
        }

        private static readonly DependencyProperty AutoPlayValue = DependencyProperty.Register("AutoPlay", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true));
        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayValue); }
            set
            {
                SetValue(AutoPlayValue, value);
            }
        }


        private static readonly DependencyProperty ShowPlayerVisibilityValue = DependencyProperty.Register("ShowPlayerVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowPlayerVisibility
        {
            get { return (Visibility)GetValue(ShowPlayerVisibilityValue); }
            set
            {
                SetValue(ShowPlayerVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowFilmReelVisibilityValue = DependencyProperty.Register("ShowFilmReelVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowFilmReelVisibility
        {
            get { return (Visibility)GetValue(ShowFilmReelVisibilityValue); }
            set
            {
                SetValue(ShowFilmReelVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowControlsBarVisibilityValue = DependencyProperty.Register("ShowControlsBarVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowControlsBarVisibility
        {
            get { return (Visibility)GetValue(ShowControlsBarVisibilityValue); }
            set
            {
                SetValue(ShowControlsBarVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowPlayPauseControlsBarVisibilityValue = DependencyProperty.Register("ShowPlayPauseControlsBarVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowPlayPauseControlsBarVisibility
        {
            get { return (Visibility)GetValue(ShowPlayPauseControlsBarVisibilityValue); }
            set
            {
                SetValue(ShowPlayPauseControlsBarVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowSeekBarVisibilityValue = DependencyProperty.Register("ShowSeekBarVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowSeekBarVisibility
        {
            get { return (Visibility)GetValue(ShowSeekBarVisibilityValue); }
            set
            {
                SetValue(ShowSeekBarVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowTimeVisibilityValue = DependencyProperty.Register("ShowTimeVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowTimeVisibility
        {
            get { return (Visibility)GetValue(ShowTimeVisibilityValue); }
            set
            {
                SetValue(ShowTimeVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowVideoPathVisibilityValue = DependencyProperty.Register("ShowVideoPathVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowVideoPathVisibility
        {
            get { return (Visibility)GetValue(ShowVideoPathVisibilityValue); }
            set
            {
                SetValue(ShowVideoPathVisibilityValue, value);
            }
        }

        private static readonly DependencyProperty ShowVolumeVisibilityValue = DependencyProperty.Register("ShowVolumeVisibility", typeof(Visibility), typeof(VlcPlayer), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility ShowVolumeVisibility
        {
            get { return (Visibility)GetValue(ShowVolumeVisibilityValue); }
            set
            {
                SetValue(ShowVolumeVisibilityValue, value);
            }
        }

    }
}
