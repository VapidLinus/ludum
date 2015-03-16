using System;
using System.IO;

namespace Ludum.Engine
{
	public class Application : SingleInstance<Application>
	{
		private Scene scene;
		private Config config;

		internal Application()
		{
			SetInstance(this);

			scene = new Scene();
		}

		#region Static
		public static void NewScene()
		{
			Instance.scene.OnDestroy();
			Instance.scene = new Scene();
		}
		public static Scene Scene
		{
			get { return Instance.scene ?? (Instance.scene = new Scene()); }
		}
		public static string DataPath
		{
			get { return Path.Combine(Environment.CurrentDirectory, "Data"); }
		}
		public static Config Config
		{
			get
			{
				if (Instance.config == null) Instance.config = new Config(DataPath + "\\config.conf");
				return Instance.config;
			}
		}
		#endregion
	}
}	