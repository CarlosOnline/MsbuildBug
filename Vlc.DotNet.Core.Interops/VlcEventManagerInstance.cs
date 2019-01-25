using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public abstract class VlcEventManagerInstance : InteropObjectInstance
    {
        private readonly VlcManager _manager;

        internal VlcEventManagerInstance(VlcManager manager, IntPtr pointer) : base(pointer)
        {
            _manager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcEventManagerInstance instance)
        {
            return instance.Pointer;
        }
    }
}