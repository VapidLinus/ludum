using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.TestGame
{
	public class Wall : GameObject
	{
		public Wall()
		{
			var r = AddComponent<ShapeRenderer>();
			r.SetShape(new RectangleShape(new Vector2f(40, 40)));
			var b = AddComponent<BoxCollider>();
			b.Size = new Vector2(40, 40);
		}

		public override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);
		}
	}
}