using System;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.VlcMediaPlayer
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback _myOnMediaPlayerBufferingInternalEventCallback;
        public event EventHandler<VlcMediaPlayerBufferingEventArgs> Buffering;

        private void OnMediaPlayerBufferingInternal(IntPtr ptr)
        {
            var args = (VlcEventArg) Marshal.PtrToStructure(ptr, typeof (VlcEventArg));
            OnMediaPlayerBuffering(args.MediaPlayerBuffering.NewCache);
        }

        public void OnMediaPlayerBuffering(float newCache)
        {
            var del = Buffering;
            if (del != null)
                del(this, new VlcMediaPlayerBufferingEventArgs(newCache));
        }
    }
}