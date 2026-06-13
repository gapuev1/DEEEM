using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Views
{
    public partial class OrdersWindow : Window
    {
        private readonly User _currentUser;

        public OrdersWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadOrders();
            CloseBtn.Click += (s, e) => Close();
        }

        private void LoadOrders()
        {
            using (var db = new StoreDbContext())
            {
                var orders = db.Orders
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .ToList();
                OrdersGrid.ItemsSource = orders;
            }
        }
    }
}