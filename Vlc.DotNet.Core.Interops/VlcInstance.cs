using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed class VlcInstance : InteropObjectInstance
    {
        private readonly VlcManager _manager;

        internal VlcInstance(VlcManager manager, IntPtr pointer) : base(pointer)
        {
            _manager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            if (Pointer != IntPtr.Zero)
                _manager.ReleaseInstance(this);
            base.Dispose(disposing);            
        }

        public static implicit operator IntPtr(VlcInstance instance)
        {
            return instance.Pointer;
        }
    }
}