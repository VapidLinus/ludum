using SFML.Graphics;
using SFML.Window;
using System;

namespace Ludum.UI
{
	interface IElement : IDisposable
	{
		bool IsVisible { get; set; }
		Vector2f Position { get; set; }
		void Draw(RenderWindow window);
	}
}