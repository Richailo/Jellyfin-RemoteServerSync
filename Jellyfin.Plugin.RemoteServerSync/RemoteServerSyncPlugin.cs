using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using Jellyfin.Plugin.RemoteServerSync.Configuration;

namespace Jellyfin.Plugin.RemoteServerSync
{
    public class RemoteServerSyncPlugin : BasePlugin<PluginConfiguration>
    {
        public static RemoteServerSyncPlugin Instance { get; private set; }

        public RemoteServerSyncPlugin(
            IApplicationPaths applicationPaths, 
            IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "Remote Server Sync";
        public override Guid Id => Guid.Parse("f3e4471f-2a4e-4c5e-9c07-123456789abc");
    }
}