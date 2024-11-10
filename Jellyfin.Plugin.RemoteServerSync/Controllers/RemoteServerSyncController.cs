using Microsoft.AspNetCore.Mvc;
using Jellyfin.Plugin.RemoteServerSync.Models;
using Jellyfin.Plugin.RemoteServerSync.Helpers;
using Jellyfin.Plugin.RemoteServerSync.Services;

namespace Jellyfin.Plugin.RemoteServerSync.Controllers
{
    [ApiController]
    [Route("RemoteServerSync")]
    public class RemoteServerSyncController : ControllerBase
    {
        private readonly ServerSyncHelper _syncHelper;
        private readonly RemoteSyncService _syncService;
        private readonly RemoteServerSyncPlugin _plugin;

        public RemoteServerSyncController(
            ServerSyncHelper syncHelper,
            RemoteSyncService syncService,
            RemoteServerSyncPlugin plugin)
        {
            _syncHelper = syncHelper;
            _syncService = syncService;
             _plugin = plugin;
        }

        [HttpPost("SyncAll")]
        public async Task<ActionResult> SyncAllServers()
        {
            foreach (var server in _plugin.Configuration.RemoteServers)
            {
                if (server.SyncUsers)
                    await _syncHelper.SyncUsersAsync(server);
                
                if (server.SyncLibraries)
                    await _syncService.SyncLibrariesAsync(server);
            }
            return Ok();
        }

        [HttpGet("RemoteMedia")]
        public async Task<ActionResult<List<MediaItemDto>>> GetRemoteMedia(string serverName, string libraryName)
        {
            var server = _plugin.Configuration.RemoteServers
                .FirstOrDefault(s => s.Name == serverName);
            
            if (server == null)
                return NotFound();

            var mediaItems = await _syncHelper.GetRemoteMediaAsync(server, libraryName);
            return Ok(mediaItems);
        }
    }
}