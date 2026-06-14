using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using BikeStore.Models;

namespace BikeStore.Views
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;
        public bool IsAdmin => _currentUser.Role == "Admin";

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

            // Добавляем конвертер
            var converter = new BooleanToVisibilityConverter();
            Resources.Add("BooleanToVisibility", converter);

            UserInfoText.Text = $"{user.FullName} ({user.Role})";

            if (IsAdmin)
            {
                AddProductBtn.Visibility = Visibility.Visible;
                OrdersBtn.Visibility = Visibility.Visible;
                FiltersPanel.Visibility = Visibility.Visible;
            }
            else if (user.Role == "Manager")
            {
                OrdersBtn.Visibility = Visibility.Visible;
                FiltersPanel.Visibility = Visibility.Visible;
            }

            SearchBtn.Click += SearchBtn_Click;
            AddProductBtn.Click += AddProductBtn_Click;
            OrdersBtn.Click += OrdersBtn_Click;
            LogoutBtn.Click += LogoutBtn_Click;
            Loaded += MainWindow_Loaded;

            CategoryFilter.SelectionChanged += (s, e) => LoadProducts();
            ManufacturerFilter.SelectionChanged += (s, e) => LoadProducts();
            DiscountFilter.SelectionChanged += (s, e) => LoadProducts();
            SortFilter.SelectionChanged += (s, e) => LoadProducts();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFilters();
            LoadProducts();
        }

        private void LoadFilters()
        {
            using (var db = new StoreDbContext())
            {
                var categories = db.Categories.ToList();
                categories.Insert(0, new Category { Id = 0, Name = "Все категории" });
                CategoryFilter.ItemsSource = categories;
                CategoryFilter.SelectedIndex = 0;

                var manufacturers = db.Manufacturers.ToList();
                manufacturers.Insert(0, new Manufacturer { Id = 0, Name = "Все производители" });
                ManufacturerFilter.ItemsSource = manufacturers;
                ManufacturerFilter.SelectedIndex = 0;
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (var db = new StoreDbContext())
                {
                    var query = db.Products
                        .Include(p => p.Category)
                        .Include(p => p.Manufacturer)
                        .Include(p => p.Supplier)
                        .AsQueryable();

                    string search = SearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(search))
                    {
                        query = query.Where(p => p.Name.Contains(search) || p.Article.Contains(search));
                    }

                    if (CategoryFilter.SelectedItem is Category cat && cat.Id > 0)
                    {
                        query = query.Where(p => p.CategoryId == cat.Id);
                    }

                    if (ManufacturerFilter.SelectedItem is Manufacturer man && man.Id > 0)
                    {
                        query = query.Where(p => p.ManufacturerId == man.Id);
                    }

                    if (DiscountFilter.SelectedItem is System.Windows.Controls.ComboBoxItem disc)
                    {
                        string d = disc.Content.ToString();
                        if (d == "0-10.99%") query = query.Where(p => p.Discount >= 0 && p.Discount <= 10.99m);
                        else if (d == "11-14.99%") query = query.Where(p => p.Discount >= 11 && p.Discount <= 14.99m);
                        else if (d == "15% и более") query = query.Where(p => p.Discount >= 15);
                    }

                    if (SortFilter.SelectedItem is System.Windows.Controls.ComboBoxItem sort)
                    {
                        string s = sort.Content.ToString();
                        if (s == "Цена ↑") query = query.OrderBy(p => p.Price);
                        else if (s == "Цена ↓") query = query.OrderByDescending(p => p.Price);
                        else if (s == "Кол-во ↑") query = query.OrderBy(p => p.Quantity);
                        else if (s == "Кол-во ↓") query = query.OrderByDescending(p => p.Quantity);
                    }

                    var products = query.ToList();
                    ProductsGrid.ItemsSource = products;

                    int totalQuantity = products.Sum(p => p.Quantity);
                    decimal totalValue = products.Sum(p => p.Price * p.Quantity);
                    StatusText.Text = $"Товаров: {products.Count}";
                    StatsText.Text = $"Всего на складе: {totalQuantity} шт. на сумму {totalValue:C}";
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Ошибка: {ex.Message}";
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e) => LoadProducts();

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialogWindow();
            if (dialog.ShowDialog() == true)
            {
                LoadProducts();
                StatusText.Text = "✅ Товар добавлен";
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as System.Windows.Controls.Button)?.CommandParameter is Product product)
            {
                var dialog = new ProductDialogWindow(product);
                if (dialog.ShowDialog() == true)
                {
                    LoadProducts();
                    StatusText.Text = "✅ Товар обновлен";
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as System.Windows.Controls.Button)?.CommandParameter is Product product)
            {
                var result = MessageBox.Show($"Удалить товар \"{product.Name}\"?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new StoreDbContext())
                    {
                        // Удаляем файл изображения
                        if (!string.IsNullOrEmpty(product.ImagePath))
                        {
                            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", product.ImagePath);
                            if (System.IO.File.Exists(imagePath))
                                System.IO.File.Delete(imagePath);
                        }
                        db.Products.Remove(product);
                        db.SaveChanges();
                    }
                    LoadProducts();
                    StatusText.Text = "🗑️ Товар удален";
                }
            }
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            var ordersWindow = new OrdersWindow(_currentUser);
            ordersWindow.ShowDialog();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }

    // Конвертер для видимости
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is bool boolValue && boolValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}