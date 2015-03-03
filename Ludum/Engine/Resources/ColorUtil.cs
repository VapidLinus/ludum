using SFML.Graphics;

namespace Ludum.Engine
{
	public static class ColorUtil
	{
		public static Color Lerp(Color c1, Color c2, double value)
		{
			if (value > 1.0)
				return c2;
			else if (value < 0.0)
				return c1;

			return new Color(
				(byte)(c1.R + (c2.R - c1.R) * value),
				(byte)(c1.G + (c2.G - c1.G) * value),
				(byte)(c1.B + (c2.B - c1.B) * value));
		}

		public static Color Randomize(Color color, int difference)
		{
			return new Color(
				(byte)MathUtil.Clamp(color.R + SRandom.Instance.Next(-difference, difference), 0, 255),
				(byte)MathUtil.Clamp(color.G + SRandom.Instance.Next(-difference, difference), 0, 255),
				(byte)MathUtil.Clamp(color.B + SRandom.Instance.Next(-difference, difference), 0, 255));
		}
	}
}