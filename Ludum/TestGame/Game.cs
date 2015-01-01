using System;
using System.Collections.Generic;
using Ludum.Engine;
using Ludum.Engine.Resources;
using SFML.Graphics;

namespace Ludum.TestGame
{
	class Game : Core
	{
		private readonly Random random = new Random();

		private float delay = 3;
		private float timer;
		private int count = 1;

		private readonly Dictionary<GameObject, Vector2> velocity = new Dictionary<GameObject, Vector2>();
		private readonly List<GameObject> gameObjects = new List<GameObject>();

		protected override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);

			timer += delta;

			if (timer >= delay)
			{
				timer = 0;
				delay *= .8f;
				if (delay <= .1f) { delay = 2; count *= 2; }
				for (int i = 0; i < count; i++) Spawn();
			}

			GameObject[] gos = gameObjects.ToArray();
			for (int i = 0; i < gos.Length; i++)
			{
				var gameObject = gos[i];
				var transform = gameObject.Transform;
				transform.Position += velocity[gameObject] * delta;
				velocity[gameObject] += Vector2.Down * delta * 200;
				if ((new Vector2(400, 220) - transform.Position).Magnitude() > 1000)
				{
					gameObject.Destroy();
					velocity.Remove(gameObject);
					gameObjects.Remove(gameObject);
				}
			}
		}

		private void Spawn()
		{
			var go = new GameObject(new Vector2(400, -220));
			go.AddComponent<ShapeRenderer>();
			go.GetComponent<ShapeRenderer>().SetShape(new CircleShape(random.Next(10, 40), (uint)random.Next(3, 16)));
			gameObjects.Add(go);
			velocity.Add(go, new Vector2(((float)random.NextDouble() - .5f) * 2, (float)random.NextDouble()).Normalized * 250 * ((float)random.NextDouble() + .5f));

			if (gameObjects.Count % 100 == 0)
				Console.WriteLine("Objects: " + gameObjects.Count);
		}
	}
}