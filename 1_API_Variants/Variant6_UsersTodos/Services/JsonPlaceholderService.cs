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

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/users");
                return JsonConvert.DeserializeObject<List<User>>(response) ?? new List<User>();
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

        public async Task<List<Todo>> GetTodosByUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/users/{userId}/todos");
                return JsonConvert.DeserializeObject<List<Todo>>(response) ?? new List<Todo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
    }
}