using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.Engine
{
	public class Render : SingleInstance<Render>
	{
		private readonly Core core;
		private readonly RenderWindow window;

		private double time = 0;

		private const int SMOOTH_FPS_SAMPLES = 50;
		private int fpsCurrentSample = 0;
		private double[] fpsSamples = new double[SMOOTH_FPS_SAMPLES];

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

		internal void ReportDelta(double delta)
		{
			time += delta;

			fpsSamples[fpsCurrentSample++] = 1 / delta;
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
		public static int WindowWidth { get { return (int)Window.Size.X; } }
		public static int WindowHeight { get { return (int)Window.Size.Y; } }
		public static int SmoothFPS
		{
			get
			{
				double fps = 0;
				for (int i = 0; i < SMOOTH_FPS_SAMPLES; i++)
				{
					fps += Instance.fpsSamples[i];
				}
				return (int)Math.Round(fps / (double)SMOOTH_FPS_SAMPLES);
			}
		}
		public static double Time { get { return Instance.time; } }
		public static void DrawRectangle(Vector2 point1, Vector2 point2)
		{
			point1.y = -point1.y;
			point2.y = -point2.y;

			var shape = new RectangleShape((Vector2f)new Vector2(point2.x - point1.x, point2.y - point1.y));
			var camera = Application.Scene.Camera;

			float zoom = (float)camera.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.FillColor = new Color(0, 255, 0);
			shape.Position = new Vector2f(
				Render.WindowWidth * 0.5f + (float)((point1.x + point2.x) / 2f - camera.Position.x) * zoom,
				Render.WindowHeight * 0.5f + (float)((point1.y + point2.y) / 2f - camera.Position.y) * zoom);

			Render.Window.Draw(shape);
		}
		#endregion
	}
}