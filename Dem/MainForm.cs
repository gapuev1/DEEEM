using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Demo
{
    public partial class MainForm : Form
    {
        private AppDbContext _context;
        private User _currentUser;
        private BindingSource _productsBinding = new BindingSource();

        public MainForm(User user)
        {
            InitializeComponent();

            this.Font = new Font("Times New Roman", 9F, FontStyle.Regular);
            this.BackColor = Color.White;
            panelFilter.BackColor = ColorTranslator.FromHtml("#7FFF00");

            btnManageOrders.BackColor = ColorTranslator.FromHtml("#00FA9A");
            btnManageOrders.FlatStyle = FlatStyle.Flat;
            btnManageOrders.FlatAppearance.BorderSize = 0;

            _context = new AppDbContext();
            _currentUser = user;

            // Настройка грида товаров
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Article", HeaderText = "Артикул" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Наименование" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Цена" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Discount", HeaderText = "Скидка %" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockQuantity", HeaderText = "Остаток" });
            dgvProducts.DataSource = _productsBinding;

            LoadFilters();
            cbSortBy.Items.AddRange(new object[] { "Без сортировки", "Цена (возр.)", "Цена (убыв.)", "Наименование" });
            cbSortBy.SelectedIndex = 0;
            LoadProducts();

            // Настройка UI по роли
            string roleName = _currentUser?.Role?.Name;
            if (roleName == "Авторизированный клиент" || _currentUser == null)
            {
                panelFilter.Visible = false;
                btnManageOrders.Visible = false;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
            else if (roleName == "Менеджер")
            {
                panelFilter.Visible = true;
                btnManageOrders.Visible = true;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
            else if (roleName == "Администратор")
            {
                panelFilter.Visible = true;
                btnManageOrders.Visible = true;
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        private void LoadFilters()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).Select(c => c.Name).ToList();
            categories.Insert(0, "Все");
            cbCategory.DataSource = categories;

            var manufacturers = _context.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList();
            manufacturers.Insert(0, "Все");
            cbManufacturer.DataSource = manufacturers;
        }

        private void LoadProducts()
        {
            // Запоминаем артикул текущего выбранного товара
            string currentArticle = (dgvProducts.CurrentRow?.DataBoundItem as Product)?.Article;

            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier);

            if (panelFilter.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string search = txtSearch.Text.ToLower();
                    query = query.Where(p => p.Name.ToLower().Contains(search) || p.Article.ToLower().Contains(search));
                }
                if (cbCategory.SelectedItem != null && cbCategory.SelectedItem.ToString() != "Все")
                {
                    string catName = cbCategory.SelectedItem.ToString();
                    query = query.Where(p => p.Category != null && p.Category.Name == catName);
                }
                if (cbManufacturer.SelectedItem != null && cbManufacturer.SelectedItem.ToString() != "Все")
                {
                    string manName = cbManufacturer.SelectedItem.ToString();
                    query = query.Where(p => p.Manufacturer != null && p.Manufacturer.Name == manName);
                }
                string sort = cbSortBy.SelectedItem?.ToString();
                if (sort == "Цена (возр.)") query = query.OrderBy(p => p.Price);
                else if (sort == "Цена (убыв.)") query = query.OrderByDescending(p => p.Price);
                else if (sort == "Наименование") query = query.OrderBy(p => p.Name);
            }

            var newList = query.ToList();
            _productsBinding.DataSource = newList;

            // Восстанавливаем выделение
            if (!string.IsNullOrEmpty(currentArticle))
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    if (newList[i].Article == currentArticle)
                    {
                        dgvProducts.ClearSelection();
                        dgvProducts.Rows[i].Selected = true;
                        dgvProducts.CurrentCell = dgvProducts.Rows[i].Cells[0];
                        break;
                    }
                }
            }

            UpdateProductImage();
            pbProductImage.Refresh(); // Принудительная перерисовка
        }

        private void UpdateProductImage()
        {
            // Освобождаем старую картинку, если она есть
            if (pbProductImage.Image != null)
            {
                var old = pbProductImage.Image;
                pbProductImage.Image = null;
                old.Dispose();
            }

            string photoFileName = (dgvProducts.CurrentRow?.DataBoundItem as Product)?.PhotoFileName;
            pbProductImage.Image = ImageHelper.LoadProductImage(photoFileName);
        }

        private void btnFilte_Click(object sender, EventArgs e) => LoadProducts();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new ProductEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
                LoadProducts();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product prod)
            {
                var form = new ProductEditForm(prod);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadProducts();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product prod)
            {
                if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _context.Products.Remove(prod);
                    _context.SaveChanges();
                    LoadProducts();
                }
            }
        }
        private void btnManageOrders_Click(object sender, EventArgs e)
        {
            var ordersForm = new OrderManagerForm(_currentUser);
            ordersForm.ShowDialog();
        }
        private void dgvProducts_SelectionChanged(object sender, EventArgs e) => UpdateProductImage();
        private void dgvProducts_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var product = dgvProducts.Rows[e.RowIndex].DataBoundItem as Product;
            if (product != null && product.Discount > 15)
            {
                dgvProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2E8B57");
                dgvProducts.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }
    }
}