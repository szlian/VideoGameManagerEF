namespace VideoGameManagerEF.Models
{
    // For decade grouping
    public class DecadeGroup
    {
        public int Decade { get; set; }
        public int Count { get; set; }
    }

    // For average score per developer (Tasca 7.1)
    public class DeveloperAverage
    {
        public string Name { get; set; } = string.Empty;
        public int GameCount { get; set; }
        public double AvgScore { get; set; }
    }
}