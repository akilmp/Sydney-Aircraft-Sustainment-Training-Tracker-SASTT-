using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Domain;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.Tasks;

public class IndexModel : PageModel
{
    private readonly SasttDbContext _context;
    public IList<WorkOrderTask> Items { get; private set; } = new List<WorkOrderTask>();

    public IndexModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Items = await _context.WorkOrderTasks.ToListAsync();
    }
}
