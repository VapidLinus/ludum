using Ludum.Engine;
using System;

namespace Ludum.TestGame
{
	class CameraController : GameObject
	{
		public override void OnUpdate()
		{
			Application.Scene.Camera.Zoom = 50 + 20 * Math.Sin(Render.Time * 4);
			Application.Scene.Camera.Position = Vector2.Right * 6 * Math.Sin(Render.Time);
		}
	}
}