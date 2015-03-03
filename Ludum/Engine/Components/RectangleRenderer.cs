using SFML.Graphics;
using SFML.Window;
using System;

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

		public float Rotation
		{
			get { return shape.Rotation; }
			set { shape.Rotation = value; }
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
			if (Camera.Main == null || !IsOnScreen()) return;

			float zoom = (float)Camera.Main.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.Position = Camera.Main.WorldToScreenInvertedY(Transform.RenderPosition);

			Render.Window.Draw(shape);
		}

		bool IsOnScreen()
		{
			var camera = Camera.Main;

			var right = camera.WorldToScreenX(Transform.RenderPosition.x + Size.x * .5);
			if (right < 0) return false;

			var left = camera.WorldToScreenX(Transform.RenderPosition.x - Size.x * .5);
			if (left > Render.WindowWidth) return false;

			var top = camera.WorldToScreenInvertedY(Transform.RenderPosition.y + Size.y * .5);
			if (top > Render.WindowHeight) return false;

			var bottom = camera.WorldToScreenInvertedY(Transform.RenderPosition.y - Size.y * .5);
			if (bottom < 0) return false;

			return true;
		}
	}
}
