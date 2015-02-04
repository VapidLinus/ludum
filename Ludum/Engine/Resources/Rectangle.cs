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

		public Vector2 GetEdgeDirection(Vector2 direction)
		{
			if (!direction.IsNormalized) direction.Normalize();
			double magnitude = 0;

			double angle = direction.ToAngle(direction);
			double absCosAngle = Math.Abs(Math.Cos(angle));
			double absSinAngle = Math.Abs(Math.Sin(angle));

			if (Size.x / 2.0 * absSinAngle <= Size.y / 2.0 * absCosAngle)
				magnitude = Size.x / 2.0 / absCosAngle;
			else
				magnitude = Size.y / 2.0 / absSinAngle;

			return direction * magnitude;
		}

		public override string ToString()
		{
			return string.Format("Rect({0},{1},{2},{3})", position.x, position.y, position.x + size.x, position.y + size.y);
		}

		#region Collision
		public Vector2? IntersectDirection(Rectangle other)
		{
			double difX = other.CenterPosition.x - CenterPosition.x;
			float difXAbs = (float)Math.Abs(difX);
			float sizeX = (float)Math.Abs((Size.x + other.Size.x) / 2.0);
            bool colX = difXAbs < sizeX;

			double difY = other.CenterPosition.y - CenterPosition.y;
			float difYAbs = (float)Math.Abs(difY);
			float sizeY = (float)Math.Abs((Size.y + other.Size.y) / 2.0);
			bool colY = difYAbs - sizeY < 0;

			//Console.WriteLine("Size Y: {0} vs {1}", Size.y, other.Size.y);
			//Console.WriteLine("other y: {0}, my y: {1}", other.position.y, Position.y);
			//Console.WriteLine("y: {0} < {1} = {2}", difYAbs.ToString(), sizeY.ToString(), colY);

			if (colX && colY)
			{
				if (difXAbs > difYAbs)
					return difX > 0 ? Vector2.Left : Vector2.Right;
				else
					return difY > 0 ? Vector2.Down : Vector2.Up;
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