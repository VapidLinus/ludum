using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class ShapeRenderer : Component
	{
		private Shape shape;

		public void SetShape(Shape shape)
		{
			this.shape = shape;
		}

		public T GetShape<T>() where T : Shape
		{
			return shape as T;
		}

		public override void OnUpdate(float delta)
		{
			if (shape == null) return;
			shape.Position = (Vector2f) GameObject.GetComponent<Transform>().Position;
			shape.Position = new Vector2f(shape.Position.X, -shape.Position.Y);
		}

		public override void OnRender()
		{
			if (shape == null) return;
			Render.Window.Draw(shape);
		}
	}
}