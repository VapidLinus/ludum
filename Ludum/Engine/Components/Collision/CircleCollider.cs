using System;

namespace Ludum.Engine
{
	public class CircleCollider : Collider
	{
		public float Radius = 1;

		public override bool Collides(Vector2 point)
		{
			float distanceX = Math.Abs(GameObject.Position.x - point.x);
			float distanceY = Math.Abs(GameObject.Position.y - point.y);
			float distance = distanceX * distanceX + distanceY * distanceY;

			return distance < Radius * Radius;
		}

		public override bool Collides(Collider other)
		{
			if (other is CircleCollider)
			{
				var circle = (CircleCollider)other;

				float distanceX = Math.Abs(GameObject.Position.x - circle.GameObject.Position.x);
				float distanceY = Math.Abs(GameObject.Position.y - circle.GameObject.Position.y);

				return (distanceX * distanceX + distanceY * distanceY < Radius * Radius);
			}
			else if (other is BoxCollider)
			{
				return other.Collides(this);
			}
			Console.WriteLine("Unsupported collision");
			return false;
		}
	}
}