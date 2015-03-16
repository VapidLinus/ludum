using System;
using System.Collections.Generic;
using Ludum.Engine;
using SFML.Window;

namespace Ludum.Engine
{
	public abstract class Collider : Component
	{
		private static List<Collider> colliders = new List<Collider>();

		public virtual Vector2 ColliderPosition { get; protected set; }
		public abstract Vector2 Top { get; }

		public override void OnAwake()
		{
			colliders.Add(this);
		}
		public override void OnDestroy()
		{
			colliders.Remove(this);
		}
		public override void OnUpdate()
		{
			if (Mouse.IsButtonPressed(Mouse.Button.Left))
			{
				Vector2i mouse = Mouse.GetPosition(Render.Window);
				if (CheckCollisionPoint(this, Camera.Main.ScreenToWorldInvertedY(new Vector2f(mouse.X, mouse.Y))))
				{
					foreach (var component in GameObject.Components)
					{
						component.OnClicked();
					}
				}
			}
		}

		public Collision Overlap(Vector2 position)
		{
			return CheckCollision(this, position);
		}

		public HashSet<Collision> OverlapAll(Vector2 position)
		{
			return CheckCollisions(this, position);
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
		public static HashSet<Collider> CheckCollisions(Vector2 point)
		{
			var c = new HashSet<Collider>();
			for (int i = 0; i < colliders.Count; i++)
			{
				var collider = colliders[i];
				collider.ColliderPosition = collider.Transform.Position;
				if (CheckCollisionPoint(collider, point)) c.Add(collider);
			}
			return c;
		}

		/// <summary>
		/// Check if the specified collider is colliding with another.
		/// </summary>
		/// <param name="collider">The collider to check collision for</param>
		/// <returns>The collider info, or null if no collision</returns>
		public static Collision CheckCollision(Collider collider)
		{
			return CheckCollision(collider, collider.Transform.Position);
		}

		public static HashSet<Collision> CheckCollisions(Collider collider)
		{
			var c = new HashSet<Collision>();
			for (int i = 0; i < colliders.Count; i++)
			{
				var entry = colliders[i];
				entry.ColliderPosition = entry.Transform.Position;

				Collision collision;
				if ((collision = CheckCollision(collider, collider.Transform.Position)) != null) c.Add(collision);
			}
			return c;
		}

		/// <summary>
		/// Check if the specified collider is colliding with another
		/// from the specified position.
		/// </summary>
		/// <param name="collider">The collider to check collision for</param>
		/// <param name="position">The position to check collosion from</param>
		/// <returns>The collider info, or null if no collision</returns>
		public static Collision CheckCollision(Collider collider, Vector2 position)
		{
			collider.ColliderPosition = position;
			for (int i = 0; i < colliders.Count; i++)
			{
				var other = colliders[i];

				if (collider == other) continue;
				other.ColliderPosition = other.Transform.Position;
				var result = CheckTwoColliders(collider, other);
				if (result != null) return result;
			}
			return null;
		}

		public static HashSet<Collision> CheckCollisions(Collider collider, Vector2 position)
		{
			collider.ColliderPosition = position;
			var c = new HashSet<Collision>();
			for (int i = 0; i < colliders.Count; i++)
			{
				var other = colliders[i];

				if (collider == other) continue;
				other.ColliderPosition = other.Transform.Position;
				var result = CheckTwoColliders(collider, other);
				if (result != null) c.Add(result);
			}
			return c;
		}

		#region Internal
		private static Collision CheckTwoColliders(Collider c1, Collider c2)
		{
			// Store types
			var type1 = c1.GetType();
			var type2 = c2.GetType();

			// Box vs Box
			if (type1 == typeof(BoxCollider) && type2 == typeof(BoxCollider))
			{
				//return ((BoxCollider)c1).Rectangle.Intersects(((BoxCollider)c2).Rectangle);
				Vector2? direction = ((BoxCollider)c1).Rectangle.IntersectDirection(((BoxCollider)c2).Rectangle);
				if (direction == null) return null;
				return new Collision(
					c2, 
					(Vector2)direction,
					(Vector2)direction * ((BoxCollider)c2).Rectangle.Size * .5 + c2.Transform.Position);
			}
			// Box vs Circle
			else if (type1 == typeof(BoxCollider) && type2 == typeof(CircleCollider))
			{
				var box = (BoxCollider)c1;
				var circle = (CircleCollider)c2;
                if (CheckBoxCircleCollision(box, circle))
				{
					Vector2 direction = (box.Transform.Position - circle.Transform.Position).Normalized;
                    Vector2 point = box.Rectangle.GetEdgeDirection(direction);
					return new Collision(c2, direction, point);
				}
				return null;
			}
			else if (type1 == typeof(CircleCollider) && type2 == typeof(BoxCollider))
			{
				var box = (BoxCollider)c2;
				var circle = (CircleCollider)c1;
				if (CheckBoxCircleCollision(box, circle))
				{
					Vector2 direction = (box.ColliderPosition - circle.ColliderPosition).Normalized;
					Vector2 point = box.Rectangle.GetEdgeDirection(direction);
					return new Collision(c2, direction, box.ColliderPosition + point);
				}
				return null;
			}
			// Circle vs Circle
			else if (type1 == typeof(CircleCollider) && type2 == typeof(CircleCollider))
			{
				// TODO Optimize
				double distanceSquared = (c1.ColliderPosition - c2.ColliderPosition).SquareMagnitude;
				double combinedRadius = (((CircleCollider)c1).Radius + ((CircleCollider)c2).Radius) * .5;
				Vector2 direction = (c2.ColliderPosition - c1.ColliderPosition).Normalized;
                if (distanceSquared < combinedRadius * combinedRadius)
				{
					return new Collision(c2, direction, c1.ColliderPosition + direction * Math.Sqrt(distanceSquared));
				}
				return null;
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
			double radius = circle.Radius * .5;

			// Absolute distance
			double distanceX = Math.Abs(circle.ColliderPosition.x - box.ColliderPosition.x - rectangle.Size.x * .5);
			double distanceY = Math.Abs(circle.ColliderPosition.y - box.ColliderPosition.y - rectangle.Size.y * .5);

			// Check if outside (top left)
			if (distanceX > rectangle.Size.x / 2.0 + radius ||
				distanceY > rectangle.Size.y / 2.0 + radius) return false;

			// Check if inside (bottom right)
			if (distanceX < rectangle.Size.x / 2.0 ||
				distanceY < rectangle.Size.y / 2.0) return true;

			// Corner distance
			double x = distanceX - rectangle.Size.x / 2.0;
			double y = distanceY - rectangle.Size.y / 2.0;

			// Check if distance is lower than circle radius
			// Don't use sqrt, but rather do radius^2 for performance
			return x * x + y * y < radius * radius;
		}
		#endregion
		#endregion
	}
}