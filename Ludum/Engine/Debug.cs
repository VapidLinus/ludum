using System;

namespace Ludum.Engine
{
	public static class Debug
	{
		public static void Log(string message) { Log(message, null); }
		public static void Log(string message, params object[] format)
		{
			LogColor(message, ConsoleColor.Gray, ConsoleColor.Black, format);
		}

		public static void LogImportant(string message) { LogImportant(message, null); }
		public static void LogImportant(string message, params object[] format)
		{
			LogColor(message, ConsoleColor.Blue, ConsoleColor.Black, format);
		}

		public static void LogWarning(string message) { LogWarning(message, null); }
		public static void LogWarning(string message, params object[] format)
		{
			LogColor(message, ConsoleColor.Yellow, ConsoleColor.Black, format);
		}

		public static void LogError(string message) { LogError(message, null); }
		public static void LogError(string message, params object[] format)
		{
			LogColor(message, ConsoleColor.Black, ConsoleColor.Red, format);
		}

		public static void LogColor(string message, ConsoleColor foreground, ConsoleColor background, params object[] format)
		{
			// Store old colour
			var oldForeground = Console.ForegroundColor;
			var oldBackground = Console.BackgroundColor;

			// Set new colour
			Console.ForegroundColor = foreground;
			Console.BackgroundColor = background;

			// Log
			Console.WriteLine(message, format);

			// Reset colour
			Console.ForegroundColor = oldForeground;
			Console.BackgroundColor = oldBackground;
		}
	}
}
