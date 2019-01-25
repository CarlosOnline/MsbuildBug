using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerOpeningInternalEventCallback;
        public event EventHandler<VlcMediaPlayerOpeningEventArgs> Opening;

        private void OnMediaPlayerOpeningInternal(IntPtr ptr)
        {
            OnMediaPlayerOpening();
        }

        public void OnMediaPlayerOpening()
        {
            var del = Opening;
            if (del != null)
                del(this, new VlcMediaPlayerOpeningEventArgs());
        }
    }
}