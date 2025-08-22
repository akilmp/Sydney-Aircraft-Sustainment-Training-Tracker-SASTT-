using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Domain.Entities;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.TrainingSessions;

public class IndexModel : PageModel
{
    private readonly SasttDbContext _context;
    public IList<TrainingSession> Items { get; private set; } = new List<TrainingSession>();

    public IndexModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Items = await _context.TrainingSessions.ToListAsync();
    }
}
