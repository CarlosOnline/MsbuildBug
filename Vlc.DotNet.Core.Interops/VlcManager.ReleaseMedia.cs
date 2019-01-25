using System;
using System.Runtime.ExceptionServices;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        [HandleProcessCorruptedStateExceptions]
        public void ReleaseMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");

            if (Unloaded)
                return;

            try
            {
                GetInteropDelegate<ReleaseMedia>().Invoke(mediaInstance);
            }
            catch
            {
            }
        }
    }
}
