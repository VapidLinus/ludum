namespace Ludum.Engine
{
	public delegate void DestroyedEventHandler(GameObject sender);

	public class GameObject : Behaviour
	{
		public event DestroyedEventHandler Destroyed;

		private bool IsDestroyed { get; set; }

		public GameObject()
		{
			IsDestroyed = false;
		}

		public void Destroy()
		{
			// Can only destroy once
			if (IsDestroyed) return;
			IsDestroyed = true;

			Destroyed(this);

			// Destroy
			OnDestroy();
		}
	}
}