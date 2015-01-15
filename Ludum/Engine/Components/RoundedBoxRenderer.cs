using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class RoundedBoxRenderer : Component
	{
		private RectangleShape boxVertical, boxHorizontal;
		private CircleShape[] circles = new CircleShape[4];

		public Vector2 Size
		{
			get { return size; }
			set
			{
				this.size = new Vector2(Math.Max(value.x, 0), Math.Max(value.y, 0));
				boxVertical.Size = new Vector2f(size.x, size.y - radius * 2);
				boxHorizontal.Size = new Vector2f(size.x - radius * 2, size.y);
			}
		}
		public float Radius
		{
			get { return radius; }
			set
			{
				radius = Math.Max(value, 0);
				Size = Size;
				for (int i = 0; i < circles.Length; i++) circles[i].Radius = radius;
			}
		}

		private Vector2 size;
		private float radius;

		public override void OnAwake()
		{
			base.OnAwake();

			boxVertical = new RectangleShape();
			boxHorizontal = new RectangleShape();

			circles[0] = new CircleShape();
			circles[1] = new CircleShape();
			circles[2] = new CircleShape();
			circles[3] = new CircleShape();

			Size = Vector2.One * 40;
			Radius = 10;
		}

		public override void OnUpdate(float delta)
		{
			// Convert
			Vector2f position = (Vector2f)Transform.Position;
			position.Y = -position.Y;
			position -= new Vector2f(Size.x / 4f, Size.y / 4f);

			// Boxs' position
			boxHorizontal.Position = position + new Vector2f(radius, 0);
			boxVertical.Position = position + new Vector2f(0, radius);

			// For circles
			float x = size.x - radius * 2;
			float y = size.y - radius * 2;

			// Top Right, Clockwise
			circles[0].Position = position + new Vector2f(x, y);
			circles[1].Position = position + new Vector2f(x, 0);
			circles[2].Position = position + new Vector2f(0, 0);
			circles[3].Position = position + new Vector2f(0, y);
		}

		public override void OnRender()
		{
			Render.Window.Draw(boxHorizontal);
			Render.Window.Draw(boxVertical);
			Render.Window.Draw(circles[0]);
			Render.Window.Draw(circles[1]);
			Render.Window.Draw(circles[2]);
			Render.Window.Draw(circles[3]);
		}
	}
}