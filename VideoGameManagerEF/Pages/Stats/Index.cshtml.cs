using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages.Stats
{
    public class IndexModel : PageModel
    {
        private readonly GameStoreContext _context;

        public IndexModel(GameStoreContext context)
        {
            _context = context;
        }

        // 6.1
        [BindProperty(SupportsGet = true)]
        public string? SelectedGenre { get; set; }
        public List<Game> FilteredGames { get; set; } = new();
        public List<string> Genres { get; set; } = new();

        // 6.2
        public List<Game> Top5Games { get; set; } = new();

        // 6.3
        public List<DecadeGroup> GamesByDecade { get; set; } = new();

        // 7.1
        public List<DeveloperAverage> AvgScoreByDeveloper { get; set; } = new();

        // 7.2
        [BindProperty(SupportsGet = true)]
        public string? TitleFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? GenreFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? MinYear { get; set; }
        public List<Game> SearchResults { get; set; } = new();

        // 7.3
        [BindProperty(SupportsGet = true)]
        public int? GameThreshold { get; set; }
        public List<Developer> ProductiveDevelopers { get; set; } = new();

        public async Task OnGetAsync()
        {
            // 6.1: Distinct genres for dropdown
            Genres = await _context.Games
                .Select(g => g.Genre)
                .Distinct()
                .ToListAsync();

            if (!string.IsNullOrEmpty(SelectedGenre))
            {
                FilteredGames = await _context.Games
                    .Where(g => g.Genre == SelectedGenre)
                    .OrderByDescending(g => g.Score)
                    .Include(g => g.Developer)
                    .ToListAsync();
            }

            // 6.2: Top 5
            Top5Games = await _context.Games
                .Include(g => g.Developer)
                .OrderByDescending(g => g.Score)
                .Take(5)
                .ToListAsync();

            // 6.3: By decade
            GamesByDecade = await _context.Games
                .GroupBy(g => (g.Year / 10) * 10)
                .Select(grp => new DecadeGroup
                {
                    Decade = grp.Key,
                    Count = grp.Count()
                })
                .OrderBy(x => x.Decade)
                .ToListAsync();

            // 7.1: Average score per developer.
            // We filter developers that have at least 1 game, then project.
            AvgScoreByDeveloper = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Any())
                .Select(d => new DeveloperAverage
                {
                    Name = d.Name,
                    GameCount = d.Games.Count,
                    AvgScore = d.Games.Average(g => g.Score)
                })
                .OrderByDescending(x => x.AvgScore)
                .ToListAsync();

            // 7.2: Combined search.
            // We start with AsQueryable() so we can add Where clauses conditionally.
            if (!string.IsNullOrEmpty(TitleFilter) || !string.IsNullOrEmpty(GenreFilter) || MinYear.HasValue)
            {
                var query = _context.Games.Include(g => g.Developer).AsQueryable();

                if (!string.IsNullOrEmpty(TitleFilter))
                    query = query.Where(g => g.Title.Contains(TitleFilter));

                if (!string.IsNullOrEmpty(GenreFilter))
                    query = query.Where(g => g.Genre == GenreFilter);

                if (MinYear.HasValue)
                    query = query.Where(g => g.Year >= MinYear.Value);

                SearchResults = await query.OrderBy(g => g.Title).ToListAsync();
            }

            // 7.3: Developers with more than N games.
            if (GameThreshold.HasValue && GameThreshold.Value > 0)
            {
                ProductiveDevelopers = await _context.Developers
                    .Include(d => d.Games)
                    .Where(d => d.Games.Count > GameThreshold.Value)
                    .OrderByDescending(d => d.Games.Count)
                    .ToListAsync();
            }
        }
    }
}