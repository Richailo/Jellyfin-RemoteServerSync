public class SyncMetricsService
{
    private readonly IMetricsClient _metricsClient;

    public void TrackSyncPerformance(SyncResult result)
    {
        _metricsClient.RecordMetric("sync_duration", result.Duration);
        _metricsClient.RecordMetric("synced_users_count", result.SyncedUsersCount);
        _metricsClient.RecordMetric("synced_libraries_count", result.SyncedLibrariesCount);

        if (result.HasErrors)
        {
            _metricsClient.IncrementCounter("sync_errors");
        }
    }
}

public class SyncResult
{
    public TimeSpan Duration { get; set; }
    public int SyncedUsersCount { get; set; }