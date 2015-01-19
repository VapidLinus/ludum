namespace Ludum.Engine
{
	public abstract class Behaviour
	{
		public virtual void OnStart() { }
		public virtual void OnUpdate(double delta) { }
		public virtual void OnRender() { }
		public virtual void OnDestroy() { }
	}
}