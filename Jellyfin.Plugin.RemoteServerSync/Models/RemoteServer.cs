namespace Jellyfin.Plugin.RemoteServerSync.Models
{
    public class RemoteServer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public bool SyncUsers { get; set; }
        public bool SyncLibraries { get; set; }
        public List<string> SyncedLibraries { get; set; } = new();
    }
}