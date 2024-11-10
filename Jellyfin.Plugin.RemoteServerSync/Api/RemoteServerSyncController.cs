[ApiController]
[Route("RemoteServerSync")]
public class RemoteServerSyncController : ControllerBase
{
    private readonly ServerSyncHelper _syncHelper;
    private readonly RemoteServerSyncPlugin _plugin;

    public RemoteServerSyncController(
        ServerSyncHelper syncHelper, 
        RemoteServerSyncPlugin plugin)
    {
        _syncHelper = syncHelper;
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
                await _syncHelper.SyncLibrariesAsync(server);
        }
        return Ok();
    }

    [HttpGet("RemoteMedia")]
    public async Task<ActionResult<List<MediaItem>>> GetRemoteMedia(
        string serverName, 
        string libraryName)
    {
        var server = _plugin.Configuration.RemoteServers
            .FirstOrDefault(s => s.ServerName == serverName);
        
        if (server == null)
            return NotFound();

        var mediaItems = await _syncHelper.GetRemoteMediaAsync(server, libraryName);
        return Ok(mediaItems);
    }
}