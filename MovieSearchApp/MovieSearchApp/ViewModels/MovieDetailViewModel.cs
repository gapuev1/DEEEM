using MovieSearchApp.Models;
using MovieSearchApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieSearchApp.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private readonly ITmdbApi _api = new TmdbApiService();
        private readonly int _movieId;
        private bool _isLoading;
        private MovieDetails _details;

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public MovieDetails Details
        {
            get => _details;
            set { _details = value; OnPropertyChanged(); OnPropertyChanged(nameof(Title)); OnPropertyChanged(nameof(PosterUrl)); /* и т.д. */ }
        }

        public string Title => Details?.Title;
        public string PosterUrl => Details?.PosterUrl;
        public string ReleaseDate => Details?.ReleaseDate;
        public double VoteAverage => Details?.VoteAverage ?? 0;
        public string Overview => Details?.Overview;
        public string GenresString => Details?.Genres != null ? string.Join(", ", Details.Genres.Select(g => g.Name)) : "";
        public List<string> CastList => Details?.Credits?.Take(5).Select(c => $"{c.Name} — {c.Character}").ToList() ?? new List<string>();
        public bool HasTrailer => !string.IsNullOrEmpty(Details?.TrailerUrl);
        public ICommand OpenTrailerCommand => new RelayCommand(_ => OpenTrailer());

        public MovieDetailViewModel(int movieId)
        {
            _movieId = movieId;
        }

        public async Task LoadDetailsAsync()
        {
            IsLoading = true;
            try
            {
                Details = await _api.GetMovieDetailsAsync(_movieId);
                OnPropertyChanged(string.Empty); // обновить все привязки
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OpenTrailer()
        {
            if (!string.IsNullOrEmpty(Details?.TrailerUrl))
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Details.TrailerUrl) { UseShellExecute = true });
        }
    }
}
