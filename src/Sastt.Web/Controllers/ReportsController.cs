using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Reports;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Controllers;

[Route("reports")]
public class ReportsController : Controller
{
    private readonly SasttDbContext _context;
    private readonly WorkOrderReportService _workOrders;
    private readonly TrainingReportService _training;

    public ReportsController(SasttDbContext context, WorkOrderReportService workOrders, TrainingReportService training)
    {
        _context = context;
        _workOrders = workOrders;
        _training = training;
    }

    [HttpGet("workorders/csv")]
    public async Task<IActionResult> WorkOrdersCsv()
    {
        var workOrders = await _context.WorkOrders
            .Include(w => w.Aircraft)
            .Include(w => w.Tasks)
            .Include(w => w.Defects)
            .ToListAsync();

        var bytes = _workOrders.GenerateCsv(workOrders);
        return File(bytes, "text/csv", "workorders.csv");
    }

    [HttpGet("workorders/pdf")]
    public async Task<IActionResult> WorkOrdersPdf()
    {
        var workOrders = await _context.WorkOrders
            .Include(w => w.Aircraft)
            .Include(w => w.Tasks)
            .Include(w => w.Defects)
            .ToListAsync();

        var bytes = _workOrders.GeneratePdf(workOrders);
        return File(bytes, "application/pdf", "workorders.pdf");
    }

    [HttpGet("trainingsessions/csv")]
    public async Task<IActionResult> TrainingCsv()
    {
        var sessions = await _context.TrainingSessions
            .Include(t => t.Pilot)
            .ToListAsync();

        var bytes = _training.GenerateCsv(sessions);
        return File(bytes, "text/csv", "trainingsessions.csv");
    }

    [HttpGet("trainingsessions/pdf")]
    public async Task<IActionResult> TrainingPdf()
    {
        var sessions = await _context.TrainingSessions
            .Include(t => t.Pilot)
            .ToListAsync();

        var bytes = _training.GeneratePdf(sessions);
        return File(bytes, "application/pdf", "trainingsessions.pdf");
    }
}

