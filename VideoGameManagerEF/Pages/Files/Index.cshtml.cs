using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly GameRepository _repo;
        private readonly GameExporter _exporter;
        private readonly GamesRanking _ranking;

        public List<string> LogEntries { get; set; } = new();

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
            _repo = new GameRepository();
            _exporter = new GameExporter();
            _ranking = new GamesRanking();
        }

        public void OnGet()
        {
            var logPath = Path.Combine("wwwroot", "data", "activity_log.txt");
            LogEntries = System.IO.File.Exists(logPath)
                ? System.IO.File.ReadAllLines(logPath).ToList()
                : new List<string>();
        }

        public IActionResult OnPostExportJson()
        {
            EnsureDataDirectoryExists();
            _repo.SaveAll(_gameService.GetAll());
            return RedirectToPage();
        }

        public IActionResult OnPostImportJson()
        {
            var games = _repo.LoadAll();
            _gameService.LoadGames(games); // Ver abajo
            return RedirectToPage();
        }

        public IActionResult OnPostExportCsv()
        {
            var bytes = _exporter.ExportToCsv(_gameService.GetAll());
            return base.File(bytes, "text/csv", "games.csv");
        }

        public IActionResult OnPostExportXml()
        {
            EnsureDataDirectoryExists();
            var path = Path.Combine("wwwroot", "data", "games_ranking.xml");
            _ranking.GenerateRanking(_gameService.GetAll(), path);
            return RedirectToPage();
        }

        private void EnsureDataDirectoryExists()
        {
            var path = Path.Combine("wwwroot", "data");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}