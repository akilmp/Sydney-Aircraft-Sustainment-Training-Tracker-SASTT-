using System.Threading;
using System.Threading.Tasks;

namespace Sastt.Application.Reports;

public interface IWeeklyReportService
{
    Task<byte[]> GenerateWeeklySustainmentCsvAsync(CancellationToken cancellationToken = default);
    Task<byte[]> GenerateWeeklySustainmentPdfAsync(CancellationToken cancellationToken = default);
    Task<byte[]> GenerateWeeklyTrainingCsvAsync(CancellationToken cancellationToken = default);
    Task<byte[]> GenerateWeeklyTrainingPdfAsync(CancellationToken cancellationToken = default);
}
