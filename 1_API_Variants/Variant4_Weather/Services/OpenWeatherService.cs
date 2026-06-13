using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    public class OpenWeatherService
    {
        // 🔑 ПОЛУЧИТЕ КЛЮЧ НА https://home.openweathermap.org/users/sign_up
        private const string ApiKey = "ВАШ_API_КЛЮЧ";
        private readonly HttpClient _httpClient;

        public OpenWeatherService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<Weather> GetWeatherAsync(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(city)}&appid={ApiKey}&units=metric&lang=ru";
                var response = await _httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<WeatherResponse>(response);

                if (data == null) return null;

                return new Weather
                {
                    City = data.Name,
                    Temperature = data.Main.Temp,
                    FeelsLike = data.Main.FeelsLike,
                    Description = data.Weather.FirstOrDefault()?.Description ?? "",
                    Icon = data.Weather.FirstOrDefault()?.Icon ?? "",
                    Humidity = data.Main.Humidity,
                    WindSpeed = data.Wind.Speed
                };
            }
            catch (HttpRequestException)
            {
                throw new Exception("Ошибка сети. Проверьте интернет-соединение.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
    }
}