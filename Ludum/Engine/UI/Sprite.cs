using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public class Sprite : AbstractElement
	{
		private RectangleShape rectangle;

		public override Vector2f Position { get { return rectangle.Position; } set { rectangle.Position = value; } }
		public Vector2f Size { get { return rectangle.Size; } }

		public Sprite(Vector2f position, Texture texture) :
			this(position, new Vector2f(texture.Size.X, texture.Size.Y), texture)
		{ }

		public Sprite(Vector2f position, float scale, Texture texture) :
			this(position, new Vector2f(texture.Size.X * scale, texture.Size.Y * scale), texture)
		{ }

		public Sprite(Vector2f position, Vector2f size, Texture texture)
		{
			rectangle = new RectangleShape(size);
			rectangle.Texture = texture;
			Position = position;
		}

		public override void Draw(RenderWindow window)
		{
			window.Draw(rectangle);
		}

		public override void Dispose()
		{
			base.Dispose();

			rectangle.Texture.Dispose();
			rectangle.Dispose();
		}
	}
}