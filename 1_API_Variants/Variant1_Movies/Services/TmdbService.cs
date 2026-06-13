using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    public class TmdbService
    {
        // 🔑 ПОЛУЧИТЕ КЛЮЧ НА https://www.themoviedb.org/signup
        private const string ApiKey = "ВАШ_API_КЛЮЧ";
        private readonly HttpClient _httpClient;

        public TmdbService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=ru&query={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<MovieSearchResponse>(response);
                return result?.Results ?? new List<Movie>();
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