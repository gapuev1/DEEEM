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
using WinFormsApp = System.Windows.Forms.Application;

namespace Demo
{
    public partial class ProductEditForm : Form
    {
        private AppDbContext _context;
        private Product _editingProduct;
        private bool isNew;

        public ProductEditForm(Product product = null)
        {
            InitializeComponent();
            this.Font = new Font("Times New Roman", 9F, FontStyle.Regular);
            _context = new AppDbContext();
            isNew = (product == null);

            if (isNew)
            {
                _editingProduct = new Product();
            }
            else
            {
                // Загружаем продукт из текущего контекста, чтобы избежать конфликта
                _editingProduct = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Supplier)
                    .FirstOrDefault(p => p.Article == product.Article);
                if (_editingProduct == null)
                {
                    MessageBox.Show("Товар не найден в базе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
            }

            LoadComboBoxes();

            if (!isNew)
                LoadProductData();
        }

        private void LoadComboBoxes()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";

            var manufacturers = _context.Manufacturers.OrderBy(m => m.Name).ToList();
            cbManufacturer.DataSource = manufacturers;
            cbManufacturer.DisplayMember = "Name";
            cbManufacturer.ValueMember = "Id";

            var suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            cbSupplier.DataSource = suppliers;
            cbSupplier.DisplayMember = "Name";
            cbSupplier.ValueMember = "Id";
        }

        private void LoadProductData()
        {
            txtArticle.Text = _editingProduct.Article;
            txtArticle.ReadOnly = !isNew;
            txtName.Text = _editingProduct.Name;
            txtUnit.Text = _editingProduct.Unit;
            txtPrice.Text = _editingProduct.Price.ToString();
            txtDiscount.Text = _editingProduct.Discount.ToString();
            txtStock.Text = _editingProduct.StockQuantity.ToString();
            txtDescription.Text = _editingProduct.Description;
            txtPhoto.Text = _editingProduct.PhotoFileName;

            if (_editingProduct.CategoryId.HasValue)
            {
                var item = cbCategory.Items.Cast<Category>().FirstOrDefault(c => c.Id == _editingProduct.CategoryId.Value);
                cbCategory.SelectedItem = item;
            }
            else cbCategory.SelectedIndex = -1;

            if (_editingProduct.ManufacturerId.HasValue)
            {
                var item = cbManufacturer.Items.Cast<Manufacturer>().FirstOrDefault(m => m.Id == _editingProduct.ManufacturerId.Value);
                cbManufacturer.SelectedItem = item;
            }
            else cbManufacturer.SelectedIndex = -1;

            if (_editingProduct.SupplierId.HasValue)
            {
                var item = cbSupplier.Items.Cast<Supplier>().FirstOrDefault(s => s.Id == _editingProduct.SupplierId.Value);
                cbSupplier.SelectedItem = item;
            }
            else cbSupplier.SelectedIndex = -1;

            if (string.IsNullOrEmpty(_editingProduct.PhotoFileName))
                pbProductImage.Image = ImageHelper.LoadProductImage("picture.png");
            else
                pbProductImage.Image = ImageHelper.LoadProductImage(_editingProduct.PhotoFileName);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            _editingProduct.Article = txtArticle.Text.Trim();
            _editingProduct.Name = txtName.Text.Trim();
            _editingProduct.Unit = txtUnit.Text.Trim();
            _editingProduct.Price = decimal.Parse(txtPrice.Text);
            _editingProduct.Discount = int.Parse(txtDiscount.Text);
            _editingProduct.StockQuantity = int.Parse(txtStock.Text);
            _editingProduct.Description = txtDescription.Text;
            _editingProduct.PhotoFileName = txtPhoto.Text;

            // Получение Id из выбранных элементов
            _editingProduct.CategoryId = cbCategory.SelectedItem != null ? ((Category)cbCategory.SelectedItem).Id : (int?)null;
            _editingProduct.ManufacturerId = cbManufacturer.SelectedItem != null ? ((Manufacturer)cbManufacturer.SelectedItem).Id : (int?)null;
            _editingProduct.SupplierId = cbSupplier.SelectedItem != null ? ((Supplier)cbSupplier.SelectedItem).Id : (int?)null;

            try
            {
                if (isNew)
                    _context.Products.Add(_editingProduct);
                else
                    _context.Products.Update(_editingProduct);

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
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            { MessageBox.Show("Артикул обязателен"); return false; }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { MessageBox.Show("Наименование обязательно"); return false; }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            { MessageBox.Show("Цена должна быть положительным числом"); return false; }
            if (!int.TryParse(txtDiscount.Text, out int discount) || discount < 0 || discount > 100)
            { MessageBox.Show("Скидка должна быть от 0 до 100"); return false; }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            { MessageBox.Show("Количество на складе не может быть отрицательным"); return false; }
            return true;
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string destFolder = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(destFolder))
                        Directory.CreateDirectory(destFolder);
                    string destFileName = Path.Combine(destFolder, Path.GetFileName(ofd.FileName));
                    File.Copy(ofd.FileName, destFileName, true);  // true – перезапись
                    txtPhoto.Text = Path.GetFileName(ofd.FileName);
                    // Обновляем preview
                    pbProductImage.Image = new Bitmap(destFileName);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
