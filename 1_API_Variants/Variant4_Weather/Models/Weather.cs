using Newtonsoft.Json;

namespace YourApp.Models
{
    public class Weather
    {
        public string City { get; set; } = "";
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }

        public string IconUrl => $"https://openweathermap.org/img/w/{Icon}.png";
        public string DisplayText => $"{Temperature:F1}°C (ощущается {FeelsLike:F1}°C) | 💧{Humidity}% | 💨{WindSpeed:F1}м/с";
    }

    public class WeatherResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("main")]
        public MainData Main { get; set; } = new();

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; } = new();

        [JsonProperty("wind")]
        public WindData Wind { get; set; } = new();
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("icon")]
        public string Icon { get; set; } = "";
    }

    public class WindData
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}