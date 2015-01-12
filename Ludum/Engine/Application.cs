namespace Ludum.Engine
{
	public class Application : SingleInstance<Application>
	{
		private Scene scene;

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
		#endregion
	}
}