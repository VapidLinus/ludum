using SFML.Window;
using System;
using XInputDotNetPure;

namespace Ludum.Engine
{
	public class Input : SingleInstance<Input>
	{
		private readonly Core core;
		private readonly InputSystem fixedUpdateInput, normalUpdateInput;
		private readonly XInputSystem fixedUpdateXInput, normalUpdateXInput;

		public Input(Core core)
		{
			SetInstance(this);

			this.core = core;

			fixedUpdateInput = new InputSystem();
			normalUpdateInput = new InputSystem();
			fixedUpdateXInput = new XInputSystem();
			normalUpdateXInput = new XInputSystem();
		}

		internal void Update()
		{
			normalUpdateInput.Update();
			normalUpdateXInput.Update();
		}

		internal void FixedUpdate()
		{
			fixedUpdateInput.Update();
			fixedUpdateXInput.Update();
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

		public static bool IsButtonPressed(PlayerIndex index, XInputButton button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateXInput.IsButtonPressed(index, button) :
				Instance.normalUpdateXInput.IsButtonPressed(index, button);
		}
		public static bool IsButtonDown(PlayerIndex index, XInputButton button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateXInput.IsButtonDown(index, button) :
				Instance.normalUpdateXInput.IsButtonDown(index, button);
		}
		public static bool IsButtonReleased(PlayerIndex index, XInputButton button)
		{
			return Instance.core.updateState == Core.UpdateState.FixedUpdate ?
				Instance.fixedUpdateXInput.IsButtonReleased(index, button) :
				Instance.normalUpdateXInput.IsButtonReleased(index, button);
		}

		public static float GetAxis(PlayerIndex index, XInputAxis axis)
		{
			var state = GamePad.GetState(index);
			float value = 0f;

			switch (axis)
			{
				case XInputAxis.LeftX: value = state.ThumbSticks.Left.X; break;
				case XInputAxis.LeftY: value = state.ThumbSticks.Left.Y; break;
				case XInputAxis.RightX: value = state.ThumbSticks.Right.X; break;
				case XInputAxis.RightY: value = state.ThumbSticks.Right.Y; break;
			}

			const float DEADZONE = .2f;
			const float HIGH = 1.01f;

			if (Math.Abs(value) < DEADZONE) return 0f;
			return MathUtil.Clamp(value * HIGH, -1f, 1f);
        }

		public static Vector2f GetMousePosition()
		{
			Vector2i mouse = Mouse.GetPosition(Render.Window);
			return new Vector2f(mouse.X, mouse.Y);
		}

		public static void SetMousePosition(Vector2f position)
		{
			Mouse.SetPosition(new Vector2i((int)position.X, (int)position.Y), Render.Window);
		}
	}
}