using CLI_Inventory_Management_System.Models;

namespace CLI_Inventory_Management_System.Services
{
    public class AuthService
    {
        // List<T> storage only - no database
        private List<User> _users = new List<User>();
        private int _idCounter = 1;
        private User? _currentUser;

        public User? CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;
        public string CurrentUsername => _currentUser?.Username ?? "Unknown";

        // Constructor - seed default accounts
        public AuthService()
        {
            _users.Add(new User(_idCounter++, "admin", "admin123", UserRole.Admin));
            _users.Add(new User(_idCounter++, "staff", "staff123", UserRole.Staff));
        }

        // Method - login
        public bool Login(string username, string password)
        {
            var user = _users.Find(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user != null && user.Authenticate(password))
            {
                _currentUser = user;
                return true;
            }
            return false;
        }

        // Method - logout
        public void Logout()
        {
            _currentUser = null;
        }
    }
}
