namespace Jellyfin.Plugin.RemoteServerSync.Models
{
    public class MediaItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string ServerId { get; set; }
    }
}