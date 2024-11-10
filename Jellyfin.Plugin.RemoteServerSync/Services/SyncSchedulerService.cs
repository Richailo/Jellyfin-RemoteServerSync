using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.RemoteServerSync.Services
{
    public class SyncSchedulerService : IHostedService
    {
        private readonly ServerSyncHelper _syncHelper;
        private readonly RemoteServerSyncPlugin _plugin;
        private Timer _timer;

        public SyncSchedulerService(ServerSyncHelper syncHelper, RemoteServerSyncPlugin plugin)
        {
            _syncHelper = syncHelper;
            _plugin = plugin;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = _plugin.Configuration.SyncIntervalMinutes * 60 * 1000;
            _timer = new Timer(DoSync, null, 0, interval);
            return Task.CompletedTask;
        }

        private async void DoSync(object state)
        {
            if (_plugin.Configuration.AutoSync)
            {
                foreach (var server in _plugin.Configuration.RemoteServers)
                {
                    await _syncHelper.SyncUsersAsync(server);
                    await _syncHelper.SyncLibrariesAsync(server);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}