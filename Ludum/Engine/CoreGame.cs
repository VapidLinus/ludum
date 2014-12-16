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
				if ((time += delta) > 1000)
				{
					Console.WriteLine("FPS: " + frames);
					time = 0;
					frames = 0;
				}

				// Handle window
				Render.Window.DispatchEvents();
				Render.Window.Clear(new Color(0, 150, 255));
				Render.Window.Display();

				// Update
				OnUpdate((float)delta);
				OnRender();
			}

			// Exit
			OnUnloadContent();
			OnExit();
		}

		public virtual void OnInitialize()
		{
			Render.OnInitialize();
		}
		public virtual void OnLoadContent() { }
		public virtual void OnUpdate(float delta) { }
		public virtual void OnRender() { }
		public virtual void OnUnloadContent() { }
		public virtual void OnExit() { }
	}
}