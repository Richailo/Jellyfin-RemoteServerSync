using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Drawing;

namespace Jellyfin.Plugin.RemoteServerSync
{
    public class Plugin : BasePlugin, IHasWebPages
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "Remote Server Sync";
        public override string Description => "Synchronize Jellyfin servers and allow streaming from connected servers.";

        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "RemoteServerSync",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.RemoteServerSync.html",
                }
            };
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RemoteServerSync
{
    public class ServerSyncService
    {
        private readonly HttpClient _httpClient;

        public ServerSyncService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> FetchDataFromRemoteServer(string serverUrl, string apiKey)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/Users");
            request.Headers.Add("X-Emby-Token", apiKey);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
