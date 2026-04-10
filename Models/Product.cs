using System;

namespace CLI_Inventory_Management_System.Models
{
	public class Product
	{
		private int _id;
		private string _name;
		private decimal _price;
		private int _quantity;
		private int _initialQuantity;
		private int _lowStockThreshold;

		public int Id
		{
			get { return _id; }
			private set { _id = value; }
		}

		public string Name
		{
			get { return _name; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Product name cannot be empty.");
				_name = value.Trim();
			}
		}

		public decimal Price
		{
			get { return _price; }
			set
			{
				if (value < 0)
					throw new ArgumentException("Price cannot be negative.");
				_price = value;
			}
		}

		public int Quantity
		{
			get { return _quantity; }
			set
			{
				if (value < 0)
					throw new ArgumentException("Quantity cannot be negative.");
				_quantity = value;
			}
		}

		public int InitialQuantity
		{
			get { return _initialQuantity; }
			private set { _initialQuantity = value; }
		}

		public int LowStockThreshold
		{
			get { return _lowStockThreshold; }
			set
			{
				if (value < 0)
					throw new ArgumentException("Low stock threshold cannot be negative.");
				_lowStockThreshold = value;
			}
		}

		public int CategoryId { get; set; }
		public int SupplierId { get; set; }

		public decimal TotalValue => Price * Quantity;

		public bool IsLowStock => Quantity <= LowStockThreshold;

		public string StockStatus
		{
			get
			{
				if (Quantity > LowStockThreshold)
					return "OK";
				else if (Quantity > LowStockThreshold / 2)
					return "LOW STOCK ⚠";
				else
					return "CRITICAL 🔴";
			}
		}

		// Constructor
		public Product(int id, string name, decimal price, int quantity,
					   int categoryId, int supplierId)
		{
			Id = id;
			Name = name;
			Price = price;

			InitialQuantity = quantity;
			Quantity = quantity;

			CategoryId = categoryId;
			SupplierId = supplierId;

			// Default threshold: 50% of initial quantity (minimum 1)
			LowStockThreshold = Math.Max(1, quantity / 2);
		}

		public override string ToString()
		{
			return $"[ID: {Id}] {Name} | Price: {Price:C} | Qty: {Quantity} | " +
				   $"CategoryID: {CategoryId} | SupplierID: {SupplierId} | " +
				   $"Status: {StockStatus} (Threshold: {LowStockThreshold})";
		}
	}
}