using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    // ⚠️ НЕ ТРЕБУЕТ API КЛЮЧА!
    public class OpenLibraryService
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<List<Book>> SearchBooksAsync(string query)
        {
            try
            {
                string url = $"https://openlibrary.org/search.json?q={Uri.EscapeDataString(query)}&limit=20";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<BookSearchResponse>(response);
                return result?.Docs ?? new List<Book>();
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