using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;

namespace FurnitureStore.Views
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
                var query = db.Orders
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .AsQueryable();

                if (_currentUser.Role != "Admin")
                {
                    query = query.Where(o => o.UserId == _currentUser.Id);
                }

                OrdersGrid.ItemsSource = query.OrderByDescending(o => o.OrderDate).ToList();
            }
        }
    }
}