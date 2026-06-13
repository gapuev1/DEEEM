using Newtonsoft.Json;

namespace YourApp.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("category")]
        public string Category { get; set; } = "";

        [JsonProperty("image")]
        public string ImageUrl { get; set; } = "";

        [JsonProperty("rating")]
        public Rating Rating { get; set; } = new();

        public string DisplayText => $"{Price:C} | {Category} | ★ {Rating.Rate}";
    }

    public class Rating
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}