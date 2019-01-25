using System.Collections.Generic;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    public sealed class AudioTracksManagement : ITracksManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        internal AudioTracksManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public int Count
        {
            get { return _manager.GetAudioTracksCount(myMediaPlayer); }
        }

        public TrackDescription Current
        {
            get
            {
                var currentId = _manager.GetAudioTrack(myMediaPlayer);
                foreach (var track in All)
                {
                    if (track.Id == currentId)
                        return track;
                }
                return null;
            }
            set { _manager.SetAudioTrack(myMediaPlayer, value.Id); }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                var module = _manager.GetAudioTracksDescriptions(myMediaPlayer);
                var result = TrackDescription.GetSubTrackDescription(module);
                _manager.ReleaseTrackDescription(module);
                return result;
            }
        }
    }
}
