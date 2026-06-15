using MovieSearchApp.Helpers;
using MovieSearchApp.Models;
using MovieSearchApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieSearchApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ITmdbApi _api = new TmdbApiService();
        private string _searchText;
        private bool _isLoading;
        private List<Movie> _movies;

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); ((RelayCommand)SearchCommand).RaiseCanExecuteChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public List<Movie> Movies
        {
            get => _movies;
            set { _movies = value; OnPropertyChanged(); }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand<Movie> ShowDetailsCommand { get; }

        public MainWindowViewModel()
        {
            SearchCommand = new RelayCommand(async _ => await SearchAsync(), _ => !string.IsNullOrWhiteSpace(SearchText));
            ShowDetailsCommand = new RelayCommand<Movie>(movie => ShowMovieDetails(movie));
        }

        private async Task SearchAsync()
        {
            string cacheKey = $"search_{SearchText.ToLower()}";
            if (CacheHelper.Contains(cacheKey))
            {
                Movies = CacheHelper.Get<List<Movie>>(cacheKey);
                return;
            }

            IsLoading = true;
            try
            {
                var results = await _api.SearchMoviesAsync(SearchText);
                CacheHelper.Set(cacheKey, results);
                Movies = results;
                if (results.Count == 0)
                    MessageBox.Show("Фильмы не найдены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка сети: {ex.Message}\nПроверьте подключение к интернету.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ShowMovieDetails(Movie movie)
        {
            var detailWindow = new Views.MovieDetailWindow(movie.Id);
            detailWindow.Owner = Application.Current.MainWindow;
            detailWindow.ShowDialog();
        }
    }
}
