using System.Linq;
using System.Windows;
using BikeStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Views
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow(User user)
        {
            InitializeComponent();
            LoadOrders();
            CloseBtn.Click += (s, e) => Close();
        }

        private void LoadOrders()
        {
            using (var db = new StoreDbContext())
            {
                OrdersGrid.ItemsSource = db.Orders.Include(o => o.User).Include(o => o.Status).ToList();
            }
        }
    }
}