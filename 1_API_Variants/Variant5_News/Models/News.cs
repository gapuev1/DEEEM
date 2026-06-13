using Newtonsoft.Json;

namespace YourApp.Models
{
    public class News
    {
        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("url")]
        public string Url { get; set; } = "";

        [JsonProperty("urlToImage")]
        public string ImageUrl { get; set; } = "";

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; } = new();

        public string DisplayText => $"{Source.Name} | {PublishedAt:dd.MM.yyyy HH:mm}";
    }

    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }

    public class NewsResponse
    {
        [JsonProperty("articles")]
        public List<News> Articles { get; set; } = new();

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
    }
}