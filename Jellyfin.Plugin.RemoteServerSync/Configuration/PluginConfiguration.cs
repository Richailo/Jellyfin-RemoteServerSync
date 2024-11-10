using MediaBrowser.Model.Plugins;
using Jellyfin.Plugin.RemoteServerSync.Models;
using System.Collections.Generic;

namespace Jellyfin.Plugin.RemoteServerSync.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public List<RemoteServer> RemoteServers { get; set; } = new();
        public bool AutoSync { get; set; } = false;
        public int SyncIntervalMinutes { get; set; } = 60;
    }
}