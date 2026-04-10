namespace CLI_Inventory_Management_System.Models
{
    // Model 2: Supplier
    public class Supplier
    {
        // Private fields (Encapsulation)
        private int _id;
        private string _name;
        private string _contactNumber;
        private string _email;

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
                    throw new ArgumentException("Supplier name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value?.Trim() ?? string.Empty; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value?.Trim() ?? string.Empty; }
        }

        // Constructor
        public Supplier(int id, string name, string contactNumber = "", string email = "")
        {
            Id = id;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
        }

        // Method
        public override string ToString()
        {
            return $"[ID: {Id}] {Name} | Contact: {ContactNumber} | Email: {Email}";
        }
    }
}
