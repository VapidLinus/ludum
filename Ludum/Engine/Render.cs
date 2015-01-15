using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class Render : SingleInstance<Render>
	{
		private readonly Core core;
		private readonly RenderWindow window;

		private float time = 0;

		private const int SMOOTH_FPS_SAMPLES = 50;
		private int fpsCurrentSample = 0;
		private float[] fpsSamples = new float[SMOOTH_FPS_SAMPLES];

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

		internal void ReportDelta(float delta)
		{
			time += delta;

			fpsSamples[fpsCurrentSample++] = 1f / delta;
			if (fpsCurrentSample >= SMOOTH_FPS_SAMPLES) fpsCurrentSample = 0;
		}

		#region Static
		public static RenderWindow Window { get { return Instance.window; } }
		public static int FPS
		{
			get
			{
				return (int)Math.Round(Instance.fpsSamples[Instance.fpsCurrentSample]);
			}
		}
		public static int SmoothFPS
		{
			get
			{
				float fps = 0;
				for (int i = 0; i < SMOOTH_FPS_SAMPLES; i++)
				{
					fps += Instance.fpsSamples[i];
				}
				return (int)Math.Round(fps / (float)SMOOTH_FPS_SAMPLES);
			}
		}
		public static float Time { get { return Instance.time; } }
		#endregion
	}
}