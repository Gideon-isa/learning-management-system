using Lms.Enrollment.Infrastructure.DataContext;
using Lms.Shared.IntegrationEvents.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Lms.Enrollment.Infrastructure.Outbox
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private const int MaxRetryCount = 5;
        private static readonly TimeSpan RetryDelay = TimeSpan.FromMinutes(5);

        public OutboxProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateAsyncScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<EnrollmentSupportDbContext>();
                    //var outbox = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();
                    var publish = scope.ServiceProvider.GetRequiredService<MediatRIntegrationEventPublisher>();

                    // Pick pending or failed events ready for retry
                    var now = DateTime.UtcNow;
                    var pendingMessages = await dbContext.EnrollmentOutboxMessages
                        .Where(m =>
                            (m.ProcessedOn == null || m.Error != null) &&
                            (m.NextRetryOn == null || m.NextRetryOn <= now))
                        .OrderBy(m => m.OccuredOn)
                        .Take(10)
                        .ToListAsync(stoppingToken);


                    foreach (var message in pendingMessages)
                    {
                        try
                        {
                            var eventType = Type.GetType(message.Type)!;
                            if (eventType is null)
                            {
                                // Log thr error 
                                continue;
                            }
                            var integrationEvent = JsonSerializer.Deserialize(message.Content, eventType);

                            // TODO: Publish the event to message bus or mediator
                            // e.g. await _bus.PublishAsync(@event);
                            await publish.PublishEventAsync((dynamic)integrationEvent, stoppingToken);

                            //await outbox.PublishAsync((dynamic)integrationEvent!, stoppingToken); This should be in the handler for

                            message.ProcessedOn = DateTime.UtcNow;
                            message.Error = null;
                        }
                        catch (Exception ex)
                        {
                            message.RetryCount++;
                            message.Error = ex.Message;
                            message.NextRetryOn = DateTime.UtcNow.Add(RetryDelay);
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                catch (Exception)
                {

                    throw;
                }

                await Task.Delay(TimeSpan.FromSeconds(200), stoppingToken);

            }
        }
    }
}
