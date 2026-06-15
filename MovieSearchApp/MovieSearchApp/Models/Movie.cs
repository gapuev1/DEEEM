namespace MovieSearchApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
        public string PosterPath { get; set; }
        public string PosterUrl => string.IsNullOrEmpty(PosterPath)
            ? "https://via.placeholder.com/200x300?text=No+Poster"
            : $"https://image.tmdb.org/t/p/w200{PosterPath}";
        public int? Year => !string.IsNullOrEmpty(ReleaseDate) && ReleaseDate.Length >= 4
            ? int.Parse(ReleaseDate.Substring(0, 4))
            : null;
    }
}