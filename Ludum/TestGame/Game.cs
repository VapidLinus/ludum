using System;
using System.Collections.Generic;
using Ludum.Engine;

namespace Ludum.TestGame
{
	class Game : Core
	{
		protected override void OnInitialize()
		{
			base.OnInitialize();

			new Player(0) { Position = new Vector2(-1, 0) };
			new Player(1) { Position = new Vector2(1, 0) };

			new CameraController();


			new Wall();
			for (int i = -10; i < 20; i++)
			{
				new Wall() { Position = new Vector2(i, -1) };
			}
		}

		protected override void OnRender()
		{
			base.OnRender();
		}
	}
}