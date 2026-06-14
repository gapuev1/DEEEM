using System;
using System.Linq;
using System.Windows;
using ToyStore.Models;

namespace ToyStore.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginBtn.Click += LoginBtn_Click;
            GuestBtn.Click += GuestBtn_Click;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ErrorText.Text = "Введите логин и пароль!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                using (var db = new StoreDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                    if (user != null && user.IsActive)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        ErrorText.Text = "Неверный логин или пароль!";
                        ErrorText.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GuestBtn_Click(object sender, RoutedEventArgs e)
        {
            User guestUser = new User
            {
                Id = 0,
                Login = "guest",
                FullName = "Гость",
                Role = "Guest"
            };

            MainWindow mainWindow = new MainWindow(guestUser);
            mainWindow.Show();
            this.Close();
        }
    }
}