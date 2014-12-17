using System;
using System.Diagnostics;
using SFML.Graphics;

namespace Ludum.Engine
{
	abstract class CoreGame
	{
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
			double time = 0;
			int frames = 0;
			while (Render.Window.IsOpen())
			{
				// Record delta
				double delta = timer.ElapsedMilliseconds;
				timer.Restart();

				frames++;

				// Count fps
				if ((time += delta) >= 1000)
				{
					Console.WriteLine("FPS: " + frames);
					time -= 1000;
					frames = 0;
				}

				// Handle window
				Render.Window.DispatchEvents();

				// Update
				OnUpdate((float)delta);

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