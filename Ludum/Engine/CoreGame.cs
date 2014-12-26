using System;
using System.Diagnostics;
using SFML.Graphics;

namespace Ludum.Engine
{
	abstract class CoreGame
	{
		private const int SMOOTH_FPS_SAMPLES = 50;
		private int fpsCurrentSample = 0;
		private float[] fpsSamples = new float[SMOOTH_FPS_SAMPLES];

		public int SmoothFPS
		{
			get
			{
				float fps = 0;
				for (int i = 0; i < SMOOTH_FPS_SAMPLES; i++)
				{
					fps += fpsSamples[i];
				}
				return (int)Math.Round(fps / (float)SMOOTH_FPS_SAMPLES);
			}
		}

		public void Run()
		{
			// Load
			Console.Write("Initializing... ");
			OnInitialize();
			Console.WriteLine("Done!");
			Console.Write("Loading content... ");
			OnLoadContent();
			Console.WriteLine("Done!");

			// Update
			var timer = Stopwatch.StartNew();
			float time = 0;
			while (Render.Window.IsOpen())
			{
				// Record delta
				float delta = timer.ElapsedTicks / (float)Stopwatch.Frequency;

				timer.Restart();

				fpsSamples[fpsCurrentSample++] = 1f / (float)delta;
				if (fpsCurrentSample >= SMOOTH_FPS_SAMPLES) fpsCurrentSample = 0;

				// Count fps
				if ((time += delta) >= .1f)
				{
					time = 0;
					Console.WriteLine("FPS: " + SmoothFPS);
				}

				// Handle window
				Render.Window.DispatchEvents();

				// Update
				OnUpdate(delta);

				// Render
				Render.Window.Clear(new Color(0, 150, 255));
				OnRender();
				Render.Window.Display();
			}

			// Exit
			OnUnloadContent();
			OnExit();
		}

		protected virtual void OnInitialize()
		{
			Render.OnInitialize();
		}

		protected virtual void OnUpdate(float delta)
		{
			Application.Scene.OnUpdate(delta);
		}

		protected virtual void OnRender()
		{
			Application.Scene.OnRender();
		}

		protected virtual void OnExit()
		{
			Application.Scene.OnDestroy();
		}

		protected virtual void OnLoadContent() { }
		protected virtual void OnUnloadContent() { }
	}
}