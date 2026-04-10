namespace CLI_Inventory_Management_System.Helpers
{
	public static class Validators
	{
		public static bool IsValidPhone(string phone)
		{
			return phone.Length == 11 &&
				   phone.StartsWith("09") &&
				   phone.All(char.IsDigit) &&
				   !phone.Contains(' ');   // no spaces allowed
		}

		public static bool IsValidEmail(string email)
		{
			return !email.Contains(' ') &&           // no spaces allowed
				   email.EndsWith("@gmail.com") &&
				   email.IndexOf("@") > 0;
		}
	}
}