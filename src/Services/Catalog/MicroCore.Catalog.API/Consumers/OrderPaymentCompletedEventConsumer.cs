using MassTransit;
using MicroCore.Bus.Events;
using MicroCore.Catalog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MicroCore.Catalog.API.Consumers;

public class OrderPaymentCompletedEventConsumer(CatalogDbContext context, ILogger<OrderPaymentCompletedEventConsumer> logger)
    : IConsumer<OrderPaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentCompletedEvent> contextEvent)
    {
        var courseIds = contextEvent.Message.CourseIds;

        if (courseIds == null || !courseIds.Any()) return;

        logger.LogInformation("Sipariş yakalandı. {Count} adet kursun satış sayısı artırılacak.", courseIds.Count);

        var courses = await context.Courses
            .Where(x => courseIds.Contains(x.Id))
            .ToListAsync();

        foreach (var course in courses)
        {
            course.PurchaseCount += 1;
        }

        await context.SaveChangesAsync();

        logger.LogInformation("Kursların satış sayısı başarıyla güncellendi.");
    }
}