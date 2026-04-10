using CLI_Inventory_Management_System.Models;

namespace CLI_Inventory_Management_System.Services
{
	public class InventoryService
	{
		// List<T> storage only - no database (Requirement #2)
		private List<Product> _products = new List<Product>();
		private List<Category> _categories = new List<Category>();
		private List<Supplier> _suppliers = new List<Supplier>();
		private List<TransactionRecord> _transactions = new List<TransactionRecord>();

		private int _productIdCounter = 1;
		private int _categoryIdCounter = 1;
		private int _supplierIdCounter = 1;
		private int _transactionIdCounter = 1;

		private string _currentUser = "System";

		// Method - set current logged-in user for transaction logging
		public void SetCurrentUser(string username)
		{
			_currentUser = username;
		}

		// ─────────────────────────────────────────────────────────
		// 🔥 SEED DATA
		// ─────────────────────────────────────────────────────────
		public void SeedInitialData()
		{
			// Prevent duplicate seeding
			if (_categories.Count == 0)
			{
				AddCategory("Electronics", "Gadgets and electronic devices");
				AddCategory("Groceries", "Food and basic household goods");
				AddCategory("Beverages", "Drinks and bottled products");
				AddCategory("Personal Care", "Hygiene and self-care items");
				AddCategory("School Supplies", "School and office materials");
			}

			if (_suppliers.Count == 0)
			{
				AddSupplier("TechSource Inc.", "09123456789", "techsource@gmail.com");
				AddSupplier("FreshMart Supply", "09234567891", "freshmart@gmail.com");
				AddSupplier("AquaPure Distributors", "09345678912", "aquapure@gmail.com");
				AddSupplier("CarePlus Trading", "09456789123", "careplus@gmail.com");
				AddSupplier("EduSupply Co.", "09567891234", "edusupply@gmail.com");
			}

			if (_products.Count == 0)
			{
				// Electronics
				AddProduct("Laptop", 45000, 100, 1, 1);
				AddProduct("Wireless Mouse", 500, 550, 1, 1);

				// Groceries
				AddProduct("Rice (5kg)", 250, 100, 2, 2);
				AddProduct("Canned Tuna", 35, 800, 2, 2);

				// Beverages
				AddProduct("Mineral Water (1L)", 20, 590, 3, 3);
				AddProduct("Softdrinks (1.5L)", 75, 300, 3, 3);

				// Personal Care
				AddProduct("Shampoo", 120, 500, 4, 4);
				AddProduct("Toothpaste", 85, 900, 4, 4);
				
				// School Supplies
				AddProduct("Notebook", 25, 400, 5, 5);
				AddProduct("Ballpen", 10, 400, 5, 5);
			}
		}

		// ── CATEGORY METHODS ─────────────────────────────────────

		public void AddCategory(string name, string description)
		{
			if (_categories.Exists(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
				throw new InvalidOperationException($"Category '{name}' already exists.");

			_categories.Add(new Category(_categoryIdCounter++, name, description));
		}

		public List<Category> GetAllCategories()
		{
			return new List<Category>(_categories);
		}

		public Category? GetCategoryById(int id)
		{
			return _categories.Find(c => c.Id == id);
		}

		// ── SUPPLIER METHODS ─────────────────────────────────────

		public void AddSupplier(string name, string contactNumber, string email)
		{
			if (_suppliers.Exists(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
				throw new InvalidOperationException($"Supplier '{name}' already exists.");

			_suppliers.Add(new Supplier(_supplierIdCounter++, name, contactNumber, email));
		}

		public List<Supplier> GetAllSuppliers()
		{
			return new List<Supplier>(_suppliers);
		}

		public Supplier? GetSupplierById(int id)
		{
			return _suppliers.Find(s => s.Id == id);
		}

		// ── PRODUCT METHODS ──────────────────────────────────────

		public void AddProduct(string name, decimal price, int quantity,
							   int categoryId, int supplierId)
		{
			if (GetCategoryById(categoryId) == null)
				throw new ArgumentException($"Category ID {categoryId} does not exist.");
			if (GetSupplierById(supplierId) == null)
				throw new ArgumentException($"Supplier ID {supplierId} does not exist.");

			var product = new Product(_productIdCounter++, name, price, quantity,
									  categoryId, supplierId);

			_products.Add(product);

			LogTransaction(product.Id, product.Name, TransactionType.AddProduct,
				quantity,
				$"Product added with stock {quantity}. Threshold: {product.LowStockThreshold}");
		}

		public List<Product> GetAllProducts()
		{
			return new List<Product>(_products);
		}

		public Product? GetProductById(int id)
		{
			return _products.Find(p => p.Id == id);
		}

		public List<Product> SearchProducts(string keyword)
		{
			return _products.FindAll(p =>
				p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
		}

		public void UpdateProduct(int id, string name, decimal price,
								  int categoryId, int supplierId)
		{
			var product = GetProductById(id)
				?? throw new KeyNotFoundException($"Product ID {id} not found.");

			if (GetCategoryById(categoryId) == null)
				throw new ArgumentException($"Category ID {categoryId} does not exist.");
			if (GetSupplierById(supplierId) == null)
				throw new ArgumentException($"Supplier ID {supplierId} does not exist.");

			product.Name = name;
			product.Price = price;
			product.CategoryId = categoryId;
			product.SupplierId = supplierId;

			LogTransaction(product.Id, product.Name, TransactionType.UpdateProduct,
				0, "Product updated.");
		}

		public void DeleteProduct(int id)
		{
			var product = GetProductById(id)
				?? throw new KeyNotFoundException($"Product ID {id} not found.");

			LogTransaction(product.Id, product.Name, TransactionType.DeleteProduct,
				0, "Product deleted.");

			_products.Remove(product);
		}

		public void RestockProduct(int id, int quantity)
		{
			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than zero.");

			var product = GetProductById(id)
				?? throw new KeyNotFoundException($"Product ID {id} not found.");

			product.Quantity += quantity;

			LogTransaction(product.Id, product.Name, TransactionType.Restock,
				quantity, $"Restocked {quantity}. New stock: {product.Quantity}");
		}

		public void DeductStock(int id, int quantity)
		{
			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than zero.");

			var product = GetProductById(id)
				?? throw new KeyNotFoundException($"Product ID {id} not found.");

			if (product.Quantity < quantity)
				throw new InvalidOperationException("Insufficient stock.");

			product.Quantity -= quantity;

			LogTransaction(product.Id, product.Name, TransactionType.Deduction,
				-quantity, $"Deducted {quantity}. Remaining: {product.Quantity}");
		}

		// ── REPORTS ──────────────────────────────────────────────

		public List<TransactionRecord> GetAllTransactions()
		{
			return new List<TransactionRecord>(_transactions);
		}

		public List<Product> GetLowStockProducts()
		{
			return _products.FindAll(p => p.IsLowStock);
		}

		public decimal GetTotalInventoryValue()
		{
			decimal total = 0;
			foreach (var p in _products)
				total += p.TotalValue;
			return total;
		}

		// ── PRIVATE HELPER ───────────────────────────────────────

		private void LogTransaction(int productId, string productName,
			TransactionType type, int qtyChanged, string notes)
		{
			_transactions.Add(new TransactionRecord(
				_transactionIdCounter++, productId, productName,
				type, qtyChanged, _currentUser, notes));
		}
	}
}