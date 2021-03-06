﻿namespace Ludum.Engine
{
	public class Transform : Component
	{
		private Vector2 position = Vector2.Zero;
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}
		
		private Vector2 scale = Vector2.One;
		public Vector2 Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		private float rotation = 0;
		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		private Vector2 lastPosition;
		public Vector2 LastPosition
		{
			get { return lastPosition; }
			internal set { lastPosition = value; }
		}

		public string Name
		{
			get { return GameObject.Name; }
			set { GameObject.Name = value; }
		}

		public Vector2 RenderPosition
		{
			get { return position * Render.FrameAlpha + lastPosition * (1.0 - Render.FrameAlpha); }
		}

		public override void OnAwake()
		{
			LastPosition = Transform.position;
		}

		public override void OnStart()
		{
			LastPosition = Transform.position;
		}

		public override string ToString()
		{
			return Name + GameObject.instanceID;
		}
	}
}