using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerStoppedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerStoppedEventArgs> Stopped;

        private void OnMediaPlayerStoppedInternal(IntPtr ptr)
        {
            OnMediaPlayerStopped();
        }

        public void OnMediaPlayerStopped()
        {
            var del = Stopped;
            if (del != null)
                del(this, new VlcMediaPlayerStoppedEventArgs());
        }
    }
}