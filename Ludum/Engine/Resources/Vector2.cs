using System;
using SFML.Window;

namespace Ludum.Engine.Resources
{
	public class Vector2
	{
		public float X { get; set; }
		public float Y { get; set; }

		/// <summary>
		/// Creatures a 2D vector with the coordinates 0, 0.
		/// </summary>
		public Vector2() : this(0, 0) { }

		/// <summary>
		/// Creatures a 2D vector with given coordinates.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Normalizes the vector.
		/// </summary>
		public void Normalize()
		{
			float magnitude = Magnitude();
			X /= magnitude;
			Y /= magnitude;
		}

		/// <summary>
		/// Gets vector's magnitude.
		/// </summary>
		/// <returns>Vector's magnitude</returns>
		public float Magnitude()
		{
			return (float)Math.Sqrt(X * X + Y * Y);
		}

		public override bool Equals(object obj)
		{
			return obj is Vector2 && obj.Equals(this);
		}

		public bool Equals(Vector2 other)
		{
			if (ReferenceEquals(other, null)) return false;
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		/// <summary>
		/// Returns a new normalized instance of the vector.
		/// </summary>
		public Vector2 Normalized
		{
			get
			{
				var normalized = new Vector2(X, Y);
				normalized.Normalize();
				return normalized;
			}
		}

		#region Operators
		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Vector2 operator *(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
		}

		public static Vector2 operator *(Vector2 v1, float v2)
		{
			return new Vector2(v1.X * v2, v1.Y * v2);
		}

		public static Vector2 operator /(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
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
			return new Vector2f(other.X, other.Y);
		}
		#endregion

		public static Vector2 Zero { get { return new Vector2(0, 0); } }
		public static Vector2 One { get { return new Vector2(1, 1); } }
	}
}