using System;
using System.Collections.Generic;
using Ludum.Engine;

namespace Ludum.Engine
{
	public abstract class Collider : Component
	{
		private static List<Collider> colliders = new List<Collider>();

		public virtual Vector2 ColliderPosition { get; protected set; }
		public abstract Vector2 Top { get; }

		public override void OnAwake()
		{
			base.OnAwake();

			colliders.Add(this);
		}
		public override void OnDestroy()
		{
			colliders.Remove(this);
		}

		public Collider Overlap(Vector2 position)
		{
			return CheckCollision(this, position);
		}

		#region Static
		public static Collider CheckCollision(Vector2 point)
		{
			for (int i = 0; i < colliders.Count; i++)
			{
				var collider = colliders[i];
				collider.ColliderPosition = collider.Transform.Position;
				if (CheckCollisionPoint(collider, point)) return collider;
			}
			return null;
		}

		/// <summary>
		/// Check if the specified collider is colliding with another.
		/// </summary>
		/// <param name="collider">The collider to check collision for</param>
		/// <returns>The collider it collides with, or null</returns>
		public static Collider CheckCollision(Collider collider)
		{
			return CheckCollision(collider, collider.Transform.Position);
		}

		/// <summary>
		/// Check if the specified collider is colliding with another
		/// from the specified position.
		/// </summary>
		/// <param name="collider">The collider to check collision for</param>
		/// <param name="position">The position to check collosion from</param>
		/// <returns>The collider it collides with, or null</returns>
		public static Collider CheckCollision(Collider collider, Vector2 position)
		{
			collider.ColliderPosition = position;
			for (int i = 0; i < colliders.Count; i++)
			{
				var other = colliders[i];

				if (collider == other) continue;
				other.ColliderPosition = other.Transform.Position;
				if (CheckTwoColliders(collider, other)) return other;
			}
			return null;
		}

		#region Internal
		private static bool CheckTwoColliders(Collider c1, Collider c2)
		{
			// Store types
			var type1 = c1.GetType();
			var type2 = c2.GetType();

			// Box vs Box
			if (type1 == typeof(BoxCollider) && type2 == typeof(BoxCollider))
			{ return ((BoxCollider)c1).Rectangle.Intersects(((BoxCollider)c2).Rectangle); }
			// Box vs Circle
			else if (type1 == typeof(BoxCollider) && type2 == typeof(CircleCollider))
			{ return CheckBoxCircleCollision((BoxCollider)c1, (CircleCollider)c2); }
			else if (type1 == typeof(CircleCollider) && type2 == typeof(BoxCollider))
			{ return CheckBoxCircleCollision((BoxCollider)c2, (CircleCollider)c1); }
			// Circle vs Circle
			else if (type1 == typeof(CircleCollider) && type2 == typeof(CircleCollider))
			{
				double distanceSquared = (c1.ColliderPosition - c2.ColliderPosition).SquareMagnitude;
				double combinedRadius = ((CircleCollider)c1).Radius + ((CircleCollider)c2).Radius;
				return distanceSquared < combinedRadius * combinedRadius;
			}

			// Not implemented :(
			throw new NotImplementedException("Collision between " + type1 + " and " + type2 + " is not supported.");
		}

		private static bool CheckCollisionPoint(Collider collider, Vector2 point)
		{
			var type = collider.GetType();

			if (type == typeof(BoxCollider)) return ((BoxCollider)collider).Rectangle.Intersects(point);
			else if (type == typeof(CircleCollider))
			{
				var circle = (CircleCollider)collider;

				double distanceX = Math.Abs(circle.ColliderPosition.x - point.x);
				double distanceY = Math.Abs(circle.ColliderPosition.y - point.y);
				double distance = distanceX * distanceX + distanceY * distanceY;

				return distance < circle.Radius * circle.Radius;
			}

			// Not implemented :(
			throw new NotImplementedException("Collision between " + type + " and Vector2 is not supported.");
		}

		private static bool CheckBoxCircleCollision(BoxCollider box, CircleCollider circle)
		{
			// Solution based on e.James's answer
			// http://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection

			var rectangle = box.Rectangle;

			// Absolute distance
			double distanceX = Math.Abs(circle.ColliderPosition.x - box.ColliderPosition.x);
			double distanceY = Math.Abs(circle.ColliderPosition.y - box.ColliderPosition.y);

			// Check if outside (top left)
			if (distanceX > rectangle.Size.x / 2.0 + circle.Radius ||
				distanceY > rectangle.Size.y / 2.0 + circle.Radius) return false;

			// Check if inside (bottom right)
			if (distanceX < rectangle.Size.x / 2.0 ||
				distanceY < rectangle.Size.y / 2.0) return true;

			// Corner distance
			double x = distanceX - rectangle.Size.x / 2.0;
			double y = distanceY - rectangle.Size.y / 2.0;

			// Check if distance is lower than circle radius
			// Don't use sqrt, but rather do radius^2 for performance
			return x * x + y * y < circle.Radius * circle.Radius;
		}
		#endregion
		#endregion
	}
}