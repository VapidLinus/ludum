using System.Collections.Generic;

namespace Ludum.Engine
{
	public class Camera : Component
	{
		private double zoom = 100;
		public double Zoom { get { return zoom; } set { this.zoom = value; } }

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
	}
}