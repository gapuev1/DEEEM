using System;
using System.Windows;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly OpenWeatherService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new OpenWeatherService();
            SearchBtn.Click += SearchBtn_Click;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string city = CityBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(city))
            {
                StatusText.Text = "⚠️ Введите название города";
                return;
            }

            StatusText.Text = "🔍 Загрузка...";
            SearchBtn.IsEnabled = false;
            WeatherCard.Visibility = Visibility.Collapsed;

            try
            {
                var weather = await _service.GetWeatherAsync(city);
                if (weather == null)
                {
                    StatusText.Text = $"😕 Город \"{city}\" не найден";
                }
                else
                {
                    CityText.Text = weather.City;
                    TempText.Text = $"{weather.Temperature:F1}°C";
                    DescText.Text = weather.Description;
                    DetailsText.Text = weather.DisplayText;
                    WeatherIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(weather.IconUrl));

                    WeatherCard.Visibility = Visibility.Visible;
                    StatusText.Text = $"✅ Погода в {weather.City}";
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