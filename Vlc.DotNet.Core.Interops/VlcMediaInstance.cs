using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed class VlcMediaInstance : InteropObjectInstance
    {
        private readonly VlcManager _manager;

        internal VlcMediaInstance(VlcManager manager, IntPtr pointer) : base(pointer)
        {
            _manager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (Pointer != IntPtr.Zero)
                    _manager.ReleaseMedia(this);
                base.Dispose(disposing);
            }
            catch
            {
            }
        }

        public static implicit operator IntPtr(VlcMediaInstance instance)
        {
            return instance.Pointer;
        }
    }
}