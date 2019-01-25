using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interops.Signatures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackDescriptionStructure
    {
        public int Id;
        public IntPtr Name;
        public IntPtr NextTrackDescription;

        //public int i_id;
        //public IntPtr psz_name;
        //public IntPtr p_next;
    }
}
