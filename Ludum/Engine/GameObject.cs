using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum.Engine
{
	public delegate void DestroyedEventHandler(GameObject sender);

	public class GameObject : Behaviour
	{
		public event DestroyedEventHandler Destroyed;

		private bool isDestroyed = false;
		public bool IsDestroyed { get { return isDestroyed; } }

		public void Destroy()
		{
			// Can only destroy once
			if (isDestroyed) return;

			Destroyed(this);

			// Destroy
			OnDestroy();
		}
	}
}