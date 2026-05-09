using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages.Developers
{
    public class DetailsModel : PageModel
    {
        private readonly GameStoreContext _context;

        // Inject the database context so we can query the developer and their games
        public DetailsModel(GameStoreContext context)
        {
            _context = context;
        }

        public Developer Developer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Include loads the Games collection automatically.
            // Without Include, Developer.Games would be empty.
            Developer = await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (Developer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}