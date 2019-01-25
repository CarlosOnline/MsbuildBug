using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class MarqueeManagement : IMarqueeManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public MarqueeManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return _manager.GetVideoMarqueeEnabled(myMediaPlayer); }
            set { _manager.SetVideoMarqueeEnabled(myMediaPlayer, value); }
        }

        public string Text
        {
            get { return _manager.GetVideoMarqueeText(myMediaPlayer); }
            set { _manager.SetVideoMarqueeText(myMediaPlayer, value); }
        }

        public int Color
        {
            get { return _manager.GetVideoMarqueeColor(myMediaPlayer); }
            set { _manager.SetVideoMarqueeColor(myMediaPlayer, value); }
        }

        public int Opacity
        {
            get { return _manager.GetVideoMarqueeOpacity(myMediaPlayer); }
            set { _manager.SetVideoMarqueeOpacity(myMediaPlayer, value); }
        }

        public int Position
        {
            get { return _manager.GetVideoMarqueePosition(myMediaPlayer); }
            set { _manager.SetVideoMarqueePosition(myMediaPlayer, value); }
        }

        public int Refresh
        {
            get { return _manager.GetVideoMarqueeRefresh(myMediaPlayer); }
            set { _manager.SetVideoMarqueeRefresh(myMediaPlayer, value); }
        }

        public int Size
        {
            get { return _manager.GetVideoMarqueeSize(myMediaPlayer); }
            set { _manager.SetVideoMarqueeSize(myMediaPlayer, value); }
        }

        public int Timeout
        {
            get { return _manager.GetVideoMarqueeTimeout(myMediaPlayer); }
            set { _manager.SetVideoMarqueeTimeout(myMediaPlayer, value); }
        }

        public int X
        {
            get { return _manager.GetVideoMarqueeX(myMediaPlayer); }
            set { _manager.SetVideoMarqueeX(myMediaPlayer, value); }
        }

        public int Y
        {
            get { return _manager.GetVideoMarqueeY(myMediaPlayer); }
            set { _manager.SetVideoMarqueeY(myMediaPlayer, value); }
        }
    }
}
