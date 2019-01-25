using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class AdjustmentsManagement : IAdjustmentsManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public AdjustmentsManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return _manager.GetVideoAdjustEnabled(myMediaPlayer); }
            set { _manager.SetVideoAdjustEnabled(myMediaPlayer, value); }
        }

        public float Contrast
        {
            get { return _manager.GetVideoAdjustContrast(myMediaPlayer); }
            set { _manager.SetVideoAdjustContrast(myMediaPlayer, value); }
        }

        public float Brightness
        {
            get { return _manager.GetVideoAdjustBrightness(myMediaPlayer); }
            set { _manager.SetVideoAdjustBrightness(myMediaPlayer, value); }
        }

        public float Hue
        {
            get { return _manager.GetVideoAdjustHue(myMediaPlayer); }
            set { _manager.SetVideoAdjustHue(myMediaPlayer, value); }
        }

        public float Saturation
        {
            get { return _manager.GetVideoAdjustSaturation(myMediaPlayer); }
            set { _manager.SetVideoAdjustSaturation(myMediaPlayer, value); }
        }

        public float Gamma
        {
            get { return _manager.GetVideoAdjustGamma(myMediaPlayer); }
            set { _manager.SetVideoAdjustGamma(myMediaPlayer, value); }
        }
    }
}
