namespace Ludum.Engine
{
	public abstract class Behaviour
	{
		public virtual void OnStart() { }
		public virtual void OnUpdate(float delta) { }
		public virtual void OnRender() { }
		protected virtual void OnDestroy() { }
	}
}