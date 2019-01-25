using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class LogoManagement : ILogoManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public LogoManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return _manager.GetVideoLogoEnabled(myMediaPlayer); }
            set { _manager.SetVideoLogoEnabled(myMediaPlayer, value); }
        }

        public string File
        {
            set { _manager.SetVideoLogoFile(myMediaPlayer, value); }
        }

        public int X
        {
            get { return _manager.GetVideoLogoX(myMediaPlayer); }
            set { _manager.SetVideoLogoX(myMediaPlayer, value); }
        }

        public int Y
        {
            get { return _manager.GetVideoLogoY(myMediaPlayer); }
            set { _manager.SetVideoLogoY(myMediaPlayer, value); }
        }

        public int Delay
        {
            get { return _manager.GetVideoLogoDelay(myMediaPlayer); }
            set { _manager.SetVideoLogoDelay(myMediaPlayer, value); }
        }

        public int Repeat
        {
            get { return _manager.GetVideoLogoRepeat(myMediaPlayer); }
            set { _manager.SetVideoLogoRepeat(myMediaPlayer, value); }
        }

        public int Opacity
        {
            get { return _manager.GetVideoLogoOpacity(myMediaPlayer); }
            set { _manager.SetVideoLogoOpacity(myMediaPlayer, value); }
        }

        public int Position
        {
            get { return _manager.GetVideoLogoPosition(myMediaPlayer); }
            set { _manager.SetVideoLogoPosition(myMediaPlayer, value); }
        }
    }
}
