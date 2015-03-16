using SFML.Graphics;
using System;

namespace Ludum.Engine
{
	public class ShapeOutlineRenderer : Component
	{
		private ShapeRenderer outline, mainshape;

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

				Vector2[] points = outline.GetPoints();
				for (int i = 0; i < points.Length; i++)
				{
					points[i] += (points[i] - outline.Origin).Normalized * OutlineWidth;
				}
				outline.SetPoints(points);
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
			mainshape = new GameObject("ShapeOutline_Main", Transform.Position).AddComponent<ShapeRenderer>();
			outline = new GameObject("ShapeOutline_Outline", Transform.Position).AddComponent<ShapeRenderer>();

			RenderLayer = Render.DEFAULT_RENDER_LAYER - 10;

			OutlineColor = Color.Black;
			MainColor = Color.White;
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

		public void SetPoints(Vector2[] points)
		{
			mainshape.SetPoints(points);
			outline.SetPoints(points);
			// Update outline
			OutlineWidth = OutlineWidth;
		}
	}
}