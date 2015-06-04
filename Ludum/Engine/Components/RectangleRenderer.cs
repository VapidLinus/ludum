using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class RectangleRenderer : Component
	{
		private RectangleShape shape;
		public Color Color
		{
			get { return shape.FillColor; }
			set { shape.FillColor = value; }
		}
		public Texture Texture
		{
			get { return shape.Texture; }
			set { shape.Texture = value; }
		}
		private bool visible = true;
		public bool Visible { get { return visible; } set { visible = value; } }

		public override void OnAwake()
		{
			shape = new RectangleShape((Vector2f)Transform.Scale);
		}

		public override void OnRender()
		{
			if (!visible || Camera.Main == null || !IsOnScreen()) return;

			float zoom = (float)Camera.Main.Zoom;

			shape.Size = (Vector2f)Transform.Scale;
			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.Rotation = Transform.Rotation;
			shape.Position = Camera.Main.WorldToScreen(Transform.RenderPosition);

			Render.Window.Draw(shape);
		}

		public override void OnDestroy()
		{
			shape.Dispose();
		}

		bool IsOnScreen()
		{
			var camera = Camera.Main;

			var right = camera.WorldToScreenX(Transform.RenderPosition.x + Transform.Scale.x * .5);
			if (right < 0) return false;

			var left = camera.WorldToScreenX(Transform.RenderPosition.x - Transform.Scale.x * .5);
			if (left > Render.WindowWidth) return false;

			var top = camera.WorldToScreenY(Transform.RenderPosition.y + Transform.Scale.y * .5);
			if (top > Render.WindowHeight) return false;

			var bottom = camera.WorldToScreenY(Transform.RenderPosition.y - Transform.Scale.y * .5);
			if (bottom < 0) return false;

			return true;
		}
	}
}
