using System;
using SFML.Window;

namespace Ludum.Engine
{
	public struct Vector2
	{
		public float x;
		public float y;

		/// <summary>
		/// Creatures a 2D vector with given coordinates.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector2(float x = 0, float y = 0)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Normalizes the vector.
		/// </summary>
		public void Normalize()
		{
			float magnitude = Magnitude;
			x /= magnitude;
			y /= magnitude;
		}

		/// <summary>
		/// Gets vector's magnitude.
		/// </summary>
		public float Magnitude
		{
			get { return (float)Math.Sqrt(x * x + y * y); }
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
		/// Creates a copy of a Vector2
		/// </summary>
		/// <param name="other">Vector2 to make a copy of</param>
		/// <returns>Copy of other</returns>
		public Vector2 Clone(Vector2 other)
		{
			if (other == null) throw new ArgumentNullException("other");
			return new Vector2(other.x, other.y);
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

		public static Vector2 operator *(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x * v2.x, v1.y * v2.y);
		}

		public static Vector2 operator *(Vector2 v1, float v2)
		{
			return new Vector2(v1.x * v2, v1.y * v2);
		}

		public static Vector2 operator /(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x / v2.x, v1.y / v2.y);
		}

		public static Vector2 operator /(Vector2 v1, float v2)
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
			if (other == null) throw new InvalidOperationException();
			return new Vector2f(other.x, other.y);
		}
		#endregion

		public static Vector2 Zero { get { return new Vector2(0, 0); } }
		public static Vector2 One { get { return new Vector2(1, 1); } }
		public static Vector2 Up { get { return new Vector2(0, 1); } }
		public static Vector2 Down { get { return new Vector2(0, -1); } }
		public static Vector2 Right { get { return new Vector2(1, 0); } }
		public static Vector2 Left { get { return new Vector2(-1, 0); } }
	}
}