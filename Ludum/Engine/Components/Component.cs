﻿using System;

namespace Ludum.Engine
{
	public abstract class Component : Behaviour
	{
		private GameObject gameObject;
		public GameObject GameObject
		{
			get { return gameObject; }
			set
			{
				if (gameObject != null) throw new InvalidOperationException("Setting gameobject is not allowed.");
				gameObject = value;
			}
		}
		public Transform Transform
		{
			get { return gameObject.Transform; }
		}

		protected internal Component() { }

		public virtual void OnAwake() { }
	}
}