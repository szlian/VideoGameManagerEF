using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameStoreContext _context;

        public CreateModel(GameStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; } = new();

        // Dropdown list for developers
        public SelectList DeveloperList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Load all developers for the dropdown
            var developers = await _context.Developers.ToListAsync();
            DeveloperList = new SelectList(developers, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown if validation fails
                var developers = await _context.Developers.ToListAsync();
                DeveloperList = new SelectList(developers, "Id", "Name");
                return Page();
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}