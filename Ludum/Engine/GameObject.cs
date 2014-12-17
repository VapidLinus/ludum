using System;
using System.Collections.Generic;
using System.Linq;
using Ludum.Engine.Resources;

namespace Ludum.Engine
{
	public delegate void DestroyedEventHandler(GameObject sender);

	public sealed class GameObject : Behaviour
	{
		private readonly Transform transform;
		private readonly Dictionary<Component, bool> components;

		public IReadOnlyCollection<Component> Components
		{
			get
			{
				var list = new List<Component> { transform };
				list.AddRange(this.components.Keys.ToList());
				return list.AsReadOnly();
			}
		}

		public event DestroyedEventHandler Destroyed;
		private bool isDestroyed;

		public GameObject() : this(Vector2.Zero) { }

		public GameObject(Vector2 position)
		{
			transform = new Transform() { Position = position };
			components = new Dictionary<Component, bool>();

			Application.Scene.RegisterGameObject(this);
		}

		public T AddComponent<T>() where T : Component, new()
		{
			// Instantiate
			var component = new T { GameObject = this };
			if (component is Transform) throw new InvalidOperationException("Can't add Transform component");

			// Add
			components.Add(component, false);
			return component;
		}

		public T GetComponent<T>() where T : Component
		{
			return Components.OfType<T>().Select(component => component as T).FirstOrDefault();
		}

		public override void OnUpdate(float delta)
		{
			// Call start in newly added components
			foreach (var keyValuePair in components.ToArray().Where(pair => !pair.Value))
			{
				keyValuePair.Key.OnStart();
				components[keyValuePair.Key] = true;
			}

			// Update components
			foreach (var component in Components)
			{
				component.OnUpdate(delta);
			}
		}

		public override void OnRender()
		{
			// Render components
			foreach (var component in Components)
			{
				component.OnRender();
			}
		}

		public void Destroy()
		{
			// Can only destroy once
			if (isDestroyed) return;
			isDestroyed = true;

			Destroyed(this);

			// Destroy
			OnDestroy();
		}
	}
}