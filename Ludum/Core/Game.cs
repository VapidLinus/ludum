using System;
using System.Collections.Generic;
using Ludum.Engine;
using Ludum.Engine.Resources;
using SFML.Graphics;
using Transform = Ludum.Engine.Transform;

namespace Ludum.Core
{
	class Game : CoreGame
	{
		private readonly Random random = new Random();

		private float delay = 1000;
		private float timer = 0;

		private readonly Dictionary<GameObject, Vector2> velocity = new Dictionary<GameObject, Vector2>();
		private readonly List<GameObject> gameObjects = new List<GameObject>();

		protected override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);

			timer += delta;

			if (timer >= delay)
			{
				timer -= delay;
				delay -= delta;
				Spawn();
			}

			foreach (var gameObject in gameObjects)
			{
				velocity[gameObject] += new Vector2((float)random.NextDouble() - .5f, (float)random.NextDouble() - .5f) * delta * .001f * (float)random.NextDouble();
				var transform = gameObject.GetComponent<Transform>();
				transform.Position += velocity[gameObject] * delta;
				if ((new Vector2(400, 220) - transform.Position).Magnitude() > 300)
				{
					transform.Position = new Vector2(400, 220);
				}
			}
		}

		private void Spawn()
		{
			var go = new GameObject(new Vector2(400, 220));
			go.AddComponent<ShapeRenderer>();
			go.GetComponent<ShapeRenderer>().SetShape(new CircleShape(random.Next(10, 40), (uint)random.Next(3, 16)));
			gameObjects.Add(go);
			velocity.Add(go, Vector2.Zero);

			if (gameObjects.Count % 100 == 0)
				Console.WriteLine("Objects: " + gameObjects.Count);
		}
	}
}