using System.Text;

namespace CLI_Inventory_Management_System.Helpers
{
	public static class ConsoleHelper
	{
		// ── GLOBAL WIDTH ─────────────────────────────────────────────────────────

		private const int WIDTH = 115;

		private const char H = '═';
		private const char V = '║';
		private const char TL = '╔';
		private const char TR = '╗';
		private const char BL = '╚';
		private const char BR = '╝';
		private const char ML = '╠';
		private const char MR = '╣';
		private const char TH = '─';

		// ── SHARED TABLE COLUMN DEFINITIONS ──────────────────────────────────────
		// Single source of truth for every table in the app.
		// Header and rows always reference the same array — widths can never mismatch.

		public static readonly (string label, int width)[] ColProducts =
	{
	("ID",       6),
	("Name",    35),
	("Price",   14),
	("Qty",      8),
	("Cat ID",   10),
	("Sup ID",   10),
	("Status",   12),
};

		public static readonly (string label, int width)[] ColCategories =
	{
	("ID",          10),
	("Name",        24), 
    ("Description", 73),
};

		public static readonly (string label, int width)[] ColSuppliers =
		{
	("ID",      10),
	("Name",    24),
	("Contact", 15),
	("Email",   55),
};

		public static readonly (string label, int width)[] ColTransactions =
	{
	("ID",         6),
	("Timestamp", 22),
	("Type",      16),
	("Product",   30),
	("Qty",        8),
	("By",        14),
};

		public static readonly (string label, int width)[] ColLowStock =
	{
	("ID",         6),
	("Name",      40),
	("Qty",        8),
	("Threshold", 12),
	("Price",     35),
};

		public static readonly (string label, int width)[] ColInventoryValue =
	{
	("Name",        40),
	("Price",       16),
	("Qty",          8),
	("Total Value",  40),
};

		// ── HEADER / FOOTER ──────────────────────────────────────────────────────

		public static void PrintHeader(string title, string? subtitle = null)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"  {TL}{new string(H, WIDTH)}{TR}");

			string centeredTitle = title.ToUpper().PadLeft((WIDTH + title.Length) / 2).PadRight(WIDTH);
			Console.WriteLine($"  {V}{centeredTitle}{V}");

			if (subtitle != null)
			{
				string centeredSub = subtitle.PadLeft((WIDTH + subtitle.Length) / 2).PadRight(WIDTH);
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.WriteLine($"  {V}{centeredSub}{V}");
			}

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"  {ML}{new string(H, WIDTH)}{MR}");
			Console.ResetColor();
		}

		public static void PrintFooter()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"  {BL}{new string(H, WIDTH)}{BR}");
			Console.ResetColor();
		}

		public static void PrintDivider()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"  {new string(TH, WIDTH + 2)}");
			Console.ResetColor();
		}


		// ── LOGIN SCREEN ─────────────────────────────────────────────────────────

		public static void PrintLoginScreen()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(@"  ╔══════════════════════════════════════════════════════════════════════════════════════════╗");
			Console.WriteLine(@"  ║                                                                                          ║");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(@"  ║                      I N V E N T O R Y   M A N A G E R                                   ║");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(@"  ║                          CLI-Based Management System                                     ║");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(@"  ║                                                                                          ║");
			Console.WriteLine(@"  ╠══════════════════════════════════════════════════════════════════════════════════════════╣");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(@"  ║                         Please log in to continue                                        ║");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(@"  ╚══════════════════════════════════════════════════════════════════════════════════════════╝");
			Console.ResetColor();
			Console.WriteLine();
		}

		// ── MENU ITEM HELPERS ────────────────────────────────────────────────────

		public static void PrintMenuOption(string key, string label, string? note = null)
		{
			string keyStr = $"[{key}]";
			string labelStr = $"  {label}";
			string noteStr = note != null ? $"  {note}" : "";

			int used = 2 + keyStr.Length + labelStr.Length + noteStr.Length;
			int pad = Math.Max(0, WIDTH - used);

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.Write($"  {V}  ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(keyStr);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(labelStr);
			if (note != null)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write(noteStr);
			}
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(new string(' ', pad) + V);
			Console.ResetColor();
		}

		public static void PrintMenuSeparator()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine($"  {ML}{new string(TH, WIDTH)}{MR}");
			Console.ResetColor();
		}

		public static void PrintMenuFooter()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine($"  {BL}{new string(H, WIDTH)}{BR}");
			Console.ResetColor();
		}

		// ── STATUS MESSAGES ──────────────────────────────────────────────────────

		public static void PrintSuccess(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"\n  ✔  {message}");
			Console.ResetColor();
		}

		public static void PrintError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"\n  ✘  {message}");
			Console.ResetColor();
		}

		public static void PrintWarning(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"\n  ⚠  {message}");
			Console.ResetColor();
		}

		public static void PrintInfo(string message)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"\n  ℹ  {message}");
			Console.ResetColor();
		}

		// ── TABLE HELPERS ────────────────────────────────────────────────────────

		public static void PrintTableHeader(params (string label, int width)[] columns)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;

			// Top Border: ┌──────┬──────────┐
			Console.Write("  ┌");
			for (int i = 0; i < columns.Length; i++)
			{
				Console.Write(new string('─', columns[i].width + 2));
				if (i < columns.Length - 1) Console.Write("┬");
			}
			Console.WriteLine("┐");

			// Header Content: │ ID   │ NAME     │
			Console.Write("  │");
			foreach (var (label, width) in columns)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				// Ensure we use standard spaces for padding
				string text = label.ToUpper();
				string padded = text.PadRight(width);
				Console.Write($" {padded} ");

				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.Write("│");
			}
			Console.WriteLine();

			// Divider: ├──────┼──────────┤
			Console.Write("  ├");
			for (int i = 0; i < columns.Length; i++)
			{
				Console.Write(new string('─', columns[i].width + 2));
				if (i < columns.Length - 1) Console.Write("┼");
			}
			Console.WriteLine("┤");
			Console.ResetColor();
		}

		public static void PrintTableRow(bool highlight, params (string value, int width)[] cells)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.Write("  │");
			foreach (var (value, width) in cells)
			{
				Console.ForegroundColor = highlight ? ConsoleColor.Yellow : ConsoleColor.White;

				// Clean the string of any potential non-breaking space characters
				string cleanValue = value.Replace('\u00A0', ' ').Trim();

				string display = cleanValue.Length > width
					? cleanValue.Substring(0, width - 1) + "…"
					: cleanValue.PadRight(width);

				Console.Write($" {display} ");

				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.Write("│");
			}
			Console.WriteLine();
			Console.ResetColor();
		}

		public static void PrintTableFooter(params (string label, int width)[] columns)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.Write("  └");
			for (int i = 0; i < columns.Length; i++)
			{
				Console.Write(new string('─', columns[i].width + 2));
				if (i < columns.Length - 1) Console.Write("┴");
			}
			Console.WriteLine("┘");
			Console.ResetColor();
		}

		private static int GetTableWidth((string label, int width)[] columns)
{
    // Sum of column widths + 2 spaces per column + inner borders + outer borders
    return columns.Sum(c => c.width + 2) + (columns.Length - 1);
}
		// ── INPUT HELPERS ────────────────────────────────────────────────────────

		public static string ReadNonEmpty(string prompt)
		{
			string? input;
			do
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write($"  › ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write($"{prompt}: ");
				Console.ForegroundColor = ConsoleColor.White;
				input = Console.ReadLine()?.Trim();
				Console.ResetColor();

				if (string.IsNullOrWhiteSpace(input))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("  ✘  This field cannot be empty.");
					Console.ResetColor();
				}
			} while (string.IsNullOrWhiteSpace(input));

			return input;
		}

		public static string ReadPassword(string prompt)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write($"  › ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"{prompt}: ");
			Console.ResetColor();

			var sb = new StringBuilder();
			ConsoleKeyInfo key;

			do
			{
				key = Console.ReadKey(intercept: true);
				if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
				{
					sb.Remove(sb.Length - 1, 1);
					Console.Write("\b \b");
				}
				else if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
				{
					sb.Append(key.KeyChar);
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write("•");
					Console.ResetColor();
				}
			} while (key.Key != ConsoleKey.Enter);

			Console.WriteLine();
			return sb.ToString();
		}

		public static int ReadInt(string prompt, int min = 0, int max = int.MaxValue)
		{
			int result;
			while (true)
			{
				string raw = ReadNonEmpty(prompt + $" ({min}–{max})");
				if (int.TryParse(raw, out result) && result >= min && result <= max)
					return result;

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"  ✘  Please enter a number between {min} and {max}.");
				Console.ResetColor();
			}
		}

		public static decimal ReadDecimal(string prompt, decimal min = 0)
		{
			decimal result;
			while (true)
			{
				string raw = ReadNonEmpty(prompt);
				if (decimal.TryParse(raw, out result) && result >= min)
					return result;

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"  ✘  Please enter a valid number (min: {min}).");
				Console.ResetColor();
			}
		}

		public static bool Confirm(string prompt)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write($"\n  ⚠  {prompt} ");
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("[y/N]: ");
			Console.ForegroundColor = ConsoleColor.White;
			string? response = Console.ReadLine()?.Trim().ToLower();
			Console.ResetColor();
			return response == "y" || response == "yes";
		}

		public static void Pause()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("\n  Press any key to continue...");
			Console.ResetColor();
			Console.ReadKey(true);
		}
	}
}