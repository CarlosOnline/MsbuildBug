using System;
using System.Collections.Generic;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    public sealed class VideoTracksManagement : ITracksManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        internal VideoTracksManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public int Count
        {
            get { return _manager.GetVideoTracksCount(myMediaPlayer); }
        }

        public TrackDescription Current
        {
            get
            {
                var currentId = _manager.GetVideoTrack(myMediaPlayer);
                foreach (var track in All)
                {
                    if (track.Id == currentId)
                        return track;
                }
                return null;
            }
            set { _manager.SetVideoTrack(myMediaPlayer, value.Id); }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                var module = _manager.GetVideoTracksDescriptions(myMediaPlayer);
                var results = TrackDescription.GetTrackDescriptions(module);
                if (module != IntPtr.Zero)
                {
                    _manager.ReleaseTrackDescription(module);
                }
                return results;
            }
        }
    }
}
