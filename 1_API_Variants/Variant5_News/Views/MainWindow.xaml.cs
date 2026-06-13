using System;
using System.Windows;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly NewsService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new NewsService();
            SearchBtn.Click += SearchBtn_Click;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                StatusText.Text = "⚠️ Введите тему поиска";
                return;
            }

            StatusText.Text = "🔍 Поиск новостей...";
            SearchBtn.IsEnabled = false;
            ResultsList.ItemsSource = null;

            try
            {
                var news = await _service.GetNewsAsync(query);
                if (news.Count == 0)
                    StatusText.Text = $"😕 По запросу \"{query}\" новости не найдены";
                else
                {
                    StatusText.Text = $"✅ Найдено новостей: {news.Count}";
                    ResultsList.ItemsSource = news;
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