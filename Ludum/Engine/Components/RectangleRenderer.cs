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
			var right = Camera.Main.WorldToScreenInvertedY(Transform.RenderPosition + Vector2.Right * Size.x * .5).X;
			if (right < 0) return false;
					
			var left = Camera.Main.WorldToScreenInvertedY(Transform.RenderPosition + Vector2.Left * Size.x * .5).X;
			if (left > Render.WindowWidth) return false;

			var top = Camera.Main.WorldToScreenInvertedY(Transform.RenderPosition + Vector2.Up * Size.y * .5).Y;
			if (top > Render.WindowHeight) return false;

			var bottom = Camera.Main.WorldToScreenInvertedY(Transform.RenderPosition + Vector2.Down * Size.y * .5).Y;
			if (bottom < 0) return false;

			return true;
		}
	}
}
