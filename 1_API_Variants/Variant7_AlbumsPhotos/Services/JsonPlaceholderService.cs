using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    // ⚠️ НЕ ТРЕБУЕТ API КЛЮЧА!
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        public JsonPlaceholderService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<List<Album>> GetAlbumsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/albums");
                return JsonConvert.DeserializeObject<List<Album>>(response) ?? new List<Album>();
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

        public async Task<List<Photo>> GetPhotosByAlbumAsync(int albumId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/albums/{albumId}/photos");
                return JsonConvert.DeserializeObject<List<Photo>>(response) ?? new List<Photo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
    }
}