using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class ShapeRenderer : Component
	{
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

		public Vector2 Origin
		{
			get { return origin; }
		}

		private ConvexShape shape = new ConvexShape();
		private Vector2 origin;

		public void SetPoints(Vector2[] points)
		{
			shape.SetPointCount((uint)points.Length);
			origin = Vector2.Zero;
			for (uint i = 0; i < points.Length; i++)
			{
				Vector2 vector = points[i];
                shape.SetPoint(i, vector.ToVector2f());
				origin += vector;
			}
			origin /= points.Length;
		}

		public Vector2[] GetPoints()
		{
			Vector2[] points = new Vector2[shape.GetPointCount()];
			for (uint i = 0; i < points.Length; i++)
			{
				points[i] = (Vector2)shape.GetPoint(i);
			}
			return points;
		}

		public override void OnRender()
		{
			if (Camera.Main == null || shape == null) return;

			float zoom = (float)Camera.Main.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = origin.ToVector2f();
            shape.Position = Camera.Main.WorldToScreen(Transform.RenderPosition);

			Render.Window.Draw(shape);
		}
	}
}