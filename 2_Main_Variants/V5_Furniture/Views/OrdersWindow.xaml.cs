using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using FurnitureApp.Models;

namespace FurnitureApp.Views
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
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
