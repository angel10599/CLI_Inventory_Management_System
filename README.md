# 📦 CLI Inventory Management System

## 🔐 Demo Login Credentials
*For testing purposes only*

| Username | Password | Role  |
|----------|----------|-------|
| admin    | admin123 | Admin |
| staff    | staff123 | Staff |

---

## ⚠️ Requirements

Before running this project, make sure you have installed:

- ✅ .NET 10 SDK

📌 Description

A Command-Line Interface (CLI) based Inventory Management System developed in C#.
It allows users to manage products, categories, suppliers, and transactions using OOP principles.
Data is stored using List<T> (no database).

🎯 Features
Add Category
Add Supplier
Add Product
View All Products
Search Product
Update Product
Delete Product
Restock Products
Deduct Stock
View Transaction History
Show Low-Stock Items
Compute Total Inventory Value
Menu-driven CLI system
Input validation and error handling
📁 Project Structure
CLI_Inventory_Management_System/
│
├── Models/
│   ├── Product.cs
│   ├── Category.cs
│   ├── Supplier.cs
│   ├── User.cs
│   └── TransactionRecord.cs
│
├── Services/
│   ├── InventoryService.cs
│   └── TransactionService.cs
│
├── Helpers/
│   ├── InputHelper.cs
│   └── ValidationHelper.cs
│
├── Program.cs
├── CLI_Inventory_Management_System.csproj
├── .gitignore
└── README.md
▶️ How to Run
git clone https://github.com/your-username/CLI_Inventory_Management_System.git
cd CLI_Inventory_Management_System
dotnet run
👤 Author

Angel M. Baldonado
BSIT-3A

📎 Notes
This project is for academic purposes only
Credentials are for demo/testing only
Passwords should be encrypted in real-world applications
