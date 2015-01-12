using System.Collections.Generic;
using Ludum.Engine;

namespace Ludum.Engine
{
	public abstract class Collider : Component
	{
		private static List<Collider> colliders = new List<Collider>();

		public abstract bool Collides(Vector2 point);
		public abstract bool Collides(Collider other);

		public override void OnAwake()
		{
			base.OnAwake();

			colliders.Add(this);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			colliders.Remove(this);
		}

		public static bool CheckCollision(Vector2 point)
		{
			for (int i = 0; i < colliders.Count; i++)
			{
				if (colliders[i].Collides(point)) return true;
			}
			return false;
		}

		public static bool CheckCollision(Collider other)
		{
			for (int i = 0; i < colliders.Count; i++)
			{
				var collider = colliders[i];

				if (collider == other) continue;
				if (collider.Collides(other)) return true;
			}
			return false;
		}
	}
}