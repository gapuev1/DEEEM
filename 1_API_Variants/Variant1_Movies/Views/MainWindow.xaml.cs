using System;
using System.Windows;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly TmdbService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new TmdbService();
            SearchBtn.Click += SearchBtn_Click;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                StatusText.Text = "⚠️ Введите название фильма";
                return;
            }

            StatusText.Text = "🔍 Поиск...";
            SearchBtn.IsEnabled = false;
            ResultsList.ItemsSource = null;

            try
            {
                var movies = await _service.SearchMoviesAsync(query);
                if (movies.Count == 0)
                    StatusText.Text = $"😕 По запросу \"{query}\" фильмы не найдены";
                else
                {
                    StatusText.Text = $"✅ Найдено фильмов: {movies.Count}";
                    ResultsList.ItemsSource = movies;
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"❌ Ошибка: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SearchBtn.IsEnabled = true;
            }
        }
    }
}