using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	class RectangleRenderer : Component
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

		public override void OnAwake()
		{
			shape = new RectangleShape((Vector2f)size);
		}

		public override void OnRender()
		{
			Camera camera = Application.Scene.Camera;

			float zoom = (float)camera.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.Position = new Vector2f(
				Render.WindowWidth * 0.5f + (float)(Transform.Position.x - camera.Position.x) * zoom,
				Render.WindowHeight * 0.5f + (float)-(Transform.Position.y - camera.Position.y) * zoom);

			Render.Window.Draw(shape);
		}
	}
}
