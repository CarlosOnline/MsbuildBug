using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops.Signatures;
using Vlc.DotNet.Core.VlcMediaPlayer;
using Vlc.DotNet.Forms.TypeEditors;

namespace Vlc.DotNet.Forms
{
    public partial class VlcControl : Control, ISupportInitialize
    {
        private VlcMediaPlayer _vlcMediaPlayer;

        [Editor(typeof(DirectoryEditor), typeof(UITypeEditor))]
        public DirectoryInfo VlcLibDirectory { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsPlaying => _vlcMediaPlayer?.IsPlaying() ?? false;

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            if (IsInDesignMode() || _vlcMediaPlayer != null)
                return;
            if (VlcLibDirectory == null && (VlcLibDirectory = OnVlcLibDirectoryNeeded()) == null)
            {
                throw new Exception("'VlcLibDirectory' must be set.");
            }
            _vlcMediaPlayer = new VlcMediaPlayer(VlcLibDirectory);
            _vlcMediaPlayer.VideoHostControlHandle = Handle;
            RegisterEvents();
        }

        // work around http://stackoverflow.com/questions/34664/designmode-with-controls/708594
        private static bool IsInDesignMode()
        {
            return Assembly.GetExecutingAssembly().Location.Contains("VisualStudio");
        }

        public event EventHandler<VlcLibDirectoryNeededEventArgs> VlcLibDirectoryNeeded;

        protected override void Dispose(bool disposing)
        {
            if (_vlcMediaPlayer != null)
            {
                UnregisterEvents();
                _vlcMediaPlayer.Dispose();
                base.Dispose(disposing);
                GC.SuppressFinalize(this);
            }
        }

        public DirectoryInfo OnVlcLibDirectoryNeeded()
        {
            var del = VlcLibDirectoryNeeded;
            if (del != null)
            {
                var args = new VlcLibDirectoryNeededEventArgs();
                del(this, args);
                return args.VlcLibDirectory;
            }
            return null;
        }

        public void Play()
        {
            EndInit();
            _vlcMediaPlayer.Play();
        }

        public void Play(FileInfo file, params string[] options)
        {
            EndInit();
            _vlcMediaPlayer.SetMedia(file, options);
            Play();
        }

        public void Play(Uri uri, params string[] options)
        {
            EndInit();
            _vlcMediaPlayer.SetMedia(uri, options);
            Play();
        }

        public void Pause()
        {
            EndInit();
            _vlcMediaPlayer.Pause();
        }

        public void Stop()
        {
            EndInit();
            _vlcMediaPlayer.Stop();
        }

        public void NextFame()
        {
            EndInit();
            _vlcMediaPlayer.NextFrame();
        }

        public VlcMedia GetCurrentMedia()
        {
            EndInit();
            return _vlcMediaPlayer?.GetMedia();
        }

        public float Position
        {
            get { return _vlcMediaPlayer?.Position ?? 0; }
            set { _vlcMediaPlayer.Position = value; }
        }

        public IChapterManagement Chapter => _vlcMediaPlayer?.Chapters;

        public float Rate
        {
            get { return _vlcMediaPlayer?.Rate ?? 0; }
            set { _vlcMediaPlayer.Rate = value; }
        }

        public MediaStates State => _vlcMediaPlayer?.State ?? 0;

        public ISubTitlesManagement SubTitles => _vlcMediaPlayer?.SubTitles;

        public IVideoManagement Video => _vlcMediaPlayer?.Video;

        public IAudioManagement Audio => _vlcMediaPlayer?.Audio;

        public long Length => _vlcMediaPlayer?.Length ?? 0;

        public long Time
        {
            get { return _vlcMediaPlayer?.Time ?? 0; }
            set { _vlcMediaPlayer.Time = value; }
        }

        public void SetMedia(FileInfo file, params string[] options)
        {
            EndInit();
            _vlcMediaPlayer.SetMedia(file, options);
        }

        public void SetMedia(Uri file, params string[] options)
        {
            EndInit();
            _vlcMediaPlayer.SetMedia(file, options);
        }

        public void TakeSnapshot(FileInfo fi)
        {
            _vlcMediaPlayer.TakeSnapshot(fi);
        }

        public string LastErrorMessage => _vlcMediaPlayer?.Manager?.GetLastErrorMessage();
    }
}