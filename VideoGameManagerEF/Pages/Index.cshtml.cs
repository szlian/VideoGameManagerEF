using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Models;

namespace VideoGameManagerEF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GameStoreContext _context;

        public IndexModel(GameStoreContext context)
        {
            _context = context;
        }

        public List<Game> Games { get; set; } = new();
        public List<Developer> Developers { get; set; } = new();

        public async Task OnGetAsync()
        {
            // If the database is empty, seed test data automatically
            if (!await _context.Developers.AnyAsync())
            {
                var nintendo = new Developer
                {
                    Name = "Nintendo",
                    Country = "Japan",
                    FoundedYear = 1889
                };

                var cdpr = new Developer
                {
                    Name = "CD Projekt Red",
                    Country = "Poland",
                    FoundedYear = 1994
                };

                // Save developers first
                _context.Developers.AddRange(nintendo, cdpr);
                await _context.SaveChangesAsync();

                // Now create games linked directly to the developer objects.
                // EF Core will automatically set the correct DeveloperId.
                _context.Games.AddRange(
                    new Game
                    {
                        Title = "The Legend of Zelda: TotK",
                        Genre = "Adventure",
                        Year = 2023,
                        Score = 9.8,
                        Developer = nintendo  // <-- Changed from DeveloperId
                    },
                    new Game
                    {
                        Title = "Mario Kart 8",
                        Genre = "Racing",
                        Year = 2014,
                        Score = 8.7,
                        Developer = nintendo  // <-- Changed from DeveloperId
                    },
                    new Game { Title = "Nine Sols", Genre = "2d metroidvania", Year = 2015, Score = 9.5, }, new Game { Title = "Ark ASE", Genre = "Open World survial", Year = 2015, Score = 9.5, },
                    new Game
                    {
                        Title = "The Witcher 3",
                        Genre = "RPG",
                        Year = 2015,
                        Score = 9.5,
                        Developer = cdpr  // <-- Changed from DeveloperId
                    }
                );

                await _context.SaveChangesAsync();
            }

            Games = await _context.Games
                .Include(g => g.Developer)
                .ToListAsync();

            Developers = await _context.Developers
                .Include(d => d.Games)
                .ToListAsync();
        }
    }
}