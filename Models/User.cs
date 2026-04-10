namespace CLI_Inventory_Management_System.Models
{
    // Enum for user roles
    public enum UserRole
    {
        Admin,
        Staff
    }

    // Model 4: User
    public class User
    {
        // Private fields (Encapsulation)
        private int _id;
        private string _username;
        private string _password;
        private UserRole _role;

        // Properties with access modifiers
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username cannot be empty.");
                _username = value.Trim();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password cannot be empty.");
                _password = value;
            }
        }

        public UserRole Role
        {
            get { return _role; }
            set { _role = value; }
        }

        // Constructor
        public User(int id, string username, string password, UserRole role = UserRole.Staff)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }

        // Method - authenticate user
        public bool Authenticate(string inputPassword)
        {
            return _password == inputPassword;
        }

        // Method
        public override string ToString()
        {
            return $"[ID: {Id}] {Username} | Role: {Role}";
        }
    }
}
