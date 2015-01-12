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

			new Wall() { Position = new Vector2(400, -220) };
			new Player() { Position = new Vector2(400, -220) };
		}
	}
}