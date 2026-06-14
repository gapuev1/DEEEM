using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using FurnitureStore.Models;

namespace FurnitureStore.Views
{
    public partial class ProductDialogWindow : Window
    {
        private readonly Product _editingProduct;
        private string _selectedImagePath;
        private string _selectedImageFileName;

        public ProductDialogWindow(Product product = null)
        {
            InitializeComponent();
            _editingProduct = product;

            LoadCategories();
            LoadManufacturers();
            LoadSuppliers();

            if (product != null)
            {
                Title = "Редактирование товара";
                ArticleBox.Text = product.Article;
                NameBox.Text = product.Name;
                CategoryBox.SelectedValue = product.CategoryId;
                ManufacturerBox.SelectedValue = product.ManufacturerId;
                SupplierBox.SelectedValue = product.SupplierId;
                DescriptionBox.Text = product.Description;
                PriceBox.Text = product.Price.ToString();
                QuantityBox.Text = product.Quantity.ToString();
                DiscountBox.Text = product.Discount.ToString();

                // Загрузка существующего фото
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    string imageFullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", product.ImagePath);
                    if (File.Exists(imageFullPath))
                    {
                        PreviewImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imageFullPath));
                        FileNameText.Text = product.ImagePath;
                    }
                }
            }

            SelectImageBtn.Click += SelectImageBtn_Click;
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

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            dialog.Title = "Выберите фото товара";

            if (dialog.ShowDialog() == true)
            {
                _selectedImagePath = dialog.FileName;
                _selectedImageFileName = Path.GetFileName(_selectedImagePath);

                // Показываем превью
                PreviewImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(_selectedImagePath));
                FileNameText.Text = _selectedImageFileName;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(ArticleBox.Text))
            {
                MessageBox.Show("Введите артикул товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            string savedImagePath = null;

            // Сохранение выбранного изображения
            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(imagesDir))
                    Directory.CreateDirectory(imagesDir);

                // Сохраняем с оригинальным именем или генерируем новое
                string fileName = _selectedImageFileName;
                string destPath = Path.Combine(imagesDir, fileName);

                try
                {
                    // Копируем файл (перезаписываем если существует)
                    File.Copy(_selectedImagePath, destPath, true);
                    savedImagePath = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            using (var db = new StoreDbContext())
            {
                if (_editingProduct == null)
                {
                    // Добавление нового товара
                    var product = new Product
                    {
                        Article = ArticleBox.Text,
                        Name = NameBox.Text,
                        CategoryId = (int)CategoryBox.SelectedValue,
                        ManufacturerId = (int)ManufacturerBox.SelectedValue,
                        SupplierId = (int)SupplierBox.SelectedValue,
                        Description = DescriptionBox.Text,
                        Price = price,
                        Quantity = quantity,
                        Discount = discount,
                        ImagePath = savedImagePath
                    };
                    db.Products.Add(product);
                }
                else
                {
                    // Редактирование существующего товара
                    _editingProduct.Article = ArticleBox.Text;
                    _editingProduct.Name = NameBox.Text;
                    _editingProduct.CategoryId = (int)CategoryBox.SelectedValue;
                    _editingProduct.ManufacturerId = (int)ManufacturerBox.SelectedValue;
                    _editingProduct.SupplierId = (int)SupplierBox.SelectedValue;
                    _editingProduct.Description = DescriptionBox.Text;
                    _editingProduct.Price = price;
                    _editingProduct.Quantity = quantity;
                    _editingProduct.Discount = discount;

                    if (savedImagePath != null)
                    {
                        // Удаляем старое фото если есть
                        if (!string.IsNullOrEmpty(_editingProduct.ImagePath))
                        {
                            string oldImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", _editingProduct.ImagePath);
                            if (File.Exists(oldImagePath))
                                File.Delete(oldImagePath);
                        }
                        _editingProduct.ImagePath = savedImagePath;
                    }

                    db.Products.Update(_editingProduct);
                }
                db.SaveChanges();
            }
            DialogResult = true;
        }
    }
}