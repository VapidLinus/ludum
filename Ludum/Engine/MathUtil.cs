using System;

namespace Ludum.Engine
{
	public static class MathUtil
	{
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
	}
}