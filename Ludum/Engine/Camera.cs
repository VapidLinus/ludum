using SFML.Window;
using System;
using System.Collections.Generic;

namespace Ludum.Engine
{
	public class Camera : Component
	{
		private double zoom = 100;
		public double Zoom { get { return zoom; } set { zoom = Math.Max(value, Double.Epsilon); } }

		/// <summary>
		/// Finds a camera in the scene. Automatically caches.
		/// <para>Returns null if there is no camera.</para>
		/// </summary>
		private static Camera main;
		public static Camera Main
		{
			get
			{
				// If there is a reference, return the instance
				if (main != null) return main;

				// Find Camera
				IList<GameObject> objects = Application.Scene.GameObjects as IList<GameObject>;
				for (int i = 0; i < objects.Count; i++)
				{
					Camera camera = objects[i].GetComponent<Camera>();
					if (camera != null)
					{
						return main = camera;
					}
				}
				return null;
			}
		}

		public Vector2f WorldToScreen(Vector2 worldPosition)
		{
			return new Vector2f(
				WorldToScreenX(worldPosition.x),
				WorldToScreenY(worldPosition.y));
		}

		public Vector2 ScreenToWorld(Vector2f screenPosition)
		{
			double windowScale = 2 * Zoom;
			double screenScale = 1 / Zoom;

			return new Vector2(
				Transform.RenderPosition.x - (Render.WindowWidth / windowScale) + screenPosition.X * screenScale,
				Transform.RenderPosition.y + (Render.WindowHeight / windowScale) - screenPosition.Y * screenScale);
		}

		public float WorldToScreenX(double worldX)
		{
			return (float)(Render.WindowWidth * 0.5 + (worldX - Transform.RenderPosition.x) * zoom);
        }

		public float WorldToScreenY(double worldY)
		{
			return (float)(Render.WindowHeight * 0.5 + -(worldY - Transform.RenderPosition.y) * zoom);
		}

		public double ScreenToWorldX(float screenX)
		{
			double windowScale = 2 * Zoom;
			double screenScale = 1 / Zoom;

			return Transform.RenderPosition.x - (Render.WindowWidth / windowScale) + screenX * screenScale;
		}

		public double ScreenToWorldY(float screenY)
		{
			double windowScale = 2 * Zoom;
			double screenScale = 1 / Zoom;

			return Transform.RenderPosition.y + (Render.WindowHeight / windowScale) + screenY * screenScale;
		}
	}
}