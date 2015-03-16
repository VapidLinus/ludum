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
		public double OutlineWidth
		{
			get { return outlineWidth; }
			set
			{
				outlineWidth = Math.Max(0, value);
				outline.Transform.Scale = new Vector2(size.x + outlineWidth * 2, size.y + outlineWidth * 2);
			}
		}
		public byte RenderLayer
		{
			get { return mainshape.GameObject.RenderLayer; }
			set
			{
				byte layer = MathUtil.Clamp(value, (byte)10, byte.MaxValue);

				mainshape.GameObject.RenderLayer = layer;
				outline.GameObject.RenderLayer = (byte)(layer - 10);
			}
		}

		public override void OnAwake()
		{
			mainshape = new GameObject("RectangleOutline_Main", Transform.Position).AddComponent<RectangleRenderer>();
			outline = new GameObject("RectangleOutline_Outline", Transform.Position).AddComponent<RectangleRenderer>();

			outline.GameObject.RenderLayer = Render.DEFAULT_RENDER_LAYER - 10;

			OutlineColor = Color.Black;
			MainColor = Color.White;
			OutlineWidth = .1;
		}

		public override void OnUpdate()
		{
			outline.Transform.Scale = Transform.Scale + new Vector2(OutlineWidth, OutlineWidth) * 2;
			mainshape.Transform.Scale = Transform.Scale;

			outline.Transform.Rotation = mainshape.Transform.Rotation = Transform.Rotation;
			outline.Transform.Position = mainshape.Transform.Position = Transform.Position;
		}

		public override void OnDestroy()
		{
			outline.GameObject.Destroy();
			mainshape.GameObject.Destroy();
		}
	}
}