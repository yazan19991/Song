using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class CacheCleanupService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ServerSideProject.Model.UpComingEvents.CleanUpCache();

            // Wait for one hour before cleaning up the cache again.
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}
