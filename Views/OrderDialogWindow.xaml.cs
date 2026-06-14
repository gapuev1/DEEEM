using System;
using System.Linq;
using System.Windows;
using BikeStore.Models;

namespace BikeStore.Views
{
    public partial class OrderDialogWindow : Window
    {
        private readonly Order _editingOrder;
        private static int _nextOrderNumber = 100;

        public OrderDialogWindow(Order order = null)
        {
            InitializeComponent();
            _editingOrder = order;

            LoadUsers();
            LoadStatuses();

            if (order != null)
            {
                Title = "Редактирование заказа";
                UserBox.SelectedValue = order.UserId;
                StatusBox.SelectedValue = order.StatusId;
                AddressBox.Text = order.PickupPointAddress;
                if (order.DeliveryDate.HasValue)
                    DeliveryDatePicker.SelectedDate = order.DeliveryDate.Value;
            }

            SaveBtn.Click += SaveBtn_Click;
            CancelBtn.Click += (s, e) => DialogResult = false;
        }

        private void LoadUsers()
        {
            using (var db = new StoreDbContext())
            {
                UserBox.ItemsSource = db.Users.Where(u => u.Role == "Client").ToList();
            }
        }

        private void LoadStatuses()
        {
            using (var db = new StoreDbContext())
            {
                StatusBox.ItemsSource = db.OrderStatuses.ToList();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new StoreDbContext())
            {
                if (_editingOrder == null)
                {
                    // Добавление
                    var order = new Order
                    {
                        UserId = (int)UserBox.SelectedValue,
                        StatusId = (int)StatusBox.SelectedValue,
                        PickupPointAddress = AddressBox.Text,
                        DeliveryDate = DeliveryDatePicker.SelectedDate,
                        OrderNumber = $"ORD-{_nextOrderNumber++:D3}",
                        OrderDate = DateTime.Now,
                        TotalAmount = 0
                    };
                    db.Orders.Add(order);
                }
                else
                {
                    // Редактирование
                    _editingOrder.UserId = (int)UserBox.SelectedValue;
                    _editingOrder.StatusId = (int)StatusBox.SelectedValue;
                    _editingOrder.PickupPointAddress = AddressBox.Text;
                    _editingOrder.DeliveryDate = DeliveryDatePicker.SelectedDate;
                    db.Orders.Update(_editingOrder);
                }
                db.SaveChanges();
            }
            DialogResult = true;
        }
    }
}