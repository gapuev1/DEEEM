using MovieSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearchApp.Services
{
    public interface ITmdbApi
    {
        Task<List<Movie>> SearchMoviesAsync(string query);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
    }
}
