-- Tạo bảng Customers
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Password NVARCHAR(255),
    ContactName NVARCHAR(255),
    Address NVARCHAR(255),
    Phone NVARCHAR(255)
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    RequiredDate DATE,
    ShippedDate DATE,
    Freight DECIMAL(10, 2),
    ShipAddress NVARCHAR(255),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Tạo bảng Order Details
CREATE TABLE OrderDetails (
    OrderID INT,
    ProductID INT,
    UnitPrice DECIMAL(10, 2),
    Quantity INT,
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(255),
    SupplierID INT,
    CategoryID INT,
    QuantityPerUnit NVARCHAR(255),
    UnitPrice DECIMAL(10, 2),
    ProductImage IMAGE
);

-- Tạo bảng Categories
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName NVARCHAR(255),
    Description TEXT
);

-- Tạo bảng Suppliers
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY,
    CompanyName NVARCHAR(255),
    Address NVARCHAR(255),
    Phone NVARCHAR(255)
);

-- Tạo bảng Account
CREATE TABLE Account (
    AccountID INT PRIMARY KEY,
    UserName NVARCHAR(255),
    Password NVARCHAR(255),
    FullName NVARCHAR(255),
    Type NVARCHAR(50)
);

-- Thiết lập mối quan hệ giữa các bảng
ALTER TABLE Products
ADD FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID);

ALTER TABLE Products
ADD FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID);

ALTER TABLE OrderDetails
ADD FOREIGN KEY (ProductID) REFERENCES Products(ProductID);

-- Lưu ý: Các kiểu dữ liệu và mối quan hệ có thể cần được điều chỉnh
-- phù hợp với yêu cầu cụ thể của cơ sở dữ liệu của bạn.