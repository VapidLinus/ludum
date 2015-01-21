using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludum.Engine
{
	public sealed class Scene : Behaviour
	{
		private Camera camera;
		public Camera Camera { get { return camera == null ? camera = new Camera() : camera; } }

		private readonly Dictionary<GameObject, bool> gameObjects;
		public IReadOnlyCollection<GameObject> GameObjects { get { return gameObjects.Keys.ToList().AsReadOnly(); } }

		private bool isInitialized;

		public bool IsDestroyed { get; private set; }

		public Scene()
		{
			gameObjects = new Dictionary<GameObject, bool>();
		}

		public void RegisterGameObject(GameObject gameObject)
		{
			if (gameObjects.ContainsKey(gameObject)) throw new InvalidOperationException("Never manually register a game object.");

			// Add to list and listen to destroyed event
			gameObjects.Add(gameObject, false);
			gameObject.Destroyed += new DestroyedEventHandler(OnGameObjectDestroyed);
		}

		public override void OnUpdate()
		{
			if (!isInitialized)
			{
				OnStart();
				isInitialized = true;
			}

			// Call start in newly created game objects
			foreach (var keyValuePair in gameObjects.ToArray().Where(pair => !pair.Value))
			{
				keyValuePair.Key.OnStart();
				gameObjects[keyValuePair.Key] = true;
			}

			// Update
			GameObject[] objects = GameObjects.ToArray();
			for (int i = 0; i < objects.Length; i++)
			{
				// TODO: Must check if OnUpdate can get called on destroyed oboects
				objects[i].OnUpdate();
			}
		}

		public override void OnRender()
		{
			foreach (var gameObject in GameObjects)
			{
				gameObject.OnRender();
			}
		}

		public override void OnDestroy()
		{
			IsDestroyed = true;

			foreach (var gameObject in GameObjects)
			{
				gameObject.Destroy();
			}
		}

		private void OnGameObjectDestroyed(GameObject gameObject)
		{
			// Remove from list and stop listening to destroy event
			gameObjects.Remove(gameObject);
			gameObject.Destroyed -= OnGameObjectDestroyed;
		}
	}
}