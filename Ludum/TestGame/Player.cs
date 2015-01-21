using System;
using Ludum.Engine;
using SFML.Window;

namespace Ludum.TestGame
{
	public class Player : GameObject
	{
		private static Random random = new Random();

		private Keyboard.Key keyUp, keyDown, keyLeft, keyRight;

		private Vector2 velocity = Vector2.Zero;

		private RectangleRenderer renderer;
		private BoxCollider collider;

		private bool doubleJumped = true;

		public Player(int player)
		{
			switch (player)
			{
				case 0:
					keyUp = Keyboard.Key.W;
					keyDown = Keyboard.Key.S;
					keyLeft = Keyboard.Key.A;
					keyRight = Keyboard.Key.D;
					break;
				case 1:
					keyUp = Keyboard.Key.Up;
					keyDown = Keyboard.Key.Down;
					keyLeft = Keyboard.Key.Left;
					keyRight = Keyboard.Key.Right;
					break;
			}
		}

		public override void OnStart()
		{
			renderer = AddComponent<RectangleRenderer>();
			collider = AddComponent<BoxCollider>();

			renderer.Size = collider.Size = Vector2.One * .8;

			// renderer.Radius = 2f;
			// renderer.Size = collider.Size = Vector2.One * 40;
		}

		public override void OnUpdate()
		{
			double delta = Render.Delta;

			// Input
			double inputX = 0f;
			if (Input.IsKeyDown(keyLeft))
			{
				inputX--;
			}
			if (Input.IsKeyDown(keyRight))
			{
				inputX++;
			}
			velocity.x += MathUtil.Clamp(inputX, -1, 1) * .08;

			if (Input.IsKeyPressed(keyUp))
			{
				if ((doubleJumped) || collider.Overlap(Position + Vector2.Down * .1) != null)
				{
					velocity.y = 8;
					doubleJumped = !doubleJumped;
				}
			}

			// Friction and gravity
			velocity.x *= Math.Pow(0.00001, delta);
			velocity.y -= delta * 24;

			// Collision Y
			Collider other;
			double nextY = Position.y + velocity.y * delta;
			if ((other = collider.Overlap(new Vector2(Position.x, nextY))) != null)
			{
				if (velocity.y < 0) Position = new Vector2(Position.x, other.Top.y + collider.Size.y * .5);
				velocity.y = 0;
				doubleJumped = false;
			}

			// Collision X
			Collider otherX;
			if (Math.Abs(velocity.x) > 0 && (otherX = collider.Overlap(Position + Vector2.Right * velocity.x * delta)) != null)
			{
				velocity.x = 0;
			}

			Position += velocity * delta;

			if (false && Input.IsKeyPressed(keyUp))
				System.Threading.Thread.Sleep(1000);
		}
	}
}