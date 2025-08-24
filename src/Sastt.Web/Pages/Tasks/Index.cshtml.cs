using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Infrastructure.Persistence;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Web.Pages.Tasks;

public class IndexModel : PageModel
{
    private readonly SasttDbContext _context;
    public IList<TaskEntity> Items { get; private set; } = new List<TaskEntity>();

    public IndexModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Items = await _context.Tasks.ToListAsync();
    }
}
