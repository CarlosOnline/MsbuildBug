using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class AudioManagement : IAudioManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance _mediaPlayerInstance;

        internal AudioManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            _mediaPlayerInstance = mediaPlayerInstance;
            Outputs = new AudioOutputsManagement(manager, mediaPlayerInstance);
            Tracks = new AudioTracksManagement(manager, mediaPlayerInstance);
        }

        public IAudioOutputsManagement Outputs { get; }

        public bool IsMute
        {
            get { return _manager.IsMute(_mediaPlayerInstance); }
            set { _manager.SetMute(_mediaPlayerInstance, value); }
        }

        public void ToggleMute()
        {
            _manager.ToggleMute(_mediaPlayerInstance);
        }

        public int Volume
        {
            get { return _manager.GetVolume(_mediaPlayerInstance); }
            set { _manager.SetVolume(_mediaPlayerInstance, value); }
        }

        public ITracksManagement Tracks { get; }

        public int Channel
        {
            get { return _manager.GetAudioChannel(_mediaPlayerInstance); }
            set { _manager.SetAudioChannel(_mediaPlayerInstance, value); }
        }

        public long Delay
        {
            get { return _manager.GetAudioDelay(_mediaPlayerInstance); }
            set { _manager.SetAudioDelay(_mediaPlayerInstance, value); }
        }
    }
}
