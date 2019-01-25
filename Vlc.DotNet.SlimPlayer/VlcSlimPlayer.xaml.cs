using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Forms;
#pragma warning disable 67

namespace Vlc.DotNet.SlimPlayer
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

    }
}
