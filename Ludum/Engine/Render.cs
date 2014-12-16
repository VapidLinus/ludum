using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	static class Render
	{
		private static RenderWindow window;
		public static RenderWindow Window
		{
			get
			{
				if (window == null) throw new InvalidOperationException("OnInitialize() has not been called");
				return window;
			}
			private set { window = value; }
		}

		private static bool isInitialized = false;
		internal static void OnInitialize()
		{
			// Mark as initialized
			if (isInitialized) return;
			isInitialized = true;

			Window = new RenderWindow(new VideoMode(800, 450), "Ludum", Styles.Close);
			Window.SetVerticalSyncEnabled(true);
			Window.Closed += (sender, e) => ((RenderWindow)sender).Close();
		}
	}
}
