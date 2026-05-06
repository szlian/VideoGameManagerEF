using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Shared.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _service;

        [BindProperty]
        public Game Game { get; set; } = new();

        public DeleteModel(GameService service) => _service = service;

        public IActionResult OnGet(int id)
        {
            var game = _service.GetById(id);
            if (game == null) return RedirectToPage("Index");
            Game = game;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _service.Delete(id);
            return RedirectToPage("Index");
        }
    }
}
