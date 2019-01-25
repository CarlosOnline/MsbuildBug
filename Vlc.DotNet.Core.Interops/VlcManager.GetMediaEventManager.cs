using System;
using System.Runtime.ExceptionServices;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        [HandleProcessCorruptedStateExceptions]
        public VlcMediaEventManagerInstance GetMediaEventManager(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");

            if (Unloaded)
                return null;

            return new VlcMediaEventManagerInstance(this, GetInteropDelegate<GetMediaEventManager>().Invoke(mediaInstance));
        }
    }
}
