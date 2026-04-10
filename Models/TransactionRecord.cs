namespace CLI_Inventory_Management_System.Models
{
    // Enum for transaction types
    public enum TransactionType
    {
        AddProduct,
        UpdateProduct,
        DeleteProduct,
        Restock,
        Deduction
    }

    // Model 5: TransactionRecord
    public class TransactionRecord
    {
        // Private fields (Encapsulation)
        private int _id;
        private int _productId;
        private string _productName;
        private TransactionType _type;
        private int _quantityChanged;
        private string _performedBy;
        private DateTime _timestamp;
        private string _notes;

        // Properties with access modifiers
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public int ProductId
        {
            get { return _productId; }
            private set { _productId = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            private set { _productName = value; }
        }

        public TransactionType Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        public int QuantityChanged
        {
            get { return _quantityChanged; }
            private set { _quantityChanged = value; }
        }

        public string PerformedBy
        {
            get { return _performedBy; }
            private set { _performedBy = value; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            private set { _timestamp = value; }
        }

        public string Notes
        {
            get { return _notes; }
            private set { _notes = value; }
        }

        // Constructor
        public TransactionRecord(int id, int productId, string productName,
            TransactionType type, int quantityChanged, string performedBy, string notes = "")
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Type = type;
            QuantityChanged = quantityChanged;
            PerformedBy = performedBy;
            Timestamp = DateTime.Now;
            Notes = notes;
        }

        // Method
        public override string ToString()
        {
            string qtyDisplay = QuantityChanged == 0 ? "N/A" : QuantityChanged.ToString("+#;-#;0");
            return $"[{Id}] {Timestamp:yyyy-MM-dd HH:mm:ss} | {Type,-15} | " +
                   $"Product: {ProductName} (ID:{ProductId}) | " +
                   $"Qty Change: {qtyDisplay} | By: {PerformedBy} | {Notes}";
        }
    }
}
