using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Domain.Entities;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Web.Pages.Pilots;

public class DetailsModel : PageModel
{
    private readonly SasttDbContext _context;
    public Pilot? Item { get; private set; }

    public DetailsModel(SasttDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Item = await _context.Pilots.Include(p => p.TrainingSessions).FirstOrDefaultAsync(p => p.Id == id);
        if (Item == null)
        {
            return NotFound();
        }
        return Page();
    }
}
