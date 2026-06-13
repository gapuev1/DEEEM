using System;
using System.Windows;
using System.Windows.Controls;
using YourApp.Models;
using YourApp.Services;

namespace YourApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly JsonPlaceholderService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new JsonPlaceholderService();
            LoadBtn.Click += LoadBtn_Click;
            AlbumsList.MouseDoubleClick += AlbumsList_MouseDoubleClick;
        }

        private async void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "🔄 Загрузка...";
            LoadBtn.IsEnabled = false;
            AlbumsList.ItemsSource = null;

            try
            {
                var albums = await _service.GetAlbumsAsync();
                StatusText.Text = $"✅ Альбомов: {albums.Count}";
                AlbumsList.ItemsSource = albums;
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

        private async void AlbumsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AlbumsList.SelectedItem is Album album)
            {
                var photosWindow = new Window
                {
                    Title = $"Фото альбома: {album.Title}",
                    Height = 500,
                    Width = 600,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                var listBox = new ListBox();
                listBox.DisplayMemberPath = "Title";

                try
                {
                    var photos = await _service.GetPhotosByAlbumAsync(album.Id);
                    listBox.ItemsSource = photos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                photosWindow.Content = listBox;
                photosWindow.Owner = this;
                photosWindow.ShowDialog();
            }
        }
    }
}