using System;
using System.Windows;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly OpenLibraryService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new OpenLibraryService();
            SearchBtn.Click += SearchBtn_Click;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                StatusText.Text = "⚠️ Введите название книги";
                return;
            }

            StatusText.Text = "🔍 Поиск...";
            SearchBtn.IsEnabled = false;
            ResultsList.ItemsSource = null;

            try
            {
                var books = await _service.SearchBooksAsync(query);
                if (books.Count == 0)
                    StatusText.Text = $"😕 По запросу \"{query}\" книги не найдены";
                else
                {
                    StatusText.Text = $"✅ Найдено книг: {books.Count}";
                    ResultsList.ItemsSource = books;
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