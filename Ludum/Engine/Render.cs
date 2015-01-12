using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class Render : SingleInstance<Render>
	{
		private readonly Core core;
		private readonly RenderWindow window;

		internal Render(Core core)
			: this(core, new RenderMode(new VideoMode(800, 450), "Ludum", Styles.Close)) { }

		internal Render(Core core, RenderMode renderMode)
		{
			SetInstance(this);

			this.core = core;

			window = new RenderWindow(renderMode.videoMode, renderMode.title, renderMode.style);
			window.SetVerticalSyncEnabled(false);
			window.Closed += (sender, e) => ((RenderWindow)sender).Close();
		}

		#region Static
		public static RenderWindow Window { get { return Instance.window; } }
		#endregion
	}
}