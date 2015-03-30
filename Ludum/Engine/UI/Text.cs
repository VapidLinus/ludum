using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public class Text : AbstractElement
	{
		private SFML.Graphics.Text text;

		public override Vector2f Position
		{
			get { return text.Position; }
			set { text.Position = value; }
		}

		public Text(Vector2f position, Font font, string text, uint size)
		{
			this.text = new SFML.Graphics.Text(text, font, size);
		}

		public override void Draw(RenderWindow window)
		{
			Render.Window.Draw(text);
		}
	}
}