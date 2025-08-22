using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sastt.Domain;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.Tasks;

public class DetailsModel : PageModel
{
    private readonly SasttDbContext _context;
    public WorkOrderTask? Item { get; private set; }

    public DetailsModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Item = await _context.WorkOrderTasks.FindAsync(id);
        if (Item == null)
        {
            return NotFound();
        }
        return Page();
    }
}
