using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Reports;
using Sastt.Domain;
using Sastt.Domain.Enums;
using Sastt.Infrastructure.Persistence;
using Entities = Sastt.Domain.Entities;
using Xunit;

namespace Sastt.IntegrationTests;

public class ReportGenerationTests
{
    private static WeeklyReportService CreateService()
    {
        var options = new DbContextOptionsBuilder<SasttDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new SasttDbContext(options);
        context.WorkOrders.Add(new Entities.WorkOrder { Title = "WO", AircraftId = Guid.NewGuid(), Priority = Priority.Medium, PlannedStart = DateTime.Today, PlannedEnd = DateTime.Today.AddDays(1), ActualStart = DateTime.Today, ActualEnd = DateTime.Today.AddDays(1) });
        context.TrainingSessions.Add(new TrainingSession { PilotId = 1, Start = DateTime.Today, End = DateTime.Today.AddHours(2), Result = "PASS", Notes = "n" });
        context.SaveChanges();
        return new WeeklyReportService(context);
    }

    [Fact]
    public async Task GeneratesTrainingCsv()
    {
        var service = CreateService();
        var bytes = await service.GenerateWeeklyTrainingCsvAsync();
        var text = Encoding.UTF8.GetString(bytes);
        Assert.Contains("Result", text);
        Assert.Contains("PASS", text);
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
        Assert.Contains("Priority", text);
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
