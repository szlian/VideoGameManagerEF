using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameService _service;

        [BindProperty]
        public Game Game { get; set; } = new();

        public EditModel(GameService service) => _service = service;

        public IActionResult OnGet(int id)
        {
            var game = _service.GetById(id);
            if (game == null) return RedirectToPage("Index");

            Game = game;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _service.Update(Game);
            return RedirectToPage("Index");
        }
    }
}
