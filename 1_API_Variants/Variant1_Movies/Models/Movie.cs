using Newtonsoft.Json;

namespace YourApp.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("overview")]
        public string Overview { get; set; } = "";

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = "";

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";

        [JsonProperty("vote_average")]
        public double Rating { get; set; }

        public string Year => ReleaseDate?.Length >= 4 ? ReleaseDate.Substring(0, 4) : "N/A";

        public string PosterUrl => string.IsNullOrEmpty(PosterPath)
            ? "https://via.placeholder.com/200x300?text=No+Poster"
            : $"https://image.tmdb.org/t/p/w200{PosterPath}";

        public string DisplayText => $"{Year} | ★ {Rating:F1}";
    }

    public class MovieSearchResponse
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; } = new();
    }
}