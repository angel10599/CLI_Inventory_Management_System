using CLI_Inventory_Management_System.Helpers;
using CLI_Inventory_Management_System.Services;

namespace CLI_Inventory_Management_System
{
	class Program
	{
		private static InventoryService _inventory = new InventoryService();
		private static AuthService _auth = new AuthService();

		static void Main(string[] args)
		{
			Console.Title = "IMS — Inventory Management System";
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			if (!HandleLogin())
			{
				ConsoleHelper.PrintError("Access denied. Exiting program.");
				ConsoleHelper.Pause();
				return;
			}

			_inventory.SeedInitialData();

			_inventory.SetCurrentUser(_auth.CurrentUsername);
			RunMainMenu();
		}

		// ── LOGIN ────────────────────────────────────────────────────────────────

		static bool HandleLogin()
		{
			int maxAttempts = 3;

			for (int attempt = 1; attempt <= maxAttempts; attempt++)
			{
				ConsoleHelper.PrintLoginScreen();

				if (attempt > 1)
				{
					ConsoleHelper.PrintWarning($"Attempt {attempt} of {maxAttempts}");
					Console.WriteLine();
				}

				try
				{
					string username = ConsoleHelper.ReadNonEmpty("Username");
					string password = ConsoleHelper.ReadPassword("Password");

					if (_auth.Login(username, password))
					{
						Console.Clear();
						ConsoleHelper.PrintHeader("ACCESS GRANTED", $"Welcome back, {_auth.CurrentUsername}  |  Role: {_auth.CurrentUser!.Role}");
						ConsoleHelper.PrintSuccess("Login successful. Redirecting to dashboard...");
						ConsoleHelper.PrintFooter();
						Thread.Sleep(1200);
						return true;
					}
					else
					{
						ConsoleHelper.PrintError("Invalid username or password.");
						ConsoleHelper.Pause();
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}

			ConsoleHelper.PrintError("Too many failed attempts. Access denied.");
			return false;
		}

		// ── MAIN MENU ────────────────────────────────────────────────────────────

		static void RunMainMenu()
		{
			bool running = true;

			while (running)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("INVENTORY MANAGEMENT SYSTEM",
					$"Logged in as: {_auth.CurrentUsername}  |  {DateTime.Now:dddd, MMM dd yyyy  HH:mm}");

				ConsoleHelper.PrintMenuOption("1", "Category Management");
				ConsoleHelper.PrintMenuOption("2", "Supplier Management");
				ConsoleHelper.PrintMenuOption("3", "Product Management");
				ConsoleHelper.PrintMenuOption("4", "Stock Operations");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("5", "Reports & Analytics");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Logout & Exit");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 5);

				switch (choice)
				{
					case 1: CategoryMenu(); break;
					case 2: SupplierMenu(); break;
					case 3: ProductMenu(); break;
					case 4: StockMenu(); break;
					case 5: ReportsMenu(); break;
					case 0:
						if (ConsoleHelper.Confirm("Are you sure you want to logout and exit?"))
						{
							Console.Clear();
							ConsoleHelper.PrintHeader("GOODBYE", $"Session ended for {_auth.CurrentUsername}");
							ConsoleHelper.PrintSuccess("You have been logged out successfully.");
							ConsoleHelper.PrintFooter();
							Thread.Sleep(1000);
							running = false;
						}
						break;
				}
			}
		}

		// ── CATEGORY MENU ────────────────────────────────────────────────────────

		static void CategoryMenu()
		{
			bool back = false;

			while (!back)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("CATEGORY MANAGEMENT");
				ConsoleHelper.PrintMenuOption("1", "Add Category");
				ConsoleHelper.PrintMenuOption("2", "View All Categories");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Back to Main Menu");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 2);

				try
				{
					switch (choice)
					{
						case 1:
							Console.Clear();
							ConsoleHelper.PrintHeader("ADD CATEGORY");
							string name = ConsoleHelper.ReadNonEmpty("Category Name");
							string desc = ConsoleHelper.ReadNonEmpty("Description");
							_inventory.AddCategory(name, desc);
							ConsoleHelper.PrintSuccess($"Category '{name}' added successfully.");
							ConsoleHelper.Pause();
							break;

						case 2:
							Console.Clear();
							ConsoleHelper.PrintHeader("ALL CATEGORIES");
							var cats = _inventory.GetAllCategories();
							if (cats.Count == 0)
							{
								ConsoleHelper.PrintWarning("No categories found. Please add one first.");
							}
							else
							{
								// Use ColCategories for both header and every row
								ConsoleHelper.PrintTableHeader(ConsoleHelper.ColCategories);
								foreach (var c in cats)
									ConsoleHelper.PrintTableRow(false,
										(c.Id.ToString(), ConsoleHelper.ColCategories[0].width),
										(c.Name, ConsoleHelper.ColCategories[1].width),
										(c.Description, ConsoleHelper.ColCategories[2].width));
								ConsoleHelper.PrintTableFooter(ConsoleHelper.ColCategories);
							}
							ConsoleHelper.Pause();
							break;

						case 0:
							back = true;
							break;
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}
		}

		// ── SUPPLIER MENU ────────────────────────────────────────────────────────

		static void SupplierMenu()
		{
			bool back = false;

			while (!back)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("SUPPLIER MANAGEMENT");
				ConsoleHelper.PrintMenuOption("1", "Add Supplier");
				ConsoleHelper.PrintMenuOption("2", "View All Suppliers");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Back to Main Menu");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 2);

				try
				{
					switch (choice)
					{
						case 1:
							Console.Clear();
							ConsoleHelper.PrintHeader("ADD SUPPLIER");
							string name = ConsoleHelper.ReadNonEmpty("Supplier Name");

							string contact;
							do
							{
								contact = ConsoleHelper.ReadNonEmpty("Contact Number (09XXXXXXXXX)");
								if (!Validators.IsValidPhone(contact))
									ConsoleHelper.PrintError("Invalid phone number (11 digits, starts with 09, no spaces).");
							} while (!Validators.IsValidPhone(contact));

							string email;
							do
							{
								email = ConsoleHelper.ReadNonEmpty("Email (@gmail.com only)");
								if (!Validators.IsValidEmail(email))
									ConsoleHelper.PrintError("Invalid email. Must end with @gmail.com.");
							} while (!Validators.IsValidEmail(email));

							_inventory.AddSupplier(name, contact, email);
							ConsoleHelper.PrintSuccess($"Supplier '{name}' added successfully.");
							ConsoleHelper.Pause();
							break;

						case 2:
							Console.Clear();
							ConsoleHelper.PrintHeader("ALL SUPPLIERS");
							var suppliers = _inventory.GetAllSuppliers();
							if (suppliers.Count == 0)
							{
								ConsoleHelper.PrintWarning("No suppliers found. Please add one first.");
							}
							else
							{
								// Use ColSuppliers for both header and every row
								ConsoleHelper.PrintTableHeader(ConsoleHelper.ColSuppliers);
								foreach (var s in suppliers)
									ConsoleHelper.PrintTableRow(false,
										(s.Id.ToString(), ConsoleHelper.ColSuppliers[0].width),
										(s.Name, ConsoleHelper.ColSuppliers[1].width),
										(s.ContactNumber, ConsoleHelper.ColSuppliers[2].width),
										(s.Email, ConsoleHelper.ColSuppliers[3].width));
								ConsoleHelper.PrintTableFooter(ConsoleHelper.ColSuppliers);
							}
							ConsoleHelper.Pause();
							break;

						case 0:
							back = true;
							break;
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}
		}

		// ── PRODUCT MENU ─────────────────────────────────────────────────────────

		static void ProductMenu()
		{
			bool back = false;

			while (!back)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("PRODUCT MANAGEMENT");
				ConsoleHelper.PrintMenuOption("1", "Add Product");
				ConsoleHelper.PrintMenuOption("2", "View All Products");
				ConsoleHelper.PrintMenuOption("3", "Search Product");
				ConsoleHelper.PrintMenuOption("4", "Update Product");
				ConsoleHelper.PrintMenuOption("5", "Delete Product");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Back to Main Menu");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 5);

				try
				{
					switch (choice)
					{
						case 1: AddProduct(); break;
						case 2: ViewAllProducts(); ConsoleHelper.Pause(); break;
						case 3: SearchProduct(); break;
						case 4: UpdateProduct(); break;
						case 5: DeleteProduct(); break;
						case 0: back = true; break;
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}
		}

		static void AddProduct()
		{
			Console.Clear();
			ConsoleHelper.PrintHeader("ADD PRODUCT");
			ShowCategories();
			ShowSuppliers();
			ConsoleHelper.PrintDivider();

			string name = ConsoleHelper.ReadNonEmpty("Product Name");
			decimal price = ConsoleHelper.ReadDecimal("Price", 0);
			int qty = ConsoleHelper.ReadInt("Initial Quantity", 0);
			int catId;
			while (true)
			{
				catId = ConsoleHelper.ReadInt("Category ID", 1);
				if (_inventory.GetAllCategories().Any(c => c.Id == catId)) break;
				ConsoleHelper.PrintError($"Category ID {catId} does not exist. Please choose from the list above.");
			}

			int supId;
			while (true)
			{
				supId = ConsoleHelper.ReadInt("Supplier ID", 1);
				if (_inventory.GetAllSuppliers().Any(s => s.Id == supId)) break;
				ConsoleHelper.PrintError($"Supplier ID {supId} does not exist. Please choose from the list above.");
			}

			_inventory.AddProduct(name, price, qty, catId, supId);
			ConsoleHelper.PrintInfo($"Low stock threshold auto-set to {Math.Max(1, qty / 2)} (50% of initial quantity, min: 1).");
			ConsoleHelper.PrintSuccess($"Product '{name}' added successfully.");
			ConsoleHelper.Pause();
		}

		static void ViewAllProducts()
		{
			Console.Clear();
			ConsoleHelper.PrintHeader("ALL PRODUCTS");
			var products = _inventory.GetAllProducts();

			if (products.Count == 0)
			{
				ConsoleHelper.PrintWarning("No products found.");
				return;
			}

			// ColProducts drives both header and every row — widths are identical by construction
			var cols = ConsoleHelper.ColProducts;
			ConsoleHelper.PrintTableHeader(cols);
			foreach (var p in products)
			{
				bool low = p.IsLowStock;
				ConsoleHelper.PrintTableRow(low,
					(p.Id.ToString(), cols[0].width),
					(p.Name, cols[1].width),
					(p.Price.ToString("C"), cols[2].width),
					(p.Quantity.ToString(), cols[3].width),
					(p.CategoryId.ToString(), cols[4].width),
					(p.SupplierId.ToString(), cols[5].width),
					(p.StockStatus, cols[6].width));
			}
			ConsoleHelper.PrintTableFooter(cols);

			int lowCount = products.Count(p => p.IsLowStock);
			if (lowCount > 0)
				ConsoleHelper.PrintWarning($"{lowCount} product(s) are below their low stock threshold.");
		}

		static void SearchProduct()
		{
			Console.Clear();
			ConsoleHelper.PrintHeader("SEARCH PRODUCT");
			string keyword = ConsoleHelper.ReadNonEmpty("Enter product name or keyword only");
			var results = _inventory.SearchProducts(keyword);

			if (results.Count == 0)
			{
				ConsoleHelper.PrintWarning($"No products found matching '{keyword}'.");
			}
			else
			{
				ConsoleHelper.PrintInfo($"Found {results.Count} result(s) for \"{keyword}\":");
				// Reuse ColProducts so search results look identical to the main product table
				var cols = ConsoleHelper.ColProducts;
				ConsoleHelper.PrintTableHeader(cols);
				foreach (var p in results)
				{
					bool low = p.IsLowStock;
					ConsoleHelper.PrintTableRow(low,
						(p.Id.ToString(), cols[0].width),
						(p.Name, cols[1].width),
						(p.Price.ToString("C"), cols[2].width),
						(p.Quantity.ToString(), cols[3].width),
						(p.CategoryId.ToString(), cols[4].width),
						(p.SupplierId.ToString(), cols[5].width),
						(p.StockStatus, cols[6].width));
				}
				ConsoleHelper.PrintTableFooter(cols);
			}

			ConsoleHelper.Pause();
		}

		static void UpdateProduct()
		{
			Console.Clear();
			ConsoleHelper.PrintHeader("UPDATE PRODUCT");
			ViewAllProducts();

			if (_inventory.GetAllProducts().Count == 0) { ConsoleHelper.Pause(); return; }

			int id = ConsoleHelper.ReadInt("Enter Product ID to update", 1);
			var product = _inventory.GetProductById(id);

			if (product == null)
			{
				ConsoleHelper.PrintError($"Product with ID {id} not found.");
				ConsoleHelper.Pause();
				return;
			}

			ConsoleHelper.PrintInfo($"Editing: [{product.Id}] {product.Name}");
			ConsoleHelper.PrintDivider();
			ShowCategories();
			ShowSuppliers();
			ConsoleHelper.PrintDivider();

			string name = ConsoleHelper.ReadNonEmpty($"Name [{product.Name}]");
			decimal price = ConsoleHelper.ReadDecimal($"Price [current: {product.Price:C}]", 0);
			int catId;
			while (true)
			{
				catId = ConsoleHelper.ReadInt("Category ID", 1);
				if (_inventory.GetAllCategories().Any(c => c.Id == catId)) break;
				ConsoleHelper.PrintError($"Category ID {catId} does not exist. Please choose from the list above.");
			}

			int supId;
			while (true)
			{
				supId = ConsoleHelper.ReadInt("Supplier ID", 1);
				if (_inventory.GetAllSuppliers().Any(s => s.Id == supId)) break;
				ConsoleHelper.PrintError($"Supplier ID {supId} does not exist. Please choose from the list above.");
			}

			_inventory.UpdateProduct(id, name, price, catId, supId);
			ConsoleHelper.PrintSuccess("Product updated successfully.");
			ConsoleHelper.Pause();
		}

		static void DeleteProduct()
		{
			Console.Clear();
			ConsoleHelper.PrintHeader("DELETE PRODUCT");
			ViewAllProducts();

			if (_inventory.GetAllProducts().Count == 0) { ConsoleHelper.Pause(); return; }

			int id = ConsoleHelper.ReadInt("Enter Product ID to delete", 1);
			var product = _inventory.GetProductById(id);

			if (product == null)
			{
				ConsoleHelper.PrintError($"Product with ID {id} not found.");
				ConsoleHelper.Pause();
				return;
			}

			if (ConsoleHelper.Confirm($"Permanently delete '{product.Name}'? This cannot be undone."))
			{
				_inventory.DeleteProduct(id);
				ConsoleHelper.PrintSuccess($"'{product.Name}' has been deleted.");
			}
			else
			{
				ConsoleHelper.PrintInfo("Delete cancelled.");
			}

			ConsoleHelper.Pause();
		}

		// ── STOCK MENU ───────────────────────────────────────────────────────────

		static void StockMenu()
		{
			bool back = false;

			while (!back)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("STOCK OPERATIONS");
				ConsoleHelper.PrintMenuOption("1", "Restock Product", "Add units to existing stock");
				ConsoleHelper.PrintMenuOption("2", "Deduct Stock", "Remove units from stock");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Back to Main Menu");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 2);

				try
				{
					switch (choice)
					{
						case 1:
							Console.Clear();
							ConsoleHelper.PrintHeader("RESTOCK PRODUCT");
							ViewAllProducts();
							if (_inventory.GetAllProducts().Count == 0) { ConsoleHelper.Pause(); break; }
							int restockId = ConsoleHelper.ReadInt("Enter Product ID", 1);
							int restockQty = ConsoleHelper.ReadInt("Quantity to add", 1);
							_inventory.RestockProduct(restockId, restockQty);
							ConsoleHelper.PrintSuccess($"Successfully added {restockQty} units to stock.");
							ConsoleHelper.Pause();
							break;

						case 2:
							Console.Clear();
							ConsoleHelper.PrintHeader("DEDUCT STOCK");
							ViewAllProducts();
							if (_inventory.GetAllProducts().Count == 0) { ConsoleHelper.Pause(); break; }
							int deductId = ConsoleHelper.ReadInt("Enter Product ID", 1);
							int deductQty = ConsoleHelper.ReadInt("Quantity to deduct", 1);
							_inventory.DeductStock(deductId, deductQty);
							ConsoleHelper.PrintSuccess($"Successfully deducted {deductQty} units from stock.");
							ConsoleHelper.Pause();
							break;

						case 0:
							back = true;
							break;
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}
		}

		// ── REPORTS MENU ─────────────────────────────────────────────────────────

		static void ReportsMenu()
		{
			bool back = false;

			while (!back)
			{
				Console.Clear();
				ConsoleHelper.PrintHeader("REPORTS & ANALYTICS");
				ConsoleHelper.PrintMenuOption("1", "Transaction History", "Full audit log");
				ConsoleHelper.PrintMenuOption("2", "Low-Stock Items", "Products below threshold");
				ConsoleHelper.PrintMenuOption("3", "Total Inventory Value", "Computed stock value");
				ConsoleHelper.PrintMenuSeparator();
				ConsoleHelper.PrintMenuOption("0", "Back to Main Menu");
				ConsoleHelper.PrintMenuFooter();

				int choice = ConsoleHelper.ReadInt("Select an option", 0, 3);

				try
				{
					switch (choice)
					{
						case 1:
							Console.Clear();
							ConsoleHelper.PrintHeader("TRANSACTION HISTORY");
							var transactions = _inventory.GetAllTransactions();
							if (transactions.Count == 0)
							{
								ConsoleHelper.PrintWarning("No transactions recorded yet.");
							}
							else
							{
								var cols = ConsoleHelper.ColTransactions;
								ConsoleHelper.PrintTableHeader(cols);
								foreach (var t in transactions)
									ConsoleHelper.PrintTableRow(false,
										(t.Id.ToString(), cols[0].width),
										(t.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), cols[1].width),
										(t.Type.ToString(), cols[2].width),
										(t.ProductName, cols[3].width),
										(t.QuantityChanged == 0 ? "N/A" : t.QuantityChanged.ToString("+#;-#;0"), cols[4].width),
										(t.PerformedBy, cols[5].width));
								ConsoleHelper.PrintTableFooter(cols);
								ConsoleHelper.PrintInfo($"Total records: {transactions.Count}");
							}
							ConsoleHelper.Pause();
							break;

						case 2:
							Console.Clear();
							ConsoleHelper.PrintHeader("LOW-STOCK ITEMS");
							var lowStock = _inventory.GetLowStockProducts();
							if (lowStock.Count == 0)
							{
								ConsoleHelper.PrintSuccess("All products have sufficient stock.");
							}
							else
							{
								ConsoleHelper.PrintWarning($"{lowStock.Count} product(s) below threshold:");
								var cols = ConsoleHelper.ColLowStock;
								ConsoleHelper.PrintTableHeader(cols);
								foreach (var p in lowStock)
									ConsoleHelper.PrintTableRow(true,
										(p.Id.ToString(), cols[0].width),
										(p.Name, cols[1].width),
										(p.Quantity.ToString(), cols[2].width),
										(p.LowStockThreshold.ToString(), cols[3].width),
										(p.Price.ToString("C"), cols[4].width));
								ConsoleHelper.PrintTableFooter(cols);
							}
							ConsoleHelper.Pause();
							break;

						case 3:
							Console.Clear();
							ConsoleHelper.PrintHeader("TOTAL INVENTORY VALUE");
							var allProducts = _inventory.GetAllProducts();
							decimal total = _inventory.GetTotalInventoryValue();
							var cols2 = ConsoleHelper.ColInventoryValue;
							ConsoleHelper.PrintTableHeader(cols2);
							foreach (var p in allProducts)
								ConsoleHelper.PrintTableRow(false,
									(p.Name, cols2[0].width),
									(p.Price.ToString("C"), cols2[1].width),
									(p.Quantity.ToString(), cols2[2].width),
									(p.TotalValue.ToString("C"), cols2[3].width));
							ConsoleHelper.PrintTableFooter(cols2);
							ConsoleHelper.PrintInfo($"Grand Total Inventory Value: {total:C}");
							ConsoleHelper.Pause();
							break;

						case 0:
							back = true;
							break;
					}
				}
				catch (Exception ex)
				{
					ConsoleHelper.PrintError(ex.Message);
					ConsoleHelper.Pause();
				}
			}
		}

		// ── DISPLAY HELPERS ──────────────────────────────────────────────────────

		static void ShowCategories()
		{
			var cats = _inventory.GetAllCategories();
			if (cats.Count == 0)
			{
				ConsoleHelper.PrintWarning("No categories available. Please add a category first.");
				return;
			}
			Console.WriteLine();
			var cols = ConsoleHelper.ColCategories;
			ConsoleHelper.PrintTableHeader(cols);
			foreach (var c in cats)
				ConsoleHelper.PrintTableRow(false,
					(c.Id.ToString(), cols[0].width),
					(c.Name, cols[1].width),
					(c.Description, cols[2].width));
			ConsoleHelper.PrintTableFooter(cols);
		}

		static void ShowSuppliers()
		{
			var sups = _inventory.GetAllSuppliers();
			if (sups.Count == 0)
			{
				ConsoleHelper.PrintWarning("No suppliers available. Please add a supplier first.");
				return;
			}
			Console.WriteLine();
			var cols = ConsoleHelper.ColSuppliers;
			ConsoleHelper.PrintTableHeader(cols);
			foreach (var s in sups)
				ConsoleHelper.PrintTableRow(false,
					(s.Id.ToString(), cols[0].width),
					(s.Name, cols[1].width),
					(s.ContactNumber, cols[2].width),
					(s.Email, cols[3].width));
			ConsoleHelper.PrintTableFooter(cols);
		}
	}
}