using System;
using Ludum.Engine;

namespace Ludum.Engine
{
	public struct Rectangle
	{
		private Vector2 position;
		private Vector2 size;
		private Vector2 origin;

		#region Properties
		public Vector2 Position
		{
			get { return position; }
			set { this.position = value; }
		}
		public Vector2 Size
		{
			get { return size; }
			set { size = new Vector2(Math.Max(value.x, 0), Math.Max(value.y, 0)); }
		}
		public Vector2 OriginPosition
		{
			get { return new Vector2(position.x + size.x * origin.x, position.y + size.y * origin.y); }
			set { this.position = new Vector2(value.x - size.x * origin.x, value.y - size.y * origin.y); }
		}
		public Vector2 Origin
		{
			get { return origin; }
			set
			{
				origin = new Vector2(
					Math.Min(Math.Max(0, origin.x), 1),
					Math.Min(Math.Max(0, origin.y), 1));
			}
		}
		#endregion

		public Rectangle(Vector2 position, Vector2 size)
		{
			this.position = position;
			this.size = size;
			this.origin = Vector2.One / 2;
		}

		#region Collision
		public bool Intersects(Rectangle other)
		{
			return
				other.Position.x + other.Size.x > this.position.x &&
				other.Position.x < this.position.x + this.Size.x &&
				other.Position.y + other.Size.y > this.position.y &&
				other.Position.y < this.position.y + this.Size.y;
		}

		public bool Intersects(Vector2 point)
		{
			return this.Intersects(new Rectangle(point, Vector2.Zero));
		}
		#endregion
		#region Static
		public static bool Intersects(Rectangle first, Rectangle second)
		{
			return first.Intersects(second);
		}
		public static bool Intersects(Rectangle rectangle, Vector2 point)
		{
			return rectangle.Intersects(point);
		}
		#endregion
	}
}