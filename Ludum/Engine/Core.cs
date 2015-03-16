﻿using System;
using System.Diagnostics;

namespace Ludum.Engine
{
	public abstract class Core
	{

		internal enum UpdateState
		{
			PreFrame,
			FixedUpdate,
			Update,
			Render
		}
		internal UpdateState updateState = UpdateState.PreFrame;

		private readonly Render render;
		private readonly Application application;
		private readonly Input input;

		internal double fixedDelta = 1 / 30.0;

		public Core()
		{
			application = new Application();
			render = new Render(this);
			input = new Input(this);
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
			double time = 0;
			double displayFPSTime = 0;

			double accumulator = 0;
			while (Render.Window.IsOpen())
			{
				// Record delta
				double delta = timer.ElapsedTicks / (double)Stopwatch.Frequency;
				timer.Restart();
				render.ReportDelta(delta);

				// Fixed update
				accumulator += delta;
				updateState = UpdateState.FixedUpdate;
				while (accumulator >= fixedDelta)
				{
					Application.Scene.StoreState();
					time += delta;
					input.FixedUpdate();
					Application.Scene.OnFixedUpdate();
					accumulator -= fixedDelta;
					OnFixedUpdate();
                }

				// Get alpha from fixed update
				render.ReportFrameAlpha(accumulator / fixedDelta);

				// Display fps
				if ((displayFPSTime += delta) >= .1)
				{
					displayFPSTime = 0;
					Console.WriteLine("FPS: " + Render.SmoothFPS);
				}

				// Handle window
				Render.Window.DispatchEvents();

				// Update
				updateState = UpdateState.Update;

				input.Update();
				Application.Scene.OnUpdate();
				OnUpdate();

				// Render
				updateState = UpdateState.Render;
				Render.Window.Clear(new SFML.Graphics.Color(0, 150, 255));
				render.RenderAll();
				OnRender();
				Render.Window.Display();

				updateState = UpdateState.PreFrame;
			}

			// Exit
			OnUnloadContent();
			Application.Scene.OnDestroy();
			Application.Config.Save();
			OnExit();
		}

		protected virtual void OnInitialize() { }
		protected virtual void OnUpdate() { }
		protected virtual void OnFixedUpdate() { }
		protected virtual void OnRender() { }
		protected virtual void OnExit() { }
		protected virtual void OnLoadContent() { }
		protected virtual void OnUnloadContent() { }
	}
}