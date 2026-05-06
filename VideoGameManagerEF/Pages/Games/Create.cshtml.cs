using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Shared.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService _service;

        [BindProperty]
        public Game Game { get; set; } = new();

        public CreateModel(GameService service) => _service = service;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _service.Add(Game);
            return RedirectToPage("Index");
        }
    }
}
