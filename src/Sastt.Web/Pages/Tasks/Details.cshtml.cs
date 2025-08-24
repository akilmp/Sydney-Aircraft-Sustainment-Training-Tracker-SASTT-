using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sastt.Infrastructure.Persistence;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Web.Pages.Tasks;

public class DetailsModel : PageModel
{
    private readonly SasttDbContext _context;
    public TaskEntity? Item { get; private set; }

    public DetailsModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Item = await _context.Tasks.FindAsync(id);
        if (Item == null)
        {
            return NotFound();
        }
        return Page();
    }
}
