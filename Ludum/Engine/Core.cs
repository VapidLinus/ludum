using System;
using System.Diagnostics;
using SFML.Graphics;

namespace Ludum.Engine
{
	abstract class Core
	{


		private readonly Render render;
		private readonly Application application;
		private readonly Input input;

		public Core()
		{
			render = new Render(this);
			application = new Application();
			input = new Input();
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

				render.ReportDelta((float)delta);

				// Display fps
				if ((time += delta) >= .1f)
				{
					time = 0;
					Console.WriteLine("FPS: " + Render.SmoothFPS);
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

		protected virtual void OnInitialize() { }
		protected virtual void OnUpdate(float delta)
		{
			Application.Scene.OnUpdate(delta);
			input.Update();
		}
		protected virtual void OnRender() { Application.Scene.OnRender(); }
		protected virtual void OnExit() { Application.Scene.OnDestroy(); }
		protected virtual void OnLoadContent() { }
		protected virtual void OnUnloadContent() { }
	}
}