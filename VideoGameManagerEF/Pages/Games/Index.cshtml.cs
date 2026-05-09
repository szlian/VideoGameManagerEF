using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManagerEF.Models;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _service;
        public List<Game> Games { get; set; } = new();

        public IndexModel(GameService service) => _service = service;

        public void OnGet() => Games = _service.GetAll();
    }
}
