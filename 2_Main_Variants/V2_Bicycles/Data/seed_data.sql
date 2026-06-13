-- Создание базы данных
CREATE DATABASE StoreDB;
GO

USE StoreDB;
GO

-- Таблица пользователей
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'Client',
    IsActive BIT NOT NULL DEFAULT 1
);

-- Таблица категорий
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- Таблица производителей
CREATE TABLE Manufacturers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- Таблица поставщиков
CREATE TABLE Suppliers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    ContactInfo NVARCHAR(200)
);

-- Таблица товаров
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    CategoryId INT NOT NULL,
    ManufacturerId INT NOT NULL,
    SupplierId INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Unit NVARCHAR(20) NOT NULL DEFAULT 'шт',
    Quantity INT NOT NULL DEFAULT 0,
    Discount DECIMAL(5,2) NOT NULL DEFAULT 0,
    ImagePath NVARCHAR(500),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers(Id),
    FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);

-- Таблица статусов заказов
CREATE TABLE OrderStatuses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE
);

-- Таблица заказов
CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    OrderNumber NVARCHAR(20) NOT NULL UNIQUE,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    DeliveryDate DATETIME,
    PickupPointAddress NVARCHAR(200),
    StatusId INT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (StatusId) REFERENCES OrderStatuses(Id)
);

-- Таблица позиций заказа
CREATE TABLE OrderItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    PriceAtOrder DECIMAL(18,2) NOT NULL,
    DiscountAtOrder DECIMAL(5,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Добавление пользователей
INSERT INTO Users (Login, Password, FullName, Role) VALUES
('admin', 'admin123', 'Администратор', 'Admin'),
('manager', 'manager123', 'Менеджер', 'Manager'),
('client', 'client123', 'Клиент', 'Client'),
('guest', 'guest', 'Гость', 'Guest');

-- Добавление статусов заказов
INSERT INTO OrderStatuses (Name) VALUES
('Новый'), ('В обработке'), ('Готов к выдаче'), ('Выдан'), ('Отменен');

-- Добавление поставщиков
INSERT INTO Suppliers (Name, ContactInfo) VALUES
('ООО Поставка', 'Москва, ул. Торговая 1'),
('ИП Снабжение', 'Москва, ул. Складская 5');

-- ДАННЫЕ ДЛЯ ВАРИАНТА В2 (ВЕЛОСИПЕДЫ)
INSERT INTO Categories (Name) VALUES
('Горные'), ('Шоссейные'), ('Городские'), ('Детские');

INSERT INTO Manufacturers (Name) VALUES
('Stels'), ('Giant'), ('Trek'), ('Forward');

INSERT INTO Products (Name, CategoryId, ManufacturerId, SupplierId, Price, Quantity, Discount) VALUES
('Stels Navigator 750', 1, 1, 1, 25000.00, 15, 0),
('Stels Miss 7100', 1, 1, 1, 22000.00, 8, 10),
('Giant Escape 3', 3, 2, 2, 35000.00, 5, 0),
('Giant Defy Advanced', 2, 2, 2, 120000.00, 3, 5),
('Trek FX 2', 3, 3, 1, 45000.00, 7, 0),
('Trek Marlin 5', 1, 3, 1, 38000.00, 12, 15),
('Forward Sport 24', 4, 4, 2, 15000.00, 10, 20),
('Forward Apache', 1, 4, 2, 28000.00, 6, 0),
('Детский велосипед Forward', 4, 4, 2, 8000.00, 0, 0);