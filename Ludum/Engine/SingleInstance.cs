using System;

namespace Ludum.Engine
{
	public abstract class SingleInstance<T> where T : SingleInstance<T>
	{
		private static T instance;
		protected static T Instance 
		{
			get 
			{
				if (instance == null) throw new InvalidOperationException("Not initliazed");
				return instance;
			}
		}
		protected void SetInstance(T instance)
		{
			if (SingleInstance<T>.instance != null) throw new InvalidOperationException("Already initialized");
			SingleInstance<T>.instance = instance;
		}
	}
}