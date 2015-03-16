namespace Ludum.Engine
{
	public class BoxCollider : Collider, ISizable
	{
		private Rectangle rectangle = new Rectangle(Vector2.Zero, Vector2.One);

		public override Vector2 Top
		{
			get 
			{ 
				ColliderPosition = Transform.Position; 
				return Transform.Position + Vector2.Up * rectangle.Size.y / 2.0; 
			}
		}

		/// <summary>
		/// Internal rectangle, used for collision.
		/// Warning! Not guaranteed to be in correct position.
		/// </summary>
		public Rectangle Rectangle { get { return rectangle; } }

		public override Vector2 ColliderPosition
		{
			get { return rectangle.Position; }
			protected set
			{
				base.ColliderPosition = value;
				rectangle.CenterPosition = value;
			}
		}

		public Vector2 Size
		{
			get { return rectangle.Size; }
			set { rectangle.Size = value; }
		}
	}
}