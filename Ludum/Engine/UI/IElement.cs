using SFML.Graphics;
using SFML.Window;
using System;

namespace Ludum.UI
{
	interface IElement : IDisposable, IComparable
	{
		bool IsVisible { get; set; }
		Vector2f Position { get; set; }
		byte Layer { get; set; }
		void Update();
		void Draw(RenderWindow window);

		bool OnClick(Vector2f position);
    }
}