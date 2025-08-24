using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;

namespace Sastt.Application.Reports;

public class WeeklyReportService : IWeeklyReportService
{
    private readonly IApplicationDbContext _context;

    public WeeklyReportService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerateWeeklySustainmentCsvAsync(CancellationToken cancellationToken = default)
    {
        var workOrders = await _context.WorkOrders
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var sb = new StringBuilder();
        sb.AppendLine("WorkOrderId,Title,AircraftId");
        foreach (var wo in workOrders)
        {
            var title = wo.Title.Replace("\"", "\"\"");
            sb.AppendLine($"{wo.Id},\"{title}\",{wo.AircraftId}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<byte[]> GenerateWeeklySustainmentPdfAsync(CancellationToken cancellationToken = default)
    {
        var csvBytes = await GenerateWeeklySustainmentCsvAsync(cancellationToken);
        var text = Encoding.UTF8.GetString(csvBytes);
        return BuildPdfFromText(text);
    }

    public async Task<byte[]> GenerateWeeklyTrainingCsvAsync(CancellationToken cancellationToken = default)
    {
        var cutoff = DateTime.Today.AddDays(-7);
        var sessions = await _context.TrainingSessions
            .AsNoTracking()
            .Where(s => s.Date >= cutoff)
            .ToListAsync(cancellationToken);

        var sb = new StringBuilder();
        sb.AppendLine("SessionId,PilotId,Date,Hours");
        foreach (var s in sessions)
        {
            sb.AppendLine($"{s.Id},{s.PilotId},{s.Date:O},{s.Hours}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<byte[]> GenerateWeeklyTrainingPdfAsync(CancellationToken cancellationToken = default)
    {
        var csvBytes = await GenerateWeeklyTrainingCsvAsync(cancellationToken);
        var text = Encoding.UTF8.GetString(csvBytes);
        return BuildPdfFromText(text);
    }

    private static byte[] BuildPdfFromText(string text)
    {
        var sb = new StringBuilder();
        sb.AppendLine("%PDF-1.1");
        sb.AppendLine(text.Replace("\r\n", "\n"));
        sb.AppendLine("%%EOF");
        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
