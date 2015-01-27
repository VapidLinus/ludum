namespace Ludum.Engine
{
	public delegate void OnDestroyHandler(Behaviour behaviour);

	public abstract class Behaviour
	{
		private static int nextID = 0;
		private int instanceID;

		public event OnDestroyHandler OnDestroyHandler;

		private bool isDestroyed = false;
		public bool IsDestroyed
		{
			get { return isDestroyed; }
			internal set { isDestroyed = value; }
		}

		public Behaviour()
		{
			instanceID = nextID++;
        }

		/// <summary>
		/// Destroys the behaviour if it's not already destroyed.
		/// </summary>
		/// <returns>Whether the behaviour is already destroyed.</returns>
		public bool Destroy()
		{
			if (isDestroyed) return false;

			// Invoke event
			if (OnDestroyHandler != null)
				OnDestroyHandler(this);

			return true;
		}

		public virtual void OnStart() { }
		public virtual void OnUpdate() { }
		public virtual void OnRender() { }
		public virtual void OnDestroy() { }

		public static bool operator ==(Behaviour b1, Behaviour b2)
		{
			bool b1Null = ReferenceEquals(b1, null);
			bool b2Null = ReferenceEquals(b2, null);

			// Both null
			if (b1Null && b2Null) return true;

			// None null
			if (!b1Null && !b2Null)
			{
				// If same behaviour
				if (b1.instanceID == b2.instanceID)
				{
					// Return true if not destroyed
					return !b1.IsDestroyed;
				}
				return false;
			}

			// One is null
			if (b1Null) return b2.IsDestroyed;
			else return b1.IsDestroyed;
		}

		public static bool operator !=(Behaviour b1, Behaviour b2)
		{
			return !(b1 == b2);
		}
    }
}