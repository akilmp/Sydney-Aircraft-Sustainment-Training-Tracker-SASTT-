using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.WorkOrders.Commands;
using Sastt.Domain;
using Sastt.Domain.Enums;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.WorkOrders;

public class DetailsModel : PageModel
{
    private readonly SasttDbContext _context;
    private readonly IMediator _mediator;
    public WorkOrder? Item { get; private set; }
    public IEnumerable<WorkOrderStatus> AllowedTransitions { get; private set; } = Enumerable.Empty<WorkOrderStatus>();

    public DetailsModel(SasttDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Item = await _context.WorkOrders
            .Include(w => w.Tasks)
            .Include(w => w.Defects)
            .FirstOrDefaultAsync(w => w.Id == id);
        if (Item == null)
        {
            return NotFound();
        }
        AllowedTransitions = Item.Status switch
        {
            WorkOrderStatus.Open => new[] { WorkOrderStatus.InProgress, WorkOrderStatus.Cancelled },
            WorkOrderStatus.InProgress => new[] { WorkOrderStatus.QaReview, WorkOrderStatus.Cancelled },
            WorkOrderStatus.QaReview => new[] { WorkOrderStatus.Completed, WorkOrderStatus.Deferred },
            WorkOrderStatus.Deferred => new[] { WorkOrderStatus.InProgress, WorkOrderStatus.Cancelled },
            _ => Enumerable.Empty<WorkOrderStatus>()
        };
        return Page();
    }

    public async Task<IActionResult> OnPostTransitionAsync(int id, WorkOrderStatus status)
    {
        await _mediator.Send(new TransitionWorkOrderStatusCommand(id, status));
        return RedirectToPage(new { id });
    }
}
