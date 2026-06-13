using System;
using System.Windows;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly FakeStoreService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new FakeStoreService();
            LoadBtn.Click += LoadBtn_Click;
        }

        private async void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "🔄 Загрузка...";
            LoadBtn.IsEnabled = false;
            ResultsList.ItemsSource = null;

            try
            {
                var products = await _service.GetProductsAsync();
                if (products.Count == 0)
                    StatusText.Text = "😕 Товары не найдены";
                else
                {
                    StatusText.Text = $"✅ Товаров: {products.Count}";
                    ResultsList.ItemsSource = products;
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"❌ Ошибка: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadBtn.IsEnabled = true;
            }
        }
    }
}