using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures;
#pragma warning disable 659

namespace Vlc.DotNet.Core
{
    public sealed class TrackDescription
    {
        public int Id { get; }
        public string Name { get; }

        public TrackDescription(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }

        public override bool Equals(object obj)
        {
            var right = obj as TrackDescription;
            if (right == null)
                return false;

            return right.Name == Name;
        }

        internal static List<TrackDescription> GetTrackDescriptions(IntPtr trackDescriptionsPointer)
        {
            var results = new List<TrackDescription>();
            var trackDescriptionPointer = trackDescriptionsPointer;
            while (trackDescriptionPointer != IntPtr.Zero)
            {
                var trackDescription = (TrackDescriptionStructure)Marshal.PtrToStructure(trackDescriptionPointer, typeof(TrackDescriptionStructure));
                results.Add(new TrackDescription(trackDescription.Id, NativeString.String(trackDescription.Name)));
                trackDescriptionPointer = trackDescription.NextTrackDescription;
            }
            return results;
        }

        internal static List<TrackDescription> GetSubTrackDescription(IntPtr trackDescriptionsPointer)
        {
            return GetTrackDescriptions(trackDescriptionsPointer);
        }
    }
}
