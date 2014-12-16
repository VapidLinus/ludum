using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum.Engine
{
	public abstract class Behaviour
	{
		public virtual void OnStart() { }
		public virtual void OnUpdate(float delta) { }
		public virtual void OnRender() { }
		public virtual void OnDestroy() { }
	}
}