using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
{
    private readonly List<Game> _games = new();
    private int _nextId = 1;

    public List<Game> GetAll() => _games;
    
    public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);
    


    public void Add(Game game)
    {
        game.Id = _nextId++;
        _games.Add(game);
    }
    
    public void Update(Game game)
    {
        var index = _games.FindIndex(g => g.Id == game.Id);
        if (index != -1) _games[index] = game;
    }

        public void LoadGames(List<Game> games)
        {
            _games.Clear();
            _games.AddRange(games);
            _nextId = games.Count > 0 ? games.Max(g => g.Id) + 1 : 1;
        }

        public void Delete(int id) => _games.RemoveAll(g => g.Id == id);

        private void LogActivity(string action, string title)
        {
            var path = Path.Combine("wwwroot", "data", "activity_log.txt");
            var line = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] [{action}] [{title}]{Environment.NewLine}";
            File.AppendAllText(path, line);
        }
    }
}
