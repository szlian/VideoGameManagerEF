using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManagerEF.Models;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly GameService _service;
        public Game Game { get; set; } = new();

        public DetailsModel(GameService service) => _service = service;

        public IActionResult OnGet(int id)
        {
            var game = _service.GetById(id);
            if (game == null) return RedirectToPage("Index");

            Game = game;
            return Page();
        }
    }
}
