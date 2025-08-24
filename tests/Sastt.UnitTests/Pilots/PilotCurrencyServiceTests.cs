using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Application.Pilots;
using Sastt.Domain;
using Sastt.Domain.Entities;
using Xunit;

namespace Sastt.UnitTests.Pilots;

public class PilotCurrencyServiceTests
{
    private readonly TestDbContext _context;
    private readonly PilotCurrencyService _service;

    public PilotCurrencyServiceTests()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new TestDbContext(options);
        _service = new PilotCurrencyService(_context);
    }

    [Fact]
    public async Task UpdateCurrencyAsync_CreatesRecord_With60DayExpiry()
    {
        var pilotId = Guid.NewGuid();
        var completion = new DateTime(2024, 1, 1);

        await _service.UpdateCurrencyAsync(pilotId, completion, CancellationToken.None);

        var currency = await _context.PilotCurrencies.SingleAsync();
        Assert.Equal(pilotId, currency.PilotId);
        Assert.Equal(completion.AddDays(60), currency.ExpirationDate);
    }

    [Fact]
    public async Task UpdateCurrencyAsync_UpdatesExistingRecord()
    {
        var pilotId = Guid.NewGuid();
        var existing = new PilotCurrency
        {
            PilotId = pilotId,
            CurrencyType = "GENERAL",
            ExpirationDate = new DateTime(2024, 1, 15)
        };
        _context.PilotCurrencies.Add(existing);
        await _context.SaveChangesAsync();

        var completion = new DateTime(2024, 2, 1);
        await _service.UpdateCurrencyAsync(pilotId, completion, CancellationToken.None);

        var currency = await _context.PilotCurrencies.SingleAsync();
        Assert.Equal(completion.AddDays(60), currency.ExpirationDate);
    }

    private class TestDbContext : DbContext, IApplicationDbContext
    {
        public TestDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Aircraft> Aircraft => Set<Aircraft>();
        public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
        public DbSet<WorkOrderTask> WorkOrderTasks => Set<WorkOrderTask>();
        public DbSet<Defect> Defects => Set<Defect>();
        public DbSet<Pilot> Pilots => Set<Pilot>();
        public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
        public DbSet<PilotCurrency> PilotCurrencies => Set<PilotCurrency>();
    }
}

