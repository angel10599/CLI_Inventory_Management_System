# 📦 CLI Inventory Management System

## 🔐 Demo Login Credentials
*For testing purposes only*

| Username | Password | Role  |
|----------|----------|-------|
| admin    | admin123 | Admin |
| staff    | staff123 | Staff |

---

## 📌 Description
A Command-Line Interface (CLI) based Inventory Management System developed in C#. It allows users to manage products, categories, suppliers, and transactions using OOP principles. Data is stored using `List<T>` (no database).

---

## ⚠️ Requirements

Before running this project, make sure you have installed:

- ✅ .NET 10 SDK


## 🎯 Features
- Add Category  
- Add Supplier  
- Add Product  
- View All Products  
- Search Product  
- Update Product  
- Delete Product  
- Restock Products  
- Deduct Stock  
- View Transaction History  
- Show Low-Stock Items  
- Compute Total Inventory Value  
- Menu-driven CLI system  
- Input validation and error handling  
---

## 🛠️ Technologies Used
- C#  
- .NET Console Application  

---

## 📁 Project Structure
```
/Models
  - Product.cs
  - Category.cs
  - Supplier.cs
  - User.cs
  - TransactionRecord.cs

/Services
  - InventoryService.cs
  - TransactionService.cs

/Helpers
  - InputHelper.cs
  - ValidationHelper.cs

Program.cs
.gitignore
README.md
```

---

## 🚀 Run
```bash
dotnet run
```

---

## 👤 Author
**Angel M. Baldonado**  
BSIT-3A  

---

## 📎 Notes
This project is for academic purposes only.  
Credentials are for demo/testing only.  
Passwords should be encrypted in real-world applications.
