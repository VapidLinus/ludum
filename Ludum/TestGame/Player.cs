using System;
using Ludum.Engine;
using SFML.Window;
using SFML.Graphics;

namespace Ludum.TestGame
{
	public class Player : GameObject
	{
		private Keyboard.Key keyUp, keyDown, keyLeft, keyRight;

		private Vector2 velocity = Vector2.Zero;

		private RoundedBoxRenderer renderer;
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
			base.OnStart();

			renderer = AddComponent<RoundedBoxRenderer>();
			collider = AddComponent<BoxCollider>();

			renderer.Radius = 2f;
			renderer.Size = collider.Size = Vector2.One * 40;
		}

		public override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);

			// renderer.Radius = ((float)Math.Sin(Render.Time * 10) + 1f) * 5 + 4;
			collider.Size = renderer.Size =  Vector2.One * ((float)Math.Sin(Render.Time * 2) + 1f) * 10 + Vector2.One * 10;
			Console.WriteLine(renderer.Size);

			// Input
			float input = 0f;
			if (Input.IsKeyDown(keyLeft))
			{
				input--;
			}
			if (Input.IsKeyDown(keyRight))
			{
				input++;
			}
			velocity.x += MathUtil.Clamp(input, -1, 1);

			if (Input.IsKeyPressed(keyUp))
			{
				if ((doubleJumped) || collider.Overlap(Position + Vector2.Down * .1f) != null)
				{
					velocity.y = 600;
					doubleJumped = !doubleJumped;
				}
			}

			// Friction and gravity
			velocity.x *= (float)Math.Pow(0.001f, delta);
			velocity.y -= delta * 1800;

			// Collision Y
			Collider other;
			float nextY = Position.y + velocity.y * delta;
			if ((other = collider.Overlap(new Vector2(Position.x, nextY))) != null)
			{
				if (velocity.y < 0) Position = new Vector2(Position.x, other.Top.y + collider.Size.y / 2f);
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
		}
	}
}