using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludum.Engine
{
	public delegate void GameObjectCreatedHandler(GameObject created);
	public delegate void GameObjectDestroyedHandler(GameObject destroyed);

	public sealed class Scene : Behaviour
	{
		public event GameObjectCreatedHandler GameObjectCreatedHandler;
		public event GameObjectDestroyedHandler GameObjectDestroyedHandler;

		private readonly Dictionary<GameObject, bool> gameObjects;
		public IReadOnlyCollection<GameObject> GameObjects { get { return gameObjects.Keys.ToList().AsReadOnly(); } }

		private bool isInitialized;

		public Scene()
		{
			gameObjects = new Dictionary<GameObject, bool>();
		}

		public void RegisterGameObject(GameObject gameObject)
		{
			if (gameObjects.ContainsKey(gameObject)) throw new InvalidOperationException("Never manually register a game object.");

			// Add to list and listen to destroy event
			gameObjects.Add(gameObject, false);
			Render.RebuildRenderOrder();
			gameObject.OnDestroyHandler += new OnDestroyHandler(OnGameObjectDestroyed);
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
				var gameObject = keyValuePair.Key;

				if (GameObjectCreatedHandler != null)
					GameObjectCreatedHandler(gameObject);
				gameObject.OnStart();
				gameObjects[gameObject] = true;
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
			
		}

		public override void OnDestroy()
		{
			IsDestroyed = true;

			foreach (var gameObject in GameObjects)
			{
				gameObject.Destroy();
			}
		}

		private void OnGameObjectDestroyed(Behaviour behaviour)
		{
			Render.RebuildRenderOrder();

			// We know the behaviour is a gameobject, as we only listen
			// to the event when creating game objects
			GameObject gameObject = behaviour as GameObject;

			// Remove from list and stop listening to behaviour's destroy event
			gameObjects.Remove(gameObject);
			gameObject.OnDestroyHandler -= OnGameObjectDestroyed;

			// Invoke gameobject destroyed event
			if (GameObjectDestroyedHandler != null)
				GameObjectDestroyedHandler(gameObject);
		}
	}
}