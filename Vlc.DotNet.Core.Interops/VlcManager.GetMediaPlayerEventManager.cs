using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        public VlcMediaPlayerEventManagerInstance GetMediaPlayerEventManager(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (Unloaded)
                return new VlcMediaPlayerEventManagerInstance(this, IntPtr.Zero);

            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return new VlcMediaPlayerEventManagerInstance(this, GetInteropDelegate<GetMediaPlayerEventManager>().Invoke(mediaPlayerInstance));
        }
    }
}
