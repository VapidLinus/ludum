namespace Ludum.Engine
{
	public class Collision
	{
		public readonly Collider collider;
		public readonly Vector2 direction;
		public readonly Vector2 position;

		public Collision(Collider collider, Vector2 direction, Vector2 position)
		{
			this.collider = collider;
			this.direction = direction;
			this.position = position;
		}
	}
}