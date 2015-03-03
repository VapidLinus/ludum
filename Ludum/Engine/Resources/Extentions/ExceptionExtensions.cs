using System;

namespace Ludum.Engine
{
	public static class ExceptionExtensions
	{
		public static void PrintStackTrace (this Exception e)
		{
			Console.WriteLine(e.ToString());
		}
	}
}