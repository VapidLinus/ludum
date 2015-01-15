using System;
using System.Collections.Generic;
using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.TestGame
{
	class Game : Core
	{
		protected override void OnInitialize()
		{
			base.OnInitialize();

			new Player(0) { Position = new Vector2(400, -220) };
			new Player(1) { Position = new Vector2(300, -220) };

			new Wall() { Position = new Vector2(18 * 40, -400 + 1 * 40) };
			new Wall() { Position = new Vector2(18 * 40, -400 + 2 * 40) };
			new Wall() { Position = new Vector2(12 * 40, -400 + 6 * 40) };
			new Wall() { Position = new Vector2(10 * 40, -400 + 4 * 40) };

			for (int i = 0; i < 20; i++)
			{
				new Wall() { Position = new Vector2(i * 40, -400) };
			}
		}
	}
}