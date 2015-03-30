using Ludum.Engine;
using System.Collections.Generic;

namespace Ludum.UI
{
	public class GUI : SingleInstance<GUI>
	{
		private int id = 0;

		private Dictionary<int, IElement> elements;

		public GUI()
		{
			SetInstance(this);
			elements = new Dictionary<int, IElement>();
		}

		internal void Render()
		{
			foreach (IElement element in elements.Values)
			{
				if (element.IsVisible)
					element.Draw(Engine.Render.Window);
			}
		}

		#region Static
		internal static int AddElement(IElement element)
		{
			Instance.elements.Add(Instance.id, element);
			return Instance.id++;
		}

		internal static void RemoveElement(int id)
		{
			Instance.elements.Remove(id);
		}

		public static T GetElement<T>(int id)
		{
			return (T)Instance.elements[id];
		}
		#endregion
	}
}