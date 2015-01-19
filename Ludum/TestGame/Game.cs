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

			new Player(0) { Position = new Vector2(0, 2) };
			//new Player(1) { Position = new Vector2(1, 0) };


			for (int i = -10; i < 20; i++)
			{
				new Wall() { Position = new Vector2(i, 0) };
			}
		}
	}
}