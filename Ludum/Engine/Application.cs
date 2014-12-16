namespace Ludum.Engine
{
	static class Application
	{
		private static Scene scene;
		public static Scene Scene
		{
			get
			{
				if (scene == null) scene = new Scene();
				return scene;
			}
		}
	}
}