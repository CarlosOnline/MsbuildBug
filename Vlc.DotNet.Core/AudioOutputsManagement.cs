using System;
using System.Collections.Generic;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal class AudioOutputsManagement : IAudioOutputsManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayerInstance;

        internal AudioOutputsManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayerInstance = mediaPlayerInstance;
        }

        public IEnumerable<AudioOutputDescription> All
        {
            get
            {
                var module = _manager.GetAudioOutputsDescriptions();
                var result = AudioOutputDescription.GetSubOutputDescription(module, _manager, myMediaPlayerInstance);
                _manager.ReleaseAudioOutputDescription(module);
                return result;
            }
        }

        public int Count
        {
            get { return new List<AudioOutputDescription>(All).Count; }
        }

        public AudioOutputDescription Current
        {
            get
            {
                throw new NotImplementedException("Not implemented in LibVlc.");
            }
            set { _manager.SetAudioOutput(value.Name); }
        }
    }
}
