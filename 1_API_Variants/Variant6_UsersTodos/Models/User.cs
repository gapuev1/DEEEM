using Newtonsoft.Json;

namespace YourApp.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("username")]
        public string Username { get; set; } = "";

        [JsonProperty("email")]
        public string Email { get; set; } = "";

        [JsonProperty("phone")]
        public string Phone { get; set; } = "";

        [JsonProperty("website")]
        public string Website { get; set; } = "";

        public string DisplayText => $"{Username} | {Email}";
    }

    public class Todo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}