using Ludum.UI;
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

		public T FindComponent<T>() where T : Component
		{
			foreach (var gameobject in GameObjects)
			{
				foreach (var component in gameobject.Components)
				{
					if (component is T) return (T)component;
				}
			}
			return null;
		}

		public List<T> FindComponents<T>() where T : Component
		{
			List<T> components = new List<T>();
			foreach (var gameobject in GameObjects)
			{
				foreach (var component in gameobject.Components)
				{
					if (component is T) components.Add((T)component);
				}
			}
			return components;
		}

		public override void OnFixedUpdate()
		{
			// Initialize self if not already initialized
			if (!isInitialized)
			{
				OnStart();
				isInitialized = true;
			}

			// Loop through all game objects
			Dictionary<GameObject, bool> gameObjectsClone = new Dictionary<GameObject, bool>(gameObjects); // Clone
			foreach (var pair in gameObjectsClone)
			{
				var gameObject = pair.Key;

				// If not initialized
				if (!pair.Value)
				{
					// Call created gameobject event
					if (GameObjectCreatedHandler != null)
						GameObjectCreatedHandler(gameObject);
					// Initialize
					gameObject.OnStart();
					gameObjects[gameObject] = true;
				}

				gameObject.OnFixedUpdate();
			}
		}

		public override void OnUpdate()
		{
			// Update
			GameObject[] objects = GameObjects.ToArray();
			for (int i = 0; i < objects.Length; i++)
			{
				// TODO: Must check if OnUpdate can get called on destroyed objects
				objects[i].OnUpdate();
			}
		}

		public override void OnLateUpdate()
		{
			// Update
			GameObject[] objects = GameObjects.ToArray();
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].OnLateUpdate();
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

		internal void StoreState()
		{
			foreach (var pair in gameObjects)
			{
				var gameObject = pair.Key;
				gameObject.Transform.LastPosition = gameObject.Transform.Position;
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