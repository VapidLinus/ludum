using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludum.Engine
{
	public sealed class GameObject : Behaviour
	{
		public readonly Transform Transform;
		private readonly Dictionary<Component, bool> components;
		private byte renderLayer = Render.DEFAULT_RENDER_LAYER;

		public string Name { get; set; }

		public byte RenderLayer
		{
			get { return renderLayer; }
			set
			{
				renderLayer = MathUtil.Clamp(value, Render.LOWEST_RENDER_LAYER, Render.HIGHEST_RENDER_LAYER);
				Render.RebuildRenderOrder();
			}
		}
		public IReadOnlyCollection<Component> Components
		{
			get
			{
				var list = new List<Component> { Transform };
				list.AddRange(this.components.Keys.ToList());
				return list.AsReadOnly();
			}
		}
		public Vector2 Position
		{
			get { return Transform.Position; }
			set { Transform.Position = value; }
		}

		public GameObject(string name, Vector2 position = new Vector2())
		{
			Name = name;

			Transform = new Transform() { GameObject = this,  Position = position };
			Transform.LastPosition = Transform.Position;
			components = new Dictionary<Component, bool>();

			Application.Scene.RegisterGameObject(this);
		}

		public T AddComponent<T>() where T : Component, new()
		{
			// Instantiate
			var component = new T { GameObject = this };
			if (component is Transform) throw new InvalidOperationException("Can't add Transform component");
			component.OnAwake();
			component.OnDestroyHandler += OnComponentDestroyed;

			// Add
			components.Add(component, false);
			return component;
		}

		public T GetOrAddComponent<T>() where T : Component, new()
		{
			T component = GetComponent<T>();
			return component == null ? AddComponent<T>() : component;
		}

		public T GetComponent<T>() where T : Component
		{
			return Components.OfType<T>().Select(component => component as T).FirstOrDefault();
		}

		public override void OnFixedUpdate()
		{
			// Loop through all components
			Dictionary<Component, bool> componentsClone = new Dictionary<Component, bool>(components);	 // Clone
			foreach (var pair in componentsClone)
			{
				var component = pair.Key;

				// If not initialized
				if (!pair.Value)
				{
					// Initialize
					component.OnStart();
					components[component] = true;
				}
				else component.OnFixedUpdate();
			}
		}

		public override void OnUpdate()
		{
			// Update components
			foreach (var component in Components)
			{
				component.OnUpdate();
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

		public override void OnDestroy()
		{
			// Destroy components
			foreach (var component in Components)
			{
				component.Destroy();
			}
		}

		void OnComponentDestroyed(Behaviour component)
		{
			// Unregister event
			component.OnDestroyHandler -= OnComponentDestroyed;

			// Destroy component
			components.Remove((Component)component);
		}

		public override string ToString()
		{
			return Name + instanceID;
		}

		public static T Create<T>(string name, Vector2 position = new Vector2()) where T : Component, new()
		{
			return new GameObject(name, position).AddComponent<T>();
		}
	}
}