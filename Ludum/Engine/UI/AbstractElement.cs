using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public abstract class AbstractElement : IElement
	{
		public readonly int ID;
		private byte layer = 128;

		public byte Layer
		{
			get { return layer; }
			set { layer = value; }
		}

		public abstract Vector2f Position { get; set; }
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

		public virtual bool IsVisible { get; set; }
		public virtual void Update() { }
		public virtual bool OnClick(Vector2f position) { return false; }

		public int CompareTo(object obj)
		{
			var other = obj as AbstractElement;
			if (other != null)
			{
				return Layer - other.Layer;
			}
			return 0;
		}
	}
}