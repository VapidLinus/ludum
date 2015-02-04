using System;
using SFML.Window;

namespace Ludum.Engine
{
	public struct Vector2
	{
		public double x;
		public double y;

		/// <summary>
		/// Creatures a 2D vector with given coordinates.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector2(double x = 0, double y = 0)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Normalizes the vector.
		/// </summary>
		public void Normalize()
		{
			double magnitude = Magnitude;
			if (magnitude == 0)
			{
				x = y = 0;
			}
			else
			{
				x /= magnitude;
				y /= magnitude;
			}
		}

		public Vector2 SnapTo90
		{
			get
			{
				if (Math.Abs(x) >= Math.Abs(y))
					return x >= 0 ? Right : Left;
				else
					return y >= 0 ? Up : Down;
			}
		}

		/// <summary>
		/// Returns whether the Vector is normalized, without a Sqrt call
		/// </summary>
		public bool IsNormalized
		{
			get
			{
				const double min = 1 - 1e-15;
				const double max = 1 + 1e-15;

				double len = SquareMagnitude;
				return (len >= min && len <= max);
			}
		}

		/// <summary>
		/// Gets vector's magnitude.
		/// </summary>
		public double Magnitude
		{
			get { return Math.Sqrt(SquareMagnitude); }
		}

		/// <summary>
		/// Gets vector's magnitude^2.
		/// </summary>
		public double SquareMagnitude
		{
			get { return x * x + y * y; }
		}

		/// <summary>
		/// Returns a new normalized instance of the vector.
		/// </summary>
		public Vector2 Normalized
		{
			get
			{
				var normalized = new Vector2(x, y);
				normalized.Normalize();
				return normalized;
			}
		}

		/// <summary>
		/// Converts the vector to an angle
		/// </summary>
		/// <param name="vector">Vector to convert</param>
		/// <returns>Angle from vector</returns>
		public double ToAngle(Vector2 vector)
		{
			return Math.Atan2(vector.y, vector.x);
		}

		#region Object
		public override bool Equals(object obj)
		{
			return obj is Vector2 && obj.Equals(this);
		}

		public bool Equals(Vector2 other)
		{
			if (ReferenceEquals(other, null)) return false;
			return x.Equals(other.x) && y.Equals(other.y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public override string ToString()
		{
			return x + ", " + y;
		}
		#endregion
		#region Operators
		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x + v2.x, v1.y + v2.y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x - v2.x, v1.y - v2.y);
		}

		public static Vector2 operator -(Vector2 v1)
		{
			return new Vector2(-v1.x, -v1.y);
		}

		public static Vector2 operator *(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x * v2.x, v1.y * v2.y);
		}

		public static Vector2 operator *(Vector2 v1, double v2)
		{
			return new Vector2(v1.x * v2, v1.y * v2);
		}

		public static Vector2 operator /(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x / v2.x, v1.y / v2.y);
		}

		public static Vector2 operator /(Vector2 v1, double v2)
		{
			return new Vector2(v1.x / v2, v1.y / v2);
		}

		public static bool operator ==(Vector2 v1, Vector2 v2)
		{
			return !ReferenceEquals(v1, null) && v1.Equals(v2);
		}

		public static bool operator !=(Vector2 v1, Vector2 v2)
		{
			return ReferenceEquals(v1, null) || !v1.Equals(v2);
		}

		public static explicit operator Vector2f(Vector2 other)
		{
			return new Vector2f((float)other.x, (float)other.y);
		}
		#endregion

		public static Vector2 Zero { get { return new Vector2(0, 0); } }
		public static Vector2 One { get { return new Vector2(1, 1); } }
		public static Vector2 Up { get { return new Vector2(0, 1); } }
		public static Vector2 Down { get { return new Vector2(0, -1); } }
		public static Vector2 Right { get { return new Vector2(1, 0); } }
		public static Vector2 Left { get { return new Vector2(-1, 0); } }

		public static Vector2 FromAngle(float angle)
		{
			return new Vector2(Math.Cos(angle), Math.Sin(angle));
		}
		public static double Distance(Vector2 v1, Vector2 v2)
		{
			double x = v1.x - v2.x;
			double y = v1.y - v2.y;
			return Math.Sqrt(x * x + y * y);
		}
		public static Vector2 Clamp(Vector2 vector, double min, double max)
		{
			return new Vector2(MathUtil.Clamp(vector.x, min, max), MathUtil.Clamp(vector.y, min, max)); ;
		}
		public static Vector2 Max(Vector2 vector, double value)
		{
			return new Vector2(Math.Max(vector.x, value), Math.Max(vector.y, value));
		}
		public static Vector2 Min(Vector2 vector, double value)
		{
			return new Vector2(Math.Min(vector.x, value), Math.Min(vector.y, value));
		}
		public static Vector2 Lerp(Vector2 v1, Vector2 v2, double value)
		{
			if (value > 1.0)
				return v2;
			else if (value < 0.0)
				return v1;
			return new Vector2(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value);
		}
	}
}