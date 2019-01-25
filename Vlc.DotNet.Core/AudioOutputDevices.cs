using System.Collections;
using System.Collections.Generic;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    public sealed class AudioOutputDevices : IEnumerable<AudioOutputDevice>
    {
        private VlcManager _manager;
        private VlcMediaPlayerInstance myMediaPlayerInstance;
        private AudioOutputDescription myAudioOutputDescription;

        public int Count
        {
            get
            {
                return _manager.GetAudioOutputDeviceCount(myAudioOutputDescription.Name);
            }
        }

        internal AudioOutputDevices(AudioOutputDescription audioOutputDescription, VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myAudioOutputDescription = audioOutputDescription;
            _manager = manager;
            myMediaPlayerInstance = mediaPlayerInstance;
        }

        public IEnumerator<AudioOutputDevice> GetEnumerator()
        {
            for (int id = 0; id < Count; id++)
            {
                yield return new AudioOutputDevice(_manager, myMediaPlayerInstance, myAudioOutputDescription, id);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
