using SFML.Graphics;
using System;

namespace Ludum.Engine
{
	public class RectangleOutlineRenderer : Component, ISizable
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
		private float rotation;
		public float Rotation
		{
			get { return rotation; }
			set { rotation = mainshape.Rotation = outline.Rotation = value; }
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
			Size = Vector2.One;
			OutlineWidth = .1;
		}

		public override void OnUpdate()
		{
			outline.Transform.Position = mainshape.Transform.Position = Transform.Position;
		}

		public override void OnDestroy()
		{
			outline.GameObject.Destroy();
			mainshape.GameObject.Destroy();
		}
	}
}