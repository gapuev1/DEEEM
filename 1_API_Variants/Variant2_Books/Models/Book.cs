using Newtonsoft.Json;

namespace YourApp.Models
{
    public class Book
    {
        [JsonProperty("key")]
        public string Key { get; set; } = "";

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("author_name")]
        public List<string> AuthorNames { get; set; } = new();

        [JsonProperty("first_publish_year")]
        public int? Year { get; set; }

        [JsonProperty("cover_i")]
        public int? CoverId { get; set; }

        [JsonProperty("isbn")]
        public List<string> Isbn { get; set; } = new();

        public string Author => AuthorNames?.FirstOrDefault() ?? "Автор неизвестен";

        public string CoverUrl => CoverId.HasValue
            ? $"https://covers.openlibrary.org/b/id/{CoverId}-M.jpg"
            : "https://via.placeholder.com/200x300?text=No+Cover";

        public string DisplayText => $"{Author} ({Year})";
    }

    public class BookSearchResponse
    {
        [JsonProperty("docs")]
        public List<Book> Docs { get; set; } = new();

        [JsonProperty("num_found")]
        public int NumFound { get; set; }
    }
}