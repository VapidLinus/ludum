using System;

namespace Ludum.Engine
{
	public class BoxCollider : Collider
	{
		private Rectangle rectangle = new Rectangle(Vector2.Zero, Vector2.One);

		public Vector2 Size
		{
			get { return rectangle.Size; }
			set { rectangle.Size = value; }
		}

		public override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);
			rectangle.OriginPosition = GameObject.Position;
		}

		public override bool Collides(Vector2 point)
		{
			return rectangle.Intersects(point);
		}

		public override bool Collides(Collider other)
		{
			if (other is BoxCollider)
			{
				return rectangle.Intersects(((BoxCollider)other).rectangle);
			}
			else if (other is CircleCollider)
			{
				// Solution based on e.James's answer
				// http://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection

				var circle = (CircleCollider)other;

				// Absolute distance
				float distanceX = Math.Abs(circle.GameObject.Position.x - rectangle.OriginPosition.x);
				float distanceY = Math.Abs(circle.GameObject.Position.y - rectangle.OriginPosition.y);

				// Check if outside (top left)
				if (distanceX > rectangle.Size.x / 2.0 + circle.Radius ||
					distanceY > rectangle.Size.y / 2.0 + circle.Radius) return false;

				// Check if inside (bottom right)
				if (distanceX <= rectangle.Size.x / 2.0 ||
					distanceY <= rectangle.Size.y / 2.0) return true;

				// Corner distance
				float x = distanceX - rectangle.Size.x / 2f;
				float y = distanceY - rectangle.Size.y / 2f;

				// Check if distance is lower than circle radius
				// Don't use sqrt, but rather do radius^2 for performance
				return x * x + y * y <= circle.Radius * circle.Radius;
			}
			Console.WriteLine("Unsupported collision");
			return false;
		}
	}
}