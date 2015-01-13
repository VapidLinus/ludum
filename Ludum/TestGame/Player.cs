using System;
using Ludum.Engine;
using SFML.Window;
using SFML.Graphics;

namespace Ludum.TestGame
{
	public class Player : GameObject
	{
		private Vector2 velocity = Vector2.Zero;

		private ShapeRenderer renderer;
		private CircleCollider collider;

		public override void OnStart()
		{
			base.OnStart();

			renderer = AddComponent<ShapeRenderer>();
			collider = AddComponent<CircleCollider>();

			collider.Radius = 20;
			renderer.SetShape(new CircleShape(20));;
		}

		public override void OnUpdate(float delta)
		{
			base.OnUpdate(delta);

			if (Collider.CheckCollision(collider))
			{
				
			}

			if (Input.IsKeyDown(Keyboard.Key.Left))
			{
				velocity.x -= delta * 1000;
			}
			if (Input.IsKeyDown(Keyboard.Key.Right))
			{
				velocity.x += delta * 1000;
			}
			if (Input.IsKeyDown(Keyboard.Key.Up))
			{
				velocity.y += delta * 1000;
			}
			if (Input.IsKeyDown(Keyboard.Key.Down))
			{
				velocity.y -= delta * 1000;
			}

			velocity.x *= (float)Math.Pow(0.001f, delta);
			velocity.y *= (float)Math.Pow(0.001f, delta);

			Position += velocity * delta;
		}
	}
}