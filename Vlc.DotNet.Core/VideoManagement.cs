using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class VideoManagement : IVideoManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance _mediaPlayerInstance;

        public VideoManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            _mediaPlayerInstance = mediaPlayerInstance;
            Tracks = new VideoTracksManagement(manager, mediaPlayerInstance);
            Marquee = new MarqueeManagement(manager, mediaPlayerInstance);
            Logo = new LogoManagement(manager, mediaPlayerInstance);
            Adjustments = new AdjustmentsManagement(manager, mediaPlayerInstance);
        }

        public string CropGeometry
        {
            get { return _manager.GetVideoCropGeometry(_mediaPlayerInstance);  }
            set { _manager.SetVideoCropGeometry(_mediaPlayerInstance, value); }
        }

        public int Teletext
        {
            get { return _manager.GetVideoTeletext(_mediaPlayerInstance); }
            set { _manager.SetVideoTeletext(_mediaPlayerInstance, value); }
        }

        public ITracksManagement Tracks { get; }

        public string Deinterlace
        {
            set { _manager.SetVideoDeinterlace(_mediaPlayerInstance, value); }
        }

        public IMarqueeManagement Marquee { get; }
        public ILogoManagement Logo { get; }
        public IAdjustmentsManagement Adjustments { get; }
    }
}
