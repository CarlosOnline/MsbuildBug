using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed class VlcMediaPlayerInstance : InteropObjectInstance
    {
        private readonly VlcManager _manager;

        internal VlcMediaPlayerInstance(VlcManager manager, IntPtr pointer) : base(pointer)
        {
            _manager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (Pointer != IntPtr.Zero)
                    _manager.ReleaseMediaPlayer(this);
                base.Dispose(disposing);
            }
            catch
            {
            }
        }

        public static implicit operator IntPtr(VlcMediaPlayerInstance instance)
        {
            return instance != null
                ? instance.Pointer
                : IntPtr.Zero;
        }
    }
}