using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sastt.Domain.Entities;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.WorkOrders;

public class DetailsModel : PageModel
{
    private readonly SasttDbContext _context;
    public WorkOrder? Item { get; private set; }

    public DetailsModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Item = await _context.WorkOrders.FindAsync(id);
        if (Item == null)
        {
            return NotFound();
        }
        return Page();
    }
}
