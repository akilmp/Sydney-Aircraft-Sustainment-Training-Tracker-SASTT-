using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sastt.Application.Reports;

namespace Sastt.Web.Controllers;

[Route("reports")]
public class ReportsController : Controller
{
    private readonly IWeeklyReportService _service;

    public ReportsController(IWeeklyReportService service)
    {
        _service = service;
    }

    [HttpGet("training.csv")]
    public async Task<IActionResult> GetTrainingCsv()
    {
        var bytes = await _service.GenerateWeeklyTrainingCsvAsync();
        return File(bytes, "text/csv", "training-weekly.csv");
    }

    [HttpGet("training.pdf")]
    public async Task<IActionResult> GetTrainingPdf()
    {
        var bytes = await _service.GenerateWeeklyTrainingPdfAsync();
        return File(bytes, "application/pdf", "training-weekly.pdf");
    }

    [HttpGet("sustainment.csv")]
    public async Task<IActionResult> GetSustainmentCsv()
    {
        var bytes = await _service.GenerateWeeklySustainmentCsvAsync();
        return File(bytes, "text/csv", "sustainment-weekly.csv");
    }

    [HttpGet("sustainment.pdf")]
    public async Task<IActionResult> GetSustainmentPdf()
    {
        var bytes = await _service.GenerateWeeklySustainmentPdfAsync();
        return File(bytes, "application/pdf", "sustainment-weekly.pdf");
    }
}
