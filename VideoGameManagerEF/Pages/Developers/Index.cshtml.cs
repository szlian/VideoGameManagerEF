using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages.Developers
{
    public class IndexModel : PageModel
    {
        private readonly GameStoreContext _context;

        public IndexModel(GameStoreContext context)
        {
            _context = context;
        }

        public List<Developer> Developers { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Include loads the Games collection so we can count them
            Developers = await _context.Developers
                .Include(d => d.Games)
                .ToListAsync();
        }
    }
}