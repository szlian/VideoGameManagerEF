using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GamesRanking
    {
        public void GenerateRanking(List<Game> games, string path)
        {
            var ordered = games.OrderByDescending(g => g.Score).ToList();

            var doc = new XDocument(
                new XElement("AppConfig",
                    new XElement("AppTitle", "VideoGame Ranking"),
                    new XElement("Games",
                        ordered.Select(g => new XElement("Game",
                            new XElement("id", g.Id),
                            new XElement("score", g.Score),
                            new XElement("title", g.Title),
                            new XElement("genre", g.Genre),
                            new XElement("year", g.Year),
                            new XElement("description", g.Description)
                        ))
                    )
                )
            );
            doc.Save(path);
        }
    }
}
