using Newtonsoft.Json;

namespace YourApp.Models
{
    public class Album
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        public string DisplayText => $"Пользователь {UserId}";
    }

    public class Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("albumId")]
        public int AlbumId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("url")]
        public string Url { get; set; } = "";

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; } = "";
    }
}