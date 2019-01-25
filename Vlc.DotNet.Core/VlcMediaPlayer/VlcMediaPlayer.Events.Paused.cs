using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerPausedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPausedEventArgs> Paused;

        private void OnMediaPlayerPausedInternal(IntPtr ptr)
        {
            OnMediaPlayerPaused();
        }

        public void OnMediaPlayerPaused()
        {
            var del = Paused;
            if (del != null)
                del(this, new VlcMediaPlayerPausedEventArgs());
        }
    }
}