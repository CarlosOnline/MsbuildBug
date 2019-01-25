using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    public sealed class AudioOutputDevice
    {
        private VlcManager _manager;
        private VlcMediaPlayerInstance myMediaPlayerInstance;
        private AudioOutputDescription myAudioOutputDescription;

        public int Id { get; }

        public string Name
        {
            get
            {
                return _manager.GetAudioOutputDeviceName(myAudioOutputDescription.Name, Id);
            }
        }

        public string LongName
        {
            get
            {
                return _manager.GetAudioOutputDeviceLongName(myAudioOutputDescription.Name, Id);
            }
        }

        internal AudioOutputDevice(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance, AudioOutputDescription audioDescription, int id)
        {
            _manager = manager;
            myMediaPlayerInstance = mediaPlayerInstance;
            myAudioOutputDescription = audioDescription;
            Id = id;
        }

        public void SetAsCurrent()
        {
            _manager.SetAudioOutputDevice(myMediaPlayerInstance, myAudioOutputDescription.Name, Name);
        }
    }
}
