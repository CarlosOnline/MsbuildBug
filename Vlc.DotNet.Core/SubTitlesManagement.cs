using System.Collections.Generic;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal sealed class SubTitlesManagement : ISubTitlesManagement, IEnumerableManagement<TrackDescription>
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance _mediaPlayerInstance;

        public SubTitlesManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            _mediaPlayerInstance = mediaPlayerInstance;
        }

        public int Count
        {
            get { return _manager.GetVideoSpuCount(_mediaPlayerInstance); }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                var module = _manager.GetVideoSpuDescription(_mediaPlayerInstance);
                var result = TrackDescription.GetSubTrackDescription(module);
                _manager.ReleaseTrackDescription(module);
                return result;
            }
        }


        public TrackDescription Current
        {
            get
            {
                var currentId = _manager.GetVideoSpu(_mediaPlayerInstance);
                foreach (var availableSubTitle in All)
                {
                    if (availableSubTitle.Id == currentId)
                        return availableSubTitle;
                }
                return null;
            }
            set { _manager.SetVideoSpu(_mediaPlayerInstance, value.Id); }
        }

        public long Delay
        {
            get { return _manager.GetVideoSpuDelay(_mediaPlayerInstance); }
            set { _manager.SetVideoSpuDelay(_mediaPlayerInstance, value); }
        }
    }
}
