using System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace Ludum.Engine
{
	public class Render : SingleInstance<Render>
	{
		// Render layer constants
		public const byte LOWEST_RENDER_LAYER = 0;
		public const byte DEFAULT_RENDER_LAYER = 128;
		public const byte HIGHEST_RENDER_LAYER = byte.MaxValue;

		// References
		private readonly Core core;
		private readonly RenderWindow window;

		// Time
		private double time = 0;
		private double delta = 0;
		private double maxDelta = 1 / 60.0;

		// FPS
		private const int SMOOTH_FPS_SAMPLES = 50;
		private int fpsCurrentSample = 0;
		private double[] fpsSamples = new double[SMOOTH_FPS_SAMPLES];
		private double frameAlpha = 0;

		// Render list
		private List<GameObject> renderList;
		private bool isRenderListDirty = true;

		private VideoMode LoadVideoMode()
		{
			uint width = 800;
			uint height = 450;

			try
			{
				width = uint.Parse(Application.Config.GetValue("screen-resolution-x", "800"));
				height = uint.Parse(Application.Config.GetValue("screen-resolution-y", "450"));
			}
			catch (Exception e)
			{
				e.PrintStackTrace();
			}

			return new VideoMode(width, height);
		}

		private Styles LoadStyles()
		{
			bool isFullscreen = false;

			try
			{
				isFullscreen = bool.Parse(Application.Config.GetValue("screen-resolution-fullscreen", "false"));
			}
			catch (Exception e)
			{
				e.PrintStackTrace();
			}

			return isFullscreen ? Styles.Fullscreen : Styles.Close;
		}

		internal Render(Core core)
		{
			var renderMode = new RenderMode(LoadVideoMode(), "Ludum", LoadStyles());

			ContextSettings settings = new ContextSettings();
			settings.AntialiasingLevel = 4;

			SetInstance(this);

			this.core = core;

			window = new RenderWindow(renderMode.videoMode, renderMode.title, renderMode.style, settings);
			window.SetVerticalSyncEnabled(false);
			window.Closed += (sender, e) => ((RenderWindow)sender).Close();

			RebuildRenderOrder();
		}

		internal void ReportDelta(double newDelta)
		{
			delta = newDelta;
			time += delta;

			fpsSamples[fpsCurrentSample++] = 1 / delta;
			if (fpsCurrentSample >= SMOOTH_FPS_SAMPLES) fpsCurrentSample = 0;
		}

		internal void ReportFrameAlpha(double alpha)
		{
			frameAlpha = alpha;
		}

		internal void RenderAll()
		{
			// If render list needs to be rebuilt
			if (isRenderListDirty)
			{
				isRenderListDirty = false; // No longer dirty

				// Prepare sort
				List<GameObject> objects = new List<GameObject>(Application.Scene.GameObjects);
				renderList = new List<GameObject>();

				// Sort
				for (byte i = 0; i < HIGHEST_RENDER_LAYER; i++)
				{
					for (int j = objects.Count - 1; j >= 0; j--)
					{
						if (objects[j].RenderLayer == i)
						{
							renderList.Add(objects[j]);
							objects.RemoveAt(j);
						}
					}
				}
			}

			// Render
			foreach (var gameObject in renderList)
			{
				gameObject.OnRender();
			}
		}

		#region Static
		internal static void RebuildRenderOrder()
		{
			Instance.isRenderListDirty = true;
		}

		public static int FPS { get { return (int)Math.Round(Instance.fpsSamples[Instance.fpsCurrentSample]); } }
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
		public static double Delta
		{
			get
			{
				return Instance.core.updateState == Core.UpdateState.FixedUpdate ? FixedDelta :
					Math.Min(RealDelta, Instance.maxDelta);
			}
		}
		public static double RealDelta { get { return Instance.delta; } }
		public static double FixedDelta
		{
			get { return Instance.core.fixedDelta; }
			set { Instance.core.fixedDelta = Math.Max(value, float.Epsilon); }
		}
		public static double MaxDelta
		{
			get { return Instance.maxDelta; }
			set { Instance.maxDelta = Math.Max(value, 0); }
		}
		public static double FrameAlpha { get { return Instance.frameAlpha; } }

		public static void DrawRectangle(Vector2 point1, Vector2 point2, SFML.Graphics.Color color)
		{
			point1.y = -point1.y;
			point2.y = -point2.y;

			var shape = new RectangleShape((Vector2f)new Vector2(point2.x - point1.x, point2.y - point1.y));
			var camera = Camera.Main;

			float zoom = (float)camera.Zoom;

			shape.Scale = new Vector2f(zoom, zoom);
			shape.Origin = shape.Size * 0.5f;
			shape.FillColor = color;
			shape.Position = new Vector2f(
				WindowWidth * 0.5f + (float)((point1.x + point2.x) / 2f - camera.Transform.Position.x) * zoom,
				WindowHeight * 0.5f + (float)((point1.y + point2.y) / 2f + camera.Transform.Position.y) * zoom);

			Window.Draw(shape);
		}

		public static RenderWindow Window { get { return Instance.window; } }
		public static int WindowWidth { get { return (int)Window.Size.X; } }
		public static int WindowHeight { get { return (int)Window.Size.Y; } }
		#endregion
	}
}