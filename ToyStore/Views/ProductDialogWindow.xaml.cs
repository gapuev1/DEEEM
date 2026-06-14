using System;
using System.Linq;
using System.Windows;
using ToyStore.Models;

namespace ToyStore.Views
{
    public partial class ProductDialogWindow : Window
    {
        private readonly Product _editingProduct;

        public ProductDialogWindow(Product product = null)
        {
            InitializeComponent();
            _editingProduct = product;

            LoadCategories();
            LoadManufacturers();
            LoadSuppliers();

            if (product != null)
            {
                Title = "Редактирование игрушки";
                NameBox.Text = product.Name;
                CategoryBox.SelectedValue = product.CategoryId;
                ManufacturerBox.SelectedValue = product.ManufacturerId;
                SupplierBox.SelectedValue = product.SupplierId;
                DescriptionBox.Text = product.Description;
                PriceBox.Text = product.Price.ToString();
                QuantityBox.Text = product.Quantity.ToString();
                DiscountBox.Text = product.Discount.ToString();
            }

            SaveBtn.Click += SaveBtn_Click;
            CancelBtn.Click += (s, e) => DialogResult = false;
        }

        private void LoadCategories()
        {
            using (var db = new StoreDbContext())
            {
                CategoryBox.ItemsSource = db.Categories.ToList();
            }
        }

        private void LoadManufacturers()
        {
            using (var db = new StoreDbContext())
            {
                ManufacturerBox.ItemsSource = db.Manufacturers.ToList();
            }
        }

        private void LoadSuppliers()
        {
            using (var db = new StoreDbContext())
            {
                SupplierBox.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название игрушки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Введите корректное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(DiscountBox.Text, out decimal discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Скидка должна быть от 0 до 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new StoreDbContext())
            {
                if (_editingProduct == null)
                {
                    var product = new Product
                    {
                        Name = NameBox.Text,
                        CategoryId = (int)CategoryBox.SelectedValue,
                        ManufacturerId = (int)ManufacturerBox.SelectedValue,
                        SupplierId = (int)SupplierBox.SelectedValue,
                        Description = DescriptionBox.Text,
                        Price = price,
                        Quantity = quantity,
                        Discount = discount
                    };
                    db.Products.Add(product);
                }
                else
                {
                    _editingProduct.Name = NameBox.Text;
                    _editingProduct.CategoryId = (int)CategoryBox.SelectedValue;
                    _editingProduct.ManufacturerId = (int)ManufacturerBox.SelectedValue;
                    _editingProduct.SupplierId = (int)SupplierBox.SelectedValue;
                    _editingProduct.Description = DescriptionBox.Text;
                    _editingProduct.Price = price;
                    _editingProduct.Quantity = quantity;
                    _editingProduct.Discount = discount;
                    db.Products.Update(_editingProduct);
                }
                db.SaveChanges();
            }
            DialogResult = true;
        }
    }
}