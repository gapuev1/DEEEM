using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class OrderManagerForm : Form
    {
        private AppDbContext _context;
        private User _currentUser;
        private BindingSource _ordersBinding = new BindingSource();
        public OrderManagerForm(User user)
        {
            InitializeComponent();

            this.Font = new Font("Times New Roman", 9F, FontStyle.Regular);
            _context = new AppDbContext();
            _currentUser = user;

            // Гарантируем загрузку роли (если ещё не загружена)
            if (_currentUser.Role == null)
                _context.Entry(_currentUser).Reference(u => u.Role).Load();

            LoadOrders();
            ConfigureOrderItemsGrid();

            if (_currentUser.Role.Name != "Администратор")
            {
                btnAddOrder.Visible = false;
                btnEditOrder.Visible = false;
                btnDeleteOrder.Visible = false;
            }
        }
        private void ConfigureOrderItemsGrid()
        {
            dgvOrderItems.AutoGenerateColumns = false;
            dgvOrderItems.Columns.Clear();

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductArticle",
                DataPropertyName = "ProductArticle",
                HeaderText = "Артикул",
                Width = 100
            });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                DataPropertyName = "ProductName",   // вспомогательное свойство
                HeaderText = "Наименование",
                Width = 200
            });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                DataPropertyName = "Quantity",
                HeaderText = "Кол-во",
                Width = 80
            });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductPrice",
                DataPropertyName = "ProductPrice",   // вспомогательное свойство
                HeaderText = "Цена, руб.",
                Width = 100,
                DefaultCellStyle = { Format = "N2" }
            });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSum",
                DataPropertyName = "TotalSum",       // вспомогательное свойство
                HeaderText = "Сумма, руб.",
                Width = 100,
                DefaultCellStyle = { Format = "N2" }
            });
        }
        private void LoadOrders()
        {
            var orders = _context.Orders
                .Include(o => o.PickupPoint)
                .Include(o => o.User)
                .Include(o => o.Status)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToList();

            _ordersBinding.DataSource = orders;
            dgvOrders.DataSource = _ordersBinding;

            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();

            // Номер заказа
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderNumber",
                DataPropertyName = "OrderNumber",
                HeaderText = "Номер заказа"
            });
            // Дата заказа
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderDate",
                DataPropertyName = "OrderDate",
                HeaderText = "Дата заказа",
                DefaultCellStyle = { Format = "yyyy-MM-dd" }
            });
            // Статус - используем вспомогательное свойство
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusName",
                DataPropertyName = "StatusName",
                HeaderText = "Статус"

            });
            // Клиент - используем вспомогательное свойство
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClientFullName",
                DataPropertyName = "ClientFullName",
                HeaderText = "Клиент"
            });
            // Код получения
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PickupCode",
                DataPropertyName = "PickupCode",
                HeaderText = "Код получения"
            });
        }
        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                dgvOrderItems.DataSource = order.OrderItems.ToList();
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            // Форма добавления заказа (OrderEditForm)
            var form = new OrderEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
                LoadOrders();
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                var form = new OrderEditForm(order);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadOrders();
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    LoadOrders();
                }
            }
        }

       
    }
}
