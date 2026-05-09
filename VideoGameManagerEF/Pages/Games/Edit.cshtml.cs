using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameStoreContext _context;

        public EditModel(GameStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public SelectList DeveloperList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games.FindAsync(id);

            if (Game == null)
            {
                return NotFound();
            }

            var developers = await _context.Developers.ToListAsync();
            DeveloperList = new SelectList(developers, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var developers = await _context.Developers.ToListAsync();
                DeveloperList = new SelectList(developers, "Id", "Name");
                return Page();
            }

            _context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}