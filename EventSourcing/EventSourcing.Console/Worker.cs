using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventSourcing.Console
{
	public sealed class Worker(IHostApplicationLifetime hostApplicationLifetime, 
								ILogger<Worker> logger) : BackgroundService
	{
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				await Task.Delay(1_000, stoppingToken);
				hostApplicationLifetime.StopApplication();
			}
		}
	}
}
