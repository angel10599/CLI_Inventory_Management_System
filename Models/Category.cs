namespace CLI_Inventory_Management_System.Models
{
    // Model 1: Category
    public class Category
    {
        // Private fields (Encapsulation)
        private int _id;
        private string _name;
        private string _description;

        // Properties with access modifiers
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
                    throw new ArgumentException("Category name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value?.Trim() ?? string.Empty; }
        }

        // Constructor
        public Category(int id, string name, string description = "")
        {
            Id = id;
            Name = name;
            Description = description;
        }

        // Method
        public override string ToString()
        {
            return $"[ID: {Id}] {Name} - {Description}";
        }
    }
}
