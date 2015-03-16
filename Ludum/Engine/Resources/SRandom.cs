using System;

namespace Ludum.Engine
{
	public static class SRandom
	{
		public static Random Instance = new Random();

		public static int Range(int min, int max)
		{
			return Instance.Next(min, max);
		}

		public static double Range(double minimum, double maximum)
		{
			return Instance.NextDouble() * (maximum - minimum) + minimum;
		}
	}
}