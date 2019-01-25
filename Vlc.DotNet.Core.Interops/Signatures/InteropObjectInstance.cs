using System;

namespace Vlc.DotNet.Core.Interops.Signatures
{
    public abstract class InteropObjectInstance : IDisposable
    {
        internal IntPtr Pointer { get; set; }

        public delegate void OnDisposedHandler();
        public event OnDisposedHandler OnDisposed;

        private bool myIsDisposing;

        protected InteropObjectInstance(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public void Dispose()
        {
            if (myIsDisposing)
                return;

            if (OnDisposed != null)
                OnDisposed();

            myIsDisposing = true;
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Pointer = IntPtr.Zero;
        }

        ~InteropObjectInstance()
        {
            Dispose(false);
        }

        public override bool Equals(object obj)
        {
            var instance = obj as InteropObjectInstance;
            if (instance != null)
                return instance.Pointer == Pointer;
            return false;
        }

        public override int GetHashCode()
        {
            return Pointer.ToInt32();
        }

        public static bool operator ==(InteropObjectInstance a, InteropObjectInstance b)
        {
            if (Equals(a, null) || Equals(b, null))
                return Equals(a, b);

            return a.Pointer.Equals(b.Pointer);
        }

        public static bool operator !=(InteropObjectInstance a, InteropObjectInstance b)
        {
            return !(a == b);
        }
    }
}