﻿using System;

namespace Ludum.Engine
{
	public struct Rectangle
	{
		private Vector2 position;
		private Vector2 size;

		#region Properties
		public Vector2 Position
		{
			get { return position; }
			set { this.position = value; }
		}
		public Vector2 CenterPosition
		{
			get { return position + size / 2.0; }
			set { this.position = value - size / 2.0; }
		}
		public Vector2 Size
		{
			get { return size; }
			set { size = new Vector2(Math.Max(value.x, 0), Math.Max(value.y, 0)); }
		}
		#endregion

		public Rectangle(Vector2 position, Vector2 size)
		{
			this.position = position;
			this.size = size;
		}

		public override string ToString()
		{
			return String.Format("Rect({0},{1},{2},{3})", position.x, position.y, position.x + size.x, position.y + size.y);
		}

		#region Collision
		public bool Intersects(Rectangle other)
		{
			return
				other.Position.x + other.Size.x > position.x &&
				other.Position.x < position.x + Size.x &&
				other.Position.y + other.Size.y > position.y &&
				other.Position.y < position.y + Size.y;
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