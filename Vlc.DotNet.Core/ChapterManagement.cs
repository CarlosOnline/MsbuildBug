using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Core
{
    internal sealed class ChapterManagement : IChapterManagement
    {
        private readonly VlcManager _manager;
        private readonly VlcMediaPlayerInstance _mediaPlayerInstance;

        public ChapterManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            _manager = manager;
            _mediaPlayerInstance = mediaPlayerInstance;
        }

        public int Count
        {
            get { return _manager.GetMediaChapterCount(_mediaPlayerInstance); }
        }

        public void Previous()
        {
            _manager.SetPreviousMediaChapter(_mediaPlayerInstance);
        }

        public void Next()
        {
            _manager.SetNextMediaChapter(_mediaPlayerInstance);
        }

        public int Current
        {
            get { return _manager.GetMediaChapter(_mediaPlayerInstance); }
            set { _manager.SetMediaChapter(_mediaPlayerInstance, value); }
        }
    }
}
