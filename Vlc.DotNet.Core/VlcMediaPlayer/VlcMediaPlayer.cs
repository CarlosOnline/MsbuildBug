using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer : IDisposable
    {
        private VlcMediaPlayerInstance _myMediaPlayerInstance;

        public VlcMediaPlayer(DirectoryInfo vlcLibDirectory)
            : this(VlcManager.GetInstance(vlcLibDirectory))
        {
        }

        internal VlcMediaPlayer(VlcManager manager)
        {
            Manager = manager;
            // SUR: Configure VLC options at startup
            Manager.CreateNewInstance(new string[]
            {
#if DEBUG_TODO
                "--extraintf=logger",
                "--verbose=2",
#else
                "--quiet",
#endif
                //"--play-and-pause",   // NOTE: SUR: Pauses at end of video
            });

            _myMediaPlayerInstance = manager.CreateMediaPlayer();
            RegisterEvents();
            Chapters = new ChapterManagement(manager, _myMediaPlayerInstance);
            SubTitles = new SubTitlesManagement(manager, _myMediaPlayerInstance);
            Video = new VideoManagement(manager, _myMediaPlayerInstance);
            Audio = new AudioManagement(manager, _myMediaPlayerInstance);
        }

        internal VlcManager Manager { get; }

        public IntPtr VideoHostControlHandle
        {
            get { return Manager.GetMediaPlayerVideoHostHandle(_myMediaPlayerInstance); }
            set { Manager.SetMediaPlayerVideoHostHandle(_myMediaPlayerInstance, value); }
        }

        private void ResetFromMedia()
        {
            UnregisterEvents();
            if (VideoHostControlHandle != IntPtr.Zero)
            {
                var ctrl = Control.FromHandle(VideoHostControlHandle);
                if (ctrl != null && ctrl.InvokeRequired)
                {
                    ctrl.Invoke(new ResetFromMediaCoreDelegate(ResetFromMediaCore), ctrl);
                }
                else
                {
                    ResetFromMediaCore(ctrl);
                }
            }
            else
            {
                ResetFromMediaCore(null);
            }
        }

        private delegate void ResetFromMediaCoreDelegate(Control ctrl);

        private void ResetFromMediaCore(Control ctrl)
        {
            VideoHostControlHandle = IntPtr.Zero;
            var mediaInstance = GetMedia().MediaInstance;
            if (ctrl != null)
                ctrl.GetType().GetMethod("RecreateHandle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ctrl, null);
            _myMediaPlayerInstance.Pointer = IntPtr.Zero;
            _myMediaPlayerInstance = Manager.CreateMediaPlayerFromMedia(mediaInstance);
            RegisterEvents();
            if (ctrl != null)
            {
                VideoHostControlHandle = ctrl.Handle;
                Chapters = new ChapterManagement(Manager, _myMediaPlayerInstance);
                SubTitles = new SubTitlesManagement(Manager, _myMediaPlayerInstance);
                Video = new VideoManagement(Manager, _myMediaPlayerInstance);
                Audio = new AudioManagement(Manager, _myMediaPlayerInstance);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (_myMediaPlayerInstance == IntPtr.Zero)
                    return;
                UnregisterEvents();
                if (IsPlaying())
                    Stop();

                if (VlcMedia.LoadedMedias.ContainsKey(this))
                    foreach (var loadedMedia in VlcMedia.LoadedMedias[this])
                    {
                        loadedMedia.Dispose();
                    }

                _myMediaPlayerInstance.Dispose();
                Manager.Dispose();
            }
            catch
            {
            }
        }

        ~VlcMediaPlayer()
        {
            Dispose(false);
        }

        public VlcMedia SetMedia(FileInfo file, params string[] options)
        {
            return SetMedia(new VlcMedia(this, file, options));
        }

        public VlcMedia SetMedia(Uri uri, params string[] options)
        {
            return SetMedia(new VlcMedia(this, uri, options));
        }

        private VlcMedia SetMedia(VlcMedia media)
        {
            var currentMedia = GetMedia();
            if (currentMedia != null && currentMedia.MediaInstance != media.MediaInstance)
                currentMedia.Dispose();
            Manager.SetMediaToMediaPlayer(_myMediaPlayerInstance, media.MediaInstance);
            return media;
        }

        public VlcMedia GetMedia()
        {
            var mediaPtr = Manager.GetMediaFromMediaPlayer(_myMediaPlayerInstance);
            if (mediaPtr.Pointer != IntPtr.Zero)
                return new VlcMedia(this, mediaPtr);
            return null;
        }

        public void Play()
        {
            Manager.Play(_myMediaPlayerInstance);
        }

        public void Pause()
        {
            Manager.Pause(_myMediaPlayerInstance);
        }

        public void Stop()
        {
            Manager.Stop(_myMediaPlayerInstance);
        }

        public bool IsPlaying()
        {
            return Manager.IsPlaying(_myMediaPlayerInstance);
        }

        public bool IsPausable()
        {
            return Manager.IsPausable(_myMediaPlayerInstance);
        }

        public void NextFrame()
        {
            Manager.NextFrame(_myMediaPlayerInstance);
        }

        public IEnumerable<FilterModuleDescription> GetAudioFilters()
        {
            var module = Manager.GetAudioFilterList();
            ModuleDescriptionStructure nextModule = (ModuleDescriptionStructure)Marshal.PtrToStructure(module, typeof(ModuleDescriptionStructure));
            var result = GetSubFilter(nextModule);
            if (module != IntPtr.Zero)
                Manager.ReleaseModuleDescriptionInstance(module);
            return result;
        }

        private List<FilterModuleDescription> GetSubFilter(ModuleDescriptionStructure module)
        {
            var result = new List<FilterModuleDescription>();
            var filterModule = FilterModuleDescription.GetFilterModuleDescription(module);
            if (filterModule == null)
            {
                return result;
            }
            result.Add(filterModule);
            if (module.NextModule != IntPtr.Zero)
            {
                ModuleDescriptionStructure nextModule = (ModuleDescriptionStructure)Marshal.PtrToStructure(module.NextModule, typeof(ModuleDescriptionStructure));
                var data = GetSubFilter(nextModule);
                if (data.Count > 0)
                    result.AddRange(data);
            }
            return result;
        }

        public IEnumerable<FilterModuleDescription> GetVideoFilters()
        {
            var module = Manager.GetVideoFilterList();
            ModuleDescriptionStructure nextModule = (ModuleDescriptionStructure)Marshal.PtrToStructure(module, typeof(ModuleDescriptionStructure));
            var result = GetSubFilter(nextModule);
            if (module != IntPtr.Zero)
                Manager.ReleaseModuleDescriptionInstance(module);
            return result;
        }

        public float Position
        {
            get { return Manager.GetMediaPosition(_myMediaPlayerInstance); }
            set { Manager.SetMediaPosition(_myMediaPlayerInstance, value); }
        }

        public bool CouldPlay
        {
            get { return Manager.CouldPlay(_myMediaPlayerInstance); }
        }

        public IChapterManagement Chapters { get; private set; }

        public float Rate
        {
            get { return Manager.GetRate(_myMediaPlayerInstance); }
            set { Manager.SetRate(_myMediaPlayerInstance, value); }
        }

        public MediaStates State
        {
            get { return Manager.GetMediaPlayerState(_myMediaPlayerInstance); }
        }

        public float FramesPerSecond
        {
            get { return Manager.GetFramesPerSecond(_myMediaPlayerInstance); }
        }

        public bool IsSeekable
        {
            get { return Manager.IsSeekable(_myMediaPlayerInstance); }
        }

        public void Navigate(NavigateModes navigateMode)
        {
            Manager.Navigate(_myMediaPlayerInstance, navigateMode);
        }

        public ISubTitlesManagement SubTitles { get; private set; }

        public IVideoManagement Video { get; private set; }

        public IAudioManagement Audio { get; private set; }

        public long Length
        {
            get { return Manager.GetLength(_myMediaPlayerInstance); }
        }

        public long Time
        {
            get { return Manager.GetTime(_myMediaPlayerInstance); }
            set { Manager.SetTime(_myMediaPlayerInstance, value); }
        }

        public void TakeSnapshot(FileInfo file)
        {
            TakeSnapshot(file, 0, 0);
        }
        public void TakeSnapshot(FileInfo file, uint width, uint height)
        {
            Manager.TakeSnapshot(_myMediaPlayerInstance, file, width, height);
        }

        private void RegisterEvents()
        {
            var vlcEventManager = Manager.GetMediaPlayerEventManager(_myMediaPlayerInstance);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, _myOnMediaPlayerBackwardInternalEventCallback = OnMediaPlayerBackwardInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, _myOnMediaPlayerBufferingInternalEventCallback = OnMediaPlayerBufferingInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, _myOnMediaPlayerEncounteredErrorInternalEventCallback = OnMediaPlayerEncounteredErrorInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, _myOnMediaPlayerEndReachedInternalEventCallback = OnMediaPlayerEndReachedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerForward, _myOnMediaPlayerForwardInternalEventCallback = OnMediaPlayerForwardInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, _myOnMediaPlayerLengthChangedInternalEventCallback = OnMediaPlayerLengthChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, _myOnMediaPlayerMediaChangedInternalEventCallback = OnMediaPlayerMediaChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, _myOnMediaPlayerOpeningInternalEventCallback = OnMediaPlayerOpeningInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, _myOnMediaPlayerPausableChangedInternalEventCallback = OnMediaPlayerPausableChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, _myOnMediaPlayerPausedInternalEventCallback = OnMediaPlayerPausedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, _myOnMediaPlayerPlayingInternalEventCallback = OnMediaPlayerPlayingInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, _myOnMediaPlayerPositionChangedInternalEventCallback = OnMediaPlayerPositionChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, _myOnMediaPlayerScrambledChangedInternalEventCallback = OnMediaPlayerScrambledChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, _myOnMediaPlayerSeekableChangedInternalEventCallback = OnMediaPlayerSeekableChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, _myOnMediaPlayerSnapshotTakenInternalEventCallback = OnMediaPlayerSnapshotTakenInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, _myOnMediaPlayerStoppedInternalEventCallback = OnMediaPlayerStoppedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, _myOnMediaPlayerTimeChangedInternalEventCallback = OnMediaPlayerTimeChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, _myOnMediaPlayerTitleChangedInternalEventCallback = OnMediaPlayerTitleChangedInternal);
            Manager.AttachEvent(vlcEventManager, EventTypes.MediaPlayerVout, _myOnMediaPlayerVideoOutChangedInternalEventCallback = OnMediaPlayerVideoOutChangedInternal);
            vlcEventManager.Dispose();
        }

        private void UnregisterEvents()
        {
            var vlcEventManager = Manager.GetMediaPlayerEventManager(_myMediaPlayerInstance);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, _myOnMediaPlayerBackwardInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, _myOnMediaPlayerBufferingInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, _myOnMediaPlayerEncounteredErrorInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, _myOnMediaPlayerEndReachedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerForward, _myOnMediaPlayerForwardInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, _myOnMediaPlayerLengthChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, _myOnMediaPlayerMediaChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, _myOnMediaPlayerOpeningInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, _myOnMediaPlayerPausableChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, _myOnMediaPlayerPausedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, _myOnMediaPlayerPlayingInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, _myOnMediaPlayerPositionChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, _myOnMediaPlayerScrambledChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, _myOnMediaPlayerSeekableChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, _myOnMediaPlayerSnapshotTakenInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, _myOnMediaPlayerStoppedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, _myOnMediaPlayerTimeChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, _myOnMediaPlayerTitleChangedInternalEventCallback);
            Manager.DetachEvent(vlcEventManager, EventTypes.MediaPlayerVout, _myOnMediaPlayerVideoOutChangedInternalEventCallback);
            vlcEventManager.Dispose();
        }
    }
}