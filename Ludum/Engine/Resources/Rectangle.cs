using System;

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
			return string.Format("Rect({0},{1},{2},{3})", position.x, position.y, position.x + size.x, position.y + size.y);
		}

		#region Collision
		public Vector2? IntersectDirection(Rectangle other)
		{
			double difX = other.Position.x - Position.x;
			double difXAbs = Math.Abs(difX);
			double sizeX = Math.Abs((Size.x + other.Size.x) / 2.0);
            bool colX = difXAbs < sizeX;

			double difY = other.Position.y - Position.y;
			double difYAbs = Math.Abs(difY);
			double sizeY = Math.Abs((Size.y + other.Size.y) / 2.0);
			bool colY = difYAbs < sizeY;

			/*Console.WriteLine("---");
			Console.WriteLine("Dif x: {0} Size x: {1}", difXAbs, sizeX);
			Console.WriteLine("Dif y: {0} Size y: {1}", difYAbs, sizeY);*/

			if (colX && colY)
			{
				if (difXAbs > difYAbs)
					return difX > 0 ? Vector2.Right : Vector2.Left;
				else
					return difY > 0 ? Vector2.Up : Vector2.Down;
			}

			return null;
		}

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
			return Intersects(new Rectangle(point, Vector2.Zero));
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