using System;
using Ludum.Engine.Resources;

namespace Ludum.Engine
{
	public class Transform : Component
	{
		private Vector2 position = Vector2.Zero;
		public Vector2 Position
		{
			get { return position; }
			set
			{
				if (value == null) throw new ArgumentNullException("position");
				position = value;
			}
		}

		private float rotation = 0;
		public float Rotation { get { return rotation; } set { rotation = value; } }
	}
}