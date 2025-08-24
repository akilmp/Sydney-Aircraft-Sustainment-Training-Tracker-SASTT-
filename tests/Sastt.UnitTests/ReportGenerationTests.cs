using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Reports;
using Sastt.Domain;
using Sastt.Infrastructure.Persistence;
using Xunit;

namespace Sastt.UnitTests;

public class ReportGenerationTests
{
    private static WeeklyReportService CreateService()
    {
        var options = new DbContextOptionsBuilder<SasttDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new SasttDbContext(options);
        context.WorkOrders.Add(new WorkOrder { Id = 1, Title = "WO", AircraftId = 1 });
        context.TrainingSessions.Add(new TrainingSession { Id = 1, PilotId = 1, Date = DateTime.Today, Hours = 2 });
        context.SaveChanges();
        return new WeeklyReportService(context);
    }

    [Fact]
    public async Task GeneratesTrainingCsv()
    {
        var service = CreateService();
        var bytes = await service.GenerateWeeklyTrainingCsvAsync();
        var text = Encoding.UTF8.GetString(bytes);
        Assert.Contains("SessionId", text);
        Assert.Contains(",1,", text);
    }

    [Fact]
    public async Task GeneratesTrainingPdf()
    {
        var service = CreateService();
        var bytes = await service.GenerateWeeklyTrainingPdfAsync();
        var text = Encoding.UTF8.GetString(bytes);
        Assert.StartsWith("%PDF", text);
    }

    [Fact]
    public async Task GeneratesSustainmentCsv()
    {
        var service = CreateService();
        var bytes = await service.GenerateWeeklySustainmentCsvAsync();
        var text = Encoding.UTF8.GetString(bytes);
        Assert.Contains("WorkOrderId", text);
        Assert.Contains("WO", text);
    }

    [Fact]
    public async Task GeneratesSustainmentPdf()
    {
        var service = CreateService();
        var bytes = await service.GenerateWeeklySustainmentPdfAsync();
        var text = Encoding.UTF8.GetString(bytes);
        Assert.StartsWith("%PDF", text);
    }
}
