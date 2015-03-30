using System;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public abstract class AbstractElement : IElement
	{
		public readonly int ID;
		public abstract Vector2f Position { get; set; }

		public bool IsVisible { get; set; }
		public abstract void Draw(RenderWindow window);

		public AbstractElement()
		{
			IsVisible = true;
			ID = GUI.AddElement(this);
		}

		public virtual void Dispose()
		{
			GUI.RemoveElement(ID);
		}
	}
}