using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourApp.Models;

namespace YourApp.Services
{
    // ⚠️ НЕ ТРЕБУЕТ API КЛЮЧА!
    public class FakeStoreService
    {
        private readonly HttpClient _httpClient;

        public FakeStoreService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
                return JsonConvert.DeserializeObject<List<Product>>(response) ?? new List<Product>();
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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://fakestoreapi.com/products/{id}");
                return JsonConvert.DeserializeObject<Product>(response) ?? new Product();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
    }
}