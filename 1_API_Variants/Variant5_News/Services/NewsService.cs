using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    public class NewsService
    {
        // 🔑 ПОЛУЧИТЕ КЛЮЧ НА https://newsapi.org/register
        private const string ApiKey = "ВАШ_API_КЛЮЧ";
        private readonly HttpClient _httpClient;

        public NewsService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<List<News>> GetNewsAsync(string query)
        {
            try
            {
                string url = $"https://newsapi.org/v2/everything?q={Uri.EscapeDataString(query)}&apiKey={ApiKey}&pageSize=20&language=ru";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<NewsResponse>(response);
                return result?.Articles ?? new List<News>();
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