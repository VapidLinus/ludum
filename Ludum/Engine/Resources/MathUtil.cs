namespace Ludum.Engine
{
	public static class MathUtil
	{
		public static byte Clamp(byte value, byte min, byte max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		public static int Clamp(int value, int min, int max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		public static double Clamp(double value, double min, double max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		public static double Lerp(double from, double to, double value)
		{
			if (value < 0.0)
				return from;
			else if (value > 1.0)
				return to;
			return (to - from) * value + from;
		}
	}
}