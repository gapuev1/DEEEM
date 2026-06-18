using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class ImportExcel : Form
    {
        private string _excelFolderPath = string.Empty;
        public ImportExcel()
        {
            InitializeComponent();
            ExcelPackage.License.SetNonCommercialPersonal("Вова");//Указать ваше имя
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _excelFolderPath = dialog.SelectedPath;
                lblFolderPath.Text = _excelFolderPath;
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_excelFolderPath))
            {
                MessageBox.Show("Выберите папку с Excel-файлами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnImport.Enabled = false;
            richLog.Clear();
            Log("Начало импорта...");

            try
            {
                using var db = new AppDbContext();
                await db.Database.EnsureCreatedAsync();

                // 1. Пункты выдачи
                await ImportPickupPoints(db);
                // 2. Роли и пользователи
                await ImportRolesAndUsers(db);
                // 3. Товары (категории, производители, поставщики)
                await ImportProducts(db);
                // 4. Заказы
                await ImportOrders(db);

                Log("Импорт успешно завершён!");
                MessageBox.Show("Импорт завершён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImport.Enabled = true;
            }
        }

        private async Task ImportPickupPoints(AppDbContext db)
        {
            string filePath = Path.Combine(_excelFolderPath, "Пункты выдачи_import.xlsx");
            if (!File.Exists(filePath))
            {
                Log($"Файл не найден: {filePath}");
                return;
            }

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets[0];
            int row = 2;
            int count = 0;
            while (sheet.Cells[row, 1].Value != null)
            {
                string address = sheet.Cells[row, 1].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(address) && !await db.PickupPoints.AnyAsync(p => p.Address == address))
                {
                    db.PickupPoints.Add(new PickupPoint { Address = address });
                    count++;
                }
                row++;
            }
            await db.SaveChangesAsync();
            Log($"Импортировано пунктов выдачи: {count}");
        }


        private async Task ImportRolesAndUsers(AppDbContext db)
        {
            // Предзаполнение ролей
            string[] roleNames = { "Администратор", "Менеджер", "Авторизированный клиент" };
            foreach (var roleName in roleNames)
            {
                if (!await db.Roles.AnyAsync(r => r.Name == roleName))
                    db.Roles.Add(new Role { Name = roleName });
            }
            await db.SaveChangesAsync();

            string filePath = Path.Combine(_excelFolderPath, "user_import.xlsx");
            if (!File.Exists(filePath))
            {
                Log($"Файл не найден: {filePath}");
                return;
            }

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets[0];
            int row = 2;
            int count = 0;
            while (sheet.Cells[row, 1].Value != null)
            {
                string roleName = sheet.Cells[row, 1].Value.ToString().Trim();
                string fullName = sheet.Cells[row, 2].Value.ToString().Trim();
                string login = sheet.Cells[row, 3].Value.ToString().Trim();
                string password = sheet.Cells[row, 4].Value.ToString().Trim();

                var role = await db.Roles.FirstAsync(r => r.Name == roleName);
                var (lastName, firstName, patronymic) = SplitFullName(fullName);

                if (!await db.Users.AnyAsync(u => u.Login == login))
                {
                    db.Users.Add(new User
                    {
                        RoleId = role.Id,
                        LastName = lastName,
                        FirstName = firstName,
                        Patronymic = patronymic,
                        Login = login,
                        PasswordHash = ComputeSha256Hash(password)
                    });
                    count++;
                }
                row++;
            }
            await db.SaveChangesAsync();
            Log($"Импортировано пользователей: {count}");
        }

        private async Task ImportProducts(AppDbContext db)
        {
            string filePath = Path.Combine(_excelFolderPath, "Tovar.xlsx");
            if (!File.Exists(filePath))
            {
                Log($"Файл не найден: {filePath}");
                return;
            }

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets[0];
            int row = 2;
            int count = 0;
            while (sheet.Cells[row, 1].Value != null)
            {
                string article = sheet.Cells[row, 1].Value.ToString().Trim();
                string name = sheet.Cells[row, 2].Value.ToString().Trim();
                string unit = sheet.Cells[row, 3].Value.ToString().Trim();
                decimal price = Convert.ToDecimal(sheet.Cells[row, 4].Value);
                string supplierName = sheet.Cells[row, 5].Value?.ToString().Trim();
                string manufacturerName = sheet.Cells[row, 6].Value?.ToString().Trim();
                string categoryName = sheet.Cells[row, 7].Value?.ToString().Trim();
                int discount = Convert.ToInt32(sheet.Cells[row, 8].Value);
                int quantity = Convert.ToInt32(sheet.Cells[row, 9].Value);
                string description = sheet.Cells[row, 10].Value?.ToString().Trim();
                string photo = sheet.Cells[row, 11].Value?.ToString().Trim();

                // Поставщик
                int? supplierId = null;
                if (!string.IsNullOrEmpty(supplierName))
                {
                    var supplier = await db.Suppliers.FirstOrDefaultAsync(s => s.Name == supplierName);
                    if (supplier == null)
                    {
                        supplier = new Supplier { Name = supplierName };
                        db.Suppliers.Add(supplier);
                        await db.SaveChangesAsync();
                    }
                    supplierId = supplier.Id;
                }

                // Производитель
                int? manufacturerId = null;
                if (!string.IsNullOrEmpty(manufacturerName))
                {
                    var manufacturer = await db.Manufacturers.FirstOrDefaultAsync(m => m.Name == manufacturerName);
                    if (manufacturer == null)
                    {
                        manufacturer = new Manufacturer { Name = manufacturerName };
                        db.Manufacturers.Add(manufacturer);
                        await db.SaveChangesAsync();
                    }
                    manufacturerId = manufacturer.Id;
                }

                // Категория
                int? categoryId = null;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    var category = await db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category { Name = categoryName };
                        db.Categories.Add(category);
                        await db.SaveChangesAsync();
                    }
                    categoryId = category.Id;
                }

                if (!await db.Products.AnyAsync(p => p.Article == article))
                {
                    db.Products.Add(new Product
                    {
                        Article = article,
                        Name = name,
                        Unit = unit,
                        Price = price,
                        SupplierId = supplierId,
                        ManufacturerId = manufacturerId,
                        CategoryId = categoryId,
                        Discount = discount,
                        StockQuantity = quantity,
                        Description = description,
                        PhotoFileName = photo
                    });
                    count++;
                }
                row++;
            }
            await db.SaveChangesAsync();
            Log($"Импортировано товаров: {count}");
        }

        private async Task ImportOrders(AppDbContext db)
        {
            // Статусы заказов
            string[] statusNames = { "Новый", "Завершен", "В обработке", "Готов к выдаче", "Выдан", "Отменён" };
            foreach (var statusName in statusNames)
            {
                if (!await db.OrderStatuses.AnyAsync(s => s.Name == statusName))
                    db.OrderStatuses.Add(new OrderStatus { Name = statusName });
            }
            await db.SaveChangesAsync();

            string filePath = Path.Combine(_excelFolderPath, "Заказ_import.xlsx");
            if (!File.Exists(filePath))
            {
                Log($"Файл не найден: {filePath}");
                return;
            }

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets[0];
            int row = 2;
            int ordersCount = 0, itemsCount = 0;
            while (sheet.Cells[row, 1].Value != null)
            {
                int orderNumber = Convert.ToInt32(sheet.Cells[row, 1].Value);
                string articlesRaw = sheet.Cells[row, 2].Value.ToString().Trim();
                DateTime orderDate;
                if (!DateTime.TryParse(sheet.Cells[row, 3].Value.ToString(), out orderDate))
                {
                    Log($"Пропуск заказа {orderNumber}: неверная дата");
                    row++;
                    continue;
                }
                DateTime? deliveryDate = sheet.Cells[row, 4].Value == null ? null : Convert.ToDateTime(sheet.Cells[row, 4].Value);
                int pickupPointId = Convert.ToInt32(sheet.Cells[row, 5].Value);
                string clientFullName = sheet.Cells[row, 6].Value.ToString().Trim();
                string pickupCode = sheet.Cells[row, 7].Value.ToString().Trim();
                string statusName = sheet.Cells[row, 8].Value.ToString().Trim();

                // Поиск пользователя по ФИО
                var user = await FindUserByFullName(db, clientFullName);
                if (user == null)
                {
                    Log($"Пользователь '{clientFullName}' не найден, заказ {orderNumber} пропущен");
                    row++;
                    continue;
                }

                var pickupPoint = await db.PickupPoints.FindAsync(pickupPointId);
                if (pickupPoint == null)
                {
                    Log($"Пункт выдачи ID {pickupPointId} не найден, заказ {orderNumber} пропущен");
                    row++;
                    continue;
                }

                var status = await db.OrderStatuses.FirstAsync(s => s.Name == statusName);

                var order = new Order
                {
                    OrderNumber = orderNumber,
                    OrderDate = orderDate,
                    DeliveryDate = deliveryDate,
                    PickupPointId = pickupPointId,
                    UserId = user.Id,
                    StatusId = status.Id,
                    PickupCode = pickupCode
                };
                db.Orders.Add(order);
                await db.SaveChangesAsync(); // чтобы получить Id заказа

                // Разбор товаров: "А112Т4, 2, F635R4, 2"
                var parts = articlesRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i += 2)
                {
                    string article = parts[i].Trim();
                    if (!int.TryParse(parts[i + 1].Trim(), out int quantity))
                    {
                        Log($"Ошибка количества для {article} в заказе {orderNumber}");
                        continue;
                    }
                    var product = await db.Products.FindAsync(article);
                    if (product == null)
                    {
                        Log($"Товар {article} не найден, позиция пропущена");
                        continue;
                    }
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.Id,
                        ProductArticle = article,
                        Quantity = quantity
                    });
                    itemsCount++;
                }
                await db.SaveChangesAsync();
                ordersCount++;
                row++;
            }
            Log($"Импортировано заказов: {ordersCount}, позиций: {itemsCount}");
        }

        // Вспомогательные методы
        private (string lastName, string firstName, string patronymic) SplitFullName(string fullName)
        {
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3) return (parts[0], parts[1], parts[2]);
            if (parts.Length == 2) return (parts[0], parts[1], "");
            return (fullName, "", "");
        }

        private async Task<User?> FindUserByFullName(AppDbContext db, string fullName)
        {
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                string lastName = parts[0];
                string firstName = parts[1];
                string patronymic = parts.Length >= 3 ? parts[2] : "";
                return await db.Users.FirstOrDefaultAsync(u =>
                    u.LastName == lastName && u.FirstName == firstName && u.Patronymic == patronymic);
            }
            return null;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToBase64String(bytes);
            }
        }

        private void Log(string message)
        {
            if (richLog.InvokeRequired)
            {
                richLog.Invoke(() => Log(message));
                return;
            }
            richLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
            richLog.ScrollToCaret();
        }
    }
}
