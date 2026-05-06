using System.Text;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameExporter
    {
        public byte[] ExportToCsv(List<Game> games)
        {
            var lines = new List<string> { "Id,Title,Genre,Year,Score" };
            lines.AddRange(games.Select(g => $"{g.Id},{g.Title},{g.Genre},{g.Year},{g.Score}"));
            var csv = string.Join(Environment.NewLine, lines);
            return Encoding.UTF8.GetBytes(csv);
        }
    }
}
