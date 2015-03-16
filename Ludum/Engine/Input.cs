using SFML.Window;

namespace Ludum.Engine
{
	public class Input : SingleInstance<Input>
	{
		private readonly Core core;
		private readonly InputSystem fixedUpdateInput, normalUpdateInput;

		public Input(Core core)
		{
			SetInstance(this);

			this.core = core;

			fixedUpdateInput = new InputSystem();
			normalUpdateInput = new InputSystem();
		}

		internal void Update()
		{
			normalUpdateInput.Update();
		}

		internal void FixedUpdate()
		{
			fixedUpdateInput.Update();
		}

		public static bool IsKeyPressed(Keyboard.Key key)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsKeyPressed(key) :
				Instance.normalUpdateInput.IsKeyPressed(key);
		}
		public static bool IsKeyDown(Keyboard.Key key)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsKeyDown(key) :
				Instance.normalUpdateInput.IsKeyDown(key);
		}
		public static bool IsKeyReleased(Keyboard.Key key)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsKeyReleased(key) :
				Instance.normalUpdateInput.IsKeyReleased(key);
		}

		public static bool IsMousePressed(Mouse.Button button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsMousePressed(button) :
				Instance.normalUpdateInput.IsMousePressed(button);
		}
		public static bool IsMouseDown(Mouse.Button button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsMouseDown(button) :
				Instance.normalUpdateInput.IsMouseDown(button);
		}
		public static bool IsMouseReleased(Mouse.Button button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateInput.IsMouseReleased(button) :
				Instance.normalUpdateInput.IsMouseReleased(button);
		}

		public static Vector2f GetMousePosition()
		{
			Vector2i mouse = Mouse.GetPosition(Render.Window);
			return new Vector2f(mouse.X, mouse.Y);
		}
	}
}