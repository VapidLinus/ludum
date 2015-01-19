using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.TestGame
{
	public class Wall : GameObject
	{
		public Wall()
		{
			AddComponent<RectangleRenderer>();
			AddComponent<BoxCollider>();
		}
	}
}