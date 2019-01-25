using System;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerTimeChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerTimeChangedEventArgs> TimeChanged;

        private void OnMediaPlayerTimeChangedInternal(IntPtr ptr)
        {
            var args = (VlcEventArg) Marshal.PtrToStructure(ptr, typeof (VlcEventArg));
            OnMediaPlayerTimeChanged(args.MediaPlayerTimeChanged.NewTime);
        }

        public void OnMediaPlayerTimeChanged(long newTime)
        {
            var del = TimeChanged;
            if (del != null)
                del(this, new VlcMediaPlayerTimeChangedEventArgs(newTime));
        }
    }
}