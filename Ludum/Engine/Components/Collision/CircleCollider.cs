using System;

namespace Ludum.Engine
{
	public class CircleCollider : Collider
	{
		public override Vector2 Top
		{
			get
			{
				ColliderPosition = Transform.Position;
				return Transform.Position + Vector2.Up * Radius;
			}
		}

		private float radius = 1f;
		public float Radius
		{
			get { return radius; }
			set { radius = Math.Max(0, value); }
		}
	}
}