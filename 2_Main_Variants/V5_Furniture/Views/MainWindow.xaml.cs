using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using FurnitureApp.Models;

namespace FurnitureApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;
        public bool IsAdmin => _currentUser.Role == "Admin";

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            this.DataContext = this;
            
            StoreTitle.Text = "🛋️ Магазин мебели";
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
            
            CategoryFilter.SelectionChanged += Filter_Changed;
            ManufacturerFilter.SelectionChanged += Filter_Changed;
            SortFilter.SelectionChanged += Filter_Changed;
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
                        .AsQueryable();

                    string searchText = SearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query = query.Where(p => p.Name.Contains(searchText));
                    }

                    if (CategoryFilter.SelectedItem is Category cat && cat.Id > 0)
                    {
                        query = query.Where(p => p.CategoryId == cat.Id);
                    }

                    if (ManufacturerFilter.SelectedItem is Manufacturer man && man.Id > 0)
                    {
                        query = query.Where(p => p.ManufacturerId == man.Id);
                    }

                    if (SortFilter.SelectedItem is System.Windows.Controls.ComboBoxItem sortItem)
                    {
                        string sort = sortItem.Content.ToString();
                        switch (sort)
                        {
                            case "Цена ↑": query = query.OrderBy(p => p.Price); break;
                            case "Цена ↓": query = query.OrderByDescending(p => p.Price); break;
                            case "Кол-во ↑": query = query.OrderBy(p => p.Quantity); break;
                            case "Кол-во ↓": query = query.OrderByDescending(p => p.Quantity); break;
                        }
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
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filter_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadProducts();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialogWindow();
            if (dialog.ShowDialog() == true)
            {
                LoadProducts();
                StatusText.Text = "✅ Товар добавлен";
            }
        }

        private void EditProduct(object parameter)
        {
            if (parameter is Product product)
            {
                var dialog = new ProductDialogWindow(product);
                if (dialog.ShowDialog() == true)
                {
                    LoadProducts();
                    StatusText.Text = "✅ Товар обновлен";
                }
            }
        }

        private void DeleteProduct(object parameter)
        {
            if (parameter is Product product)
            {
                var result = MessageBox.Show($"Удалить товар \"{product.Name}\"?", "Подтверждение", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new StoreDbContext())
                    {
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
            var ordersWindow = new OrdersWindow();
            ordersWindow.ShowDialog();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }

    // RelayCommand класс
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
    }
}
