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

		public GameObject() : this(Vector2.Zero) { }

		public GameObject(Vector2 position)
		{
			Transform = new Transform() { Position = position };
			components = new Dictionary<Component, bool>();

			Application.Scene.RegisterGameObject(this);
		}

		public T AddComponent<T>() where T : Component, new()
		{
			// Instantiate
			var component = new T { GameObject = this };
			if (component is Transform) throw new InvalidOperationException("Can't add Transform component");
			component.OnAwake();

			// Add
			components.Add(component, false);
			return component;
		}

		public T GetComponent<T>() where T : Component
		{
			return Components.OfType<T>().Select(component => component as T).FirstOrDefault();
		}

		public override void OnUpdate()
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
	}
}