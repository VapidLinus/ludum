using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;

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
			Stopwatch timer = Stopwatch.StartNew();
			Stopwatch fpsHelper = Stopwatch.StartNew();
			int frames = 0;
			while (Render.Window.IsOpen())
			{
				frames++;

				// Count fps
				if (fpsHelper.ElapsedMilliseconds > 1000)
				{
					Console.WriteLine("FPS: " + frames);
					fpsHelper.Restart();
					frames = 0;
				}

				// Record delta
				float delta = timer.ElapsedMilliseconds;
				timer.Restart();

				// Handle window
				Render.Window.DispatchEvents();
				Render.Window.Clear(new Color(0, 150, 255));
				Render.Window.Display();

				// Update
				OnUpdate(delta);
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