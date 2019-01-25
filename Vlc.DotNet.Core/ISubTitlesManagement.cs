namespace Vlc.DotNet.Core
{
    public interface ISubTitlesManagement : IEnumerableManagement<TrackDescription>
    {
        long Delay { get; set; }
    }

}
