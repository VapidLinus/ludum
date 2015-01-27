using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class RectangleRenderer : Component
	{
		private RectangleShape shape;

		private Vector2 size = Vector2.One;
		public Vector2 Size
		{
			get { return size; }
			set
			{
				size = Vector2.Max(value, 0);
				shape.Size = (Vector2f)size;
			}
		}

		public Color Color
		{
			get { return shape.FillColor; }
			set { shape.FillColor = value; }
		}

		public override void OnAwake()
		{
			shape = new RectangleShape((Vector2f)size);
        }

		public override void OnRender()
		{
			if (Camera.Main == null) return;

			float zoom = (float)Camera.Main.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.Position = new Vector2f(
				Render.WindowWidth * 0.5f + (float)(Transform.Position.x - Camera.Main.Transform.Position.x) * zoom,
				Render.WindowHeight * 0.5f + (float)-(Transform.Position.y - Camera.Main.Transform.Position.y) * zoom);

			Render.Window.Draw(shape);
		}
	}
}
