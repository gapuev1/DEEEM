using MovieSearchApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchApp.Services
{
    public class TmdbApiService : ITmdbApi
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "ВАШ_API_КЛЮЧ_TMDB"; // Замените на свой ключ
        private const string BaseUrl = "https://api.themoviedb.org/3/";

        public TmdbApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            var response = await _httpClient.GetAsync($"search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}&language=ru-RU");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiSearchResponse>(json);
            return result?.Results ?? new List<Movie>();
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"movie/{movieId}?api_key={ApiKey}&append_to_response=credits,videos&language=ru-RU");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var detail = JsonConvert.DeserializeObject<MovieDetailResponse>(json);

            // Заполняем дополнительные поля
            var movieDetails = new MovieDetails
            {
                Id = detail.Id,
                Title = detail.Title,
                ReleaseDate = detail.ReleaseDate,
                VoteAverage = detail.VoteAverage,
                PosterPath = detail.PosterPath,
                Overview = detail.Overview,
                Genres = detail.Genres,
                Credits = detail.Credits?.Cast ?? new List<Cast>(),
                TrailerUrl = GetTrailerUrl(detail.Videos?.Results)
            };
            return movieDetails;
        }

        private string GetTrailerUrl(List<Video> videos)
        {
            var trailer = videos?.Find(v => v.Site == "YouTube" && v.Type == "Trailer");
            return trailer != null ? $"https://www.youtube.com/watch?v={trailer.Key}" : null;
        }
    }
}
