﻿namespace Ludum.Engine
{
	static class Application
	{
		private static Scene scene;
		public static Scene Scene
		{
			get { return scene ?? (scene = new Scene()); }
		}
	}
}