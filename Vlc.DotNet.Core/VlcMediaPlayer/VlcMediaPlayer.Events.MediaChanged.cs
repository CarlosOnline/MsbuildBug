using System;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerMediaChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerMediaChangedEventArgs> MediaChanged;

        private void OnMediaPlayerMediaChangedInternal(IntPtr ptr)
        {
            var args = (VlcEventArg) Marshal.PtrToStructure(ptr, typeof (VlcEventArg));

            OnMediaPlayerMediaChanged(new VlcMedia(this, new VlcMediaInstance(Manager, args.MediaPlayerMediaChanged.MediaInstance)));
        }

        public void OnMediaPlayerMediaChanged(VlcMedia media)
        {
            var del = MediaChanged;
            if (del != null)
                del(this, new VlcMediaPlayerMediaChangedEventArgs(media));
        }
    }
}