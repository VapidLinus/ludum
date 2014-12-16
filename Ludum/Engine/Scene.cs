using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum.Engine
{
	class Scene : Behaviour
	{
		private Dictionary<GameObject, bool> gameObjects;
		public IReadOnlyCollection<GameObject> GameObjects
		{
			get
			{
				return gameObjects.Keys.ToList().AsReadOnly();
			}
		}

		public Scene()
		{
			gameObjects = new Dictionary<GameObject, bool>();
		}

		/// <summary>
		/// Registers an object to the scene.
		/// Should never be manually called.
		/// </summary>
		/// <param name="gameObject"></param>
		public void RegisterGameObject(GameObject gameObject)
		{
			if (gameObjects.ContainsKey(gameObject)) throw new InvalidOperationException("Never manually register a game object.");

			// Add to list and listen to destroyed event
			gameObjects.Add(gameObject, false);
			gameObject.Destroyed += new DestroyedEventHandler(OnGameObjectDestroyed);
		}

		/// <summary>
		/// Called first frame after scene gets created
		/// </summary>
		public override void OnStart()
		{
			
		}

		/// <summary>
		/// Called each frame
		/// </summary>
		/// <param name="delta"></param>
		public override void OnUpdate(float delta)
		{
			foreach (KeyValuePair<GameObject, bool> pair in gameObjects)
			{
				// If start hasn't been called
				if (!pair.Value)
				{
					// Call start
					pair.Key.OnStart();
					gameObjects.Add(pair.Key, true);
				}
			}

			// Update
			foreach (GameObject gameObject in GameObjects)
			{
				// TODO: Must check if OnUpdate can get called on destroyed objects
				gameObject.OnUpdate(delta);
			}
		}

		/// <summary>
		/// Called each frame after OnUpdate
		/// </summary>
		public override void OnRender()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				gameObject.OnRender();
			}
		}

		/// <summary>
		/// Called when the scene gets unloaded and destroyed
		/// </summary>
		public override void OnDestroy()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				gameObject.Destroy();
			}
		}

		private void OnGameObjectDestroyed(GameObject gameObject)
		{
			// Remove from list and stop listening to destroy event
			gameObjects.Remove(gameObject);
			gameObject.Destroyed -= new DestroyedEventHandler(OnGameObjectDestroyed);
		}
	}
}