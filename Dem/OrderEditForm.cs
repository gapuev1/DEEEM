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
    public partial class OrderEditForm : Form
    {
        private AppDbContext _context;
        private Order _editingOrder;
        private bool isNew;
        private BindingSource _itemsBinding = new BindingSource();

        public OrderEditForm(Order order = null)
        {
            InitializeComponent();

            this.Font = new Font("Times New Roman", 9F, FontStyle.Regular);
            _context = new AppDbContext();
            isNew = (order == null);
            _editingOrder = order ?? new Order { OrderItems = new System.Collections.Generic.List<OrderItem>() };

            LoadPickupPoints();
            LoadStatuses();

            if (!isNew)
                LoadOrderData();

            // Настройка грида товаров
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductArticle", HeaderText = "Артикул" });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Кол-во" });
            dgvItems.DataSource = _itemsBinding;
            RefreshItemsGrid();
        }

        private void LoadPickupPoints()
        {
            var points = _context.PickupPoints.ToList();
            cbPickupPoint.DataSource = points;
            cbPickupPoint.DisplayMember = "Address";
            cbPickupPoint.ValueMember = "Id";
        }

     

        private void LoadStatuses()
        {
            var statuses = _context.OrderStatuses.ToList();
            cbStatus.DataSource = statuses;
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";
        }

        private void LoadOrderData()
        {
            txtOrderNumber.Text = _editingOrder.OrderNumber.ToString();
            dtpOrderDate.Value = _editingOrder.OrderDate;
            dtpDeliveryDate.Value = _editingOrder.DeliveryDate ?? DateTime.Today.AddDays(7);
            cbPickupPoint.SelectedValue = _editingOrder.PickupPointId;
            txtClientFullName.Text = $"{_editingOrder.User.LastName} {_editingOrder.User.FirstName} {_editingOrder.User.Patronymic}".Trim();
            txtPickupCode.Text = _editingOrder.PickupCode;
            cbStatus.SelectedValue = _editingOrder.StatusId;
        }

        private void RefreshItemsGrid()
        {
            _itemsBinding.DataSource = null;
            _itemsBinding.DataSource = _editingOrder.OrderItems.ToList();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            _editingOrder.OrderItems.Add(new OrderItem { ProductArticle = "", Quantity = 1 });
            RefreshItemsGrid();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow?.DataBoundItem is OrderItem item)
            {
                _editingOrder.OrderItems.Remove(item);
                RefreshItemsGrid();
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            _editingOrder.OrderNumber = int.Parse(txtOrderNumber.Text);
            _editingOrder.OrderDate = dtpOrderDate.Value;
            _editingOrder.DeliveryDate = dtpDeliveryDate.Value;
            _editingOrder.PickupPointId = (int)cbPickupPoint.SelectedValue;
            _editingOrder.PickupCode = txtPickupCode.Text.Trim();
            _editingOrder.StatusId = (int)cbStatus.SelectedValue;

            // Обработка ФИО: разбиваем на фамилию, имя, отчество
            string fullName = txtClientFullName.Text.Trim();
            var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string lastName = nameParts.Length > 0 ? nameParts[0] : "";
            string firstName = nameParts.Length > 1 ? nameParts[1] : "";
            string patronymic = nameParts.Length > 2 ? nameParts[2] : "";

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Введите Фамилию и Имя (например: Иванов Иван Иванович)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ищем существующего пользователя с такой фамилией, именем, отчеством
            var user = _context.Users.FirstOrDefault(u => u.LastName == lastName && u.FirstName == firstName && u.Patronymic == patronymic);
            if (user == null)
            {
                // Создаём нового пользователя с ролью "Авторизированный клиент"
                var clientRole = _context.Roles.FirstOrDefault(r => r.Name == "Авторизированный клиент");
                if (clientRole == null)
                {
                    MessageBox.Show("Роль 'Авторизированный клиент' не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                user = new User
                {
                    LastName = lastName,
                    FirstName = firstName,
                    Patronymic = patronymic,
                    RoleId = clientRole.Id,
                    Login = "", // можно сгенерировать, но пока пусто
                    PasswordHash = ""
                };
                _context.Users.Add(user);
                _context.SaveChanges(); // сохраняем, чтобы получить Id
            }
            _editingOrder.UserId = user.Id;

            // Проверка товаров
            foreach (var item in _editingOrder.OrderItems)
            {
                if (string.IsNullOrWhiteSpace(item.ProductArticle) || !_context.Products.Any(p => p.Article == item.ProductArticle))
                {
                    MessageBox.Show("Неверный артикул товара в одной из позиций", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (item.Quantity <= 0)
                {
                    MessageBox.Show("Количество товара должно быть положительным", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                if (isNew)
                    _context.Orders.Add(_editingOrder);
                else
                    _context.Orders.Update(_editingOrder);

                _context.SaveChanges();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateFields()
        {
            if (!int.TryParse(txtOrderNumber.Text, out _))
            { MessageBox.Show("Номер заказа должен быть числом"); return false; }
            if (dtpDeliveryDate.Value < dtpOrderDate.Value)
            { MessageBox.Show("Дата доставки не может быть раньше даты заказа"); return false; }
            if (string.IsNullOrWhiteSpace(txtClientFullName.Text))
            { MessageBox.Show("Введите ФИО клиента"); return false; }
            if (string.IsNullOrWhiteSpace(txtPickupCode.Text))
            { MessageBox.Show("Укажите код получения"); return false; }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

