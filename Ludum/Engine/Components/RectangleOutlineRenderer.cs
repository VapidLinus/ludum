using SFML.Graphics;
using System;

namespace Ludum.Engine
{
	public class RectangleOutlineRenderer : Component
	{
		private RectangleRenderer outline, mainshape;

		private Vector2 size;
		private double outlineWidth;

		public Color OutlineColor
		{
			get { return outline.Color; }
			set { outline.Color = value; }
		}
		public Color MainColor
		{
			get { return mainshape.Color; }
			set { mainshape.Color = value; }
		}
		public Vector2 Size
		{
			get { return size; }
			set
			{
				size = mainshape.Size = Vector2.Max(value, 0);
				outline.Size = new Vector2(size.x + outlineWidth * 2, size.y + outlineWidth * 2);
			}
		}
		public double OutlineWidth
		{
			get { return outlineWidth; }
			set
			{
				outlineWidth = Math.Max(0, value);
				outline.Size = new Vector2(size.x + outlineWidth * 2, size.y + outlineWidth * 2);
			}
		}

		public RectangleOutlineRenderer()
		{
			mainshape = new GameObject().AddComponent<RectangleRenderer>();
			outline = new GameObject().AddComponent<RectangleRenderer>();

			outline.GameObject.RenderLayer = Render.DEFAULT_RENDER_LAYER - 10;

			OutlineColor = SFML.Graphics.Color.Black;
			MainColor = SFML.Graphics.Color.White;
			Size = Vector2.One;
			OutlineWidth = .1;
		}

		public override void OnUpdate()
		{
			outline.Transform.Position = mainshape.Transform.Position = Transform.Position;
		}
	}
}