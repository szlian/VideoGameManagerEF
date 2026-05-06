using System.Text.Json;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameRepository
    {
        private readonly string _path = Path.Combine("wwwroot", "data", "games.json");

        public List<Game> LoadAll()
        {
            if (!File.Exists(_path)) return new List<Game>();

            try
            {
                var json = File.ReadAllText(_path);
                return JsonSerializer.Deserialize<List<Game>>(json) ?? new List<Game>();
            }
            catch
            {
                return new List<Game>();
            }
        }

        public void SaveAll(IEnumerable<Game> games)
        {
            var directory = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }
    }
}