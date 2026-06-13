using System;
using System.Windows;
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
            UsersList.MouseDoubleClick += UsersList_MouseDoubleClick;
        }

        private async void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "🔄 Загрузка...";
            LoadBtn.IsEnabled = false;
            UsersList.ItemsSource = null;

            try
            {
                var users = await _service.GetUsersAsync();
                StatusText.Text = $"✅ Пользователей: {users.Count}";
                UsersList.ItemsSource = users;
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

        private async void UsersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (UsersList.SelectedItem is User user)
            {
                try
                {
                    var todos = await _service.GetTodosByUserAsync(user.Id);
                    string message = $"Задачи пользователя {user.Name}:\n\n";
                    foreach (var todo in todos)
                    {
                        message += $"{(todo.Completed ? "✅" : "⏳")} {todo.Title}\n";
                    }
                    MessageBox.Show(message, "Задачи", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки задач: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}