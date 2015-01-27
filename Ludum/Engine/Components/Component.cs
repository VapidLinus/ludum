using System;

namespace Ludum.Engine
{
	public abstract class Component : Behaviour
	{
		private GameObject gameObject;
		public GameObject GameObject
		{
			get { return gameObject; }
			internal set
			{
				if (gameObject != null) throw new InvalidOperationException("Setting gameobject is not allowed.");
				gameObject = value;
			}
		}
		public Transform Transform
		{
			get { return gameObject.Transform; }
		}

		protected Component() { }
		

		public virtual void OnAwake() { }
	}
}