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
			return false;
		}
	}
}