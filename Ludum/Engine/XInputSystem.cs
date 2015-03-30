using System;
using System.Collections.Generic;
using XInputDotNetPure;

/*
 * This could be optimized A LOT.
 * I should probably do that @-@
 * Use 2D arrays and a custom enum for states, silly Linus
 */
namespace Ludum.Engine
{
	internal class XInputSystem
	{
		private const int CONTROLLERS = 4;

		private List<XInputButton>[] buttonPressed = new List<XInputButton>[CONTROLLERS];
		private List<XInputButton>[] buttonHeld = new List<XInputButton>[CONTROLLERS];
		private List<XInputButton>[] buttonReleased = new List<XInputButton>[CONTROLLERS];

		private XInputButton[] allButtons = (XInputButton[])Enum.GetValues(typeof(XInputButton));

		public XInputSystem()
		{
			for (int i = 0; i < CONTROLLERS; i++)
			{
				buttonPressed[i] = new List<XInputButton>();
				buttonHeld[i] = new List<XInputButton>();
				buttonReleased[i] = new List<XInputButton>();
			}
		}

		internal void Update()
		{
			UpdateController(PlayerIndex.One);
			UpdateController(PlayerIndex.Two);
			UpdateController(PlayerIndex.Three);
			UpdateController(PlayerIndex.Four);
		}

		private void UpdateController(PlayerIndex playerIndex)
		{
			int index = (int)playerIndex;

			// Move all keys from buttonPressed to buttonHeld
			for (int i = 0; i < buttonPressed[index].Count; i++) buttonHeld[index].Add(buttonPressed[index][i]);
			buttonPressed[index].Clear();
			buttonReleased[index].Clear(); // Clear all released key

			for (int i = 0; i < allButtons.Length; i++)
			{
				XInputButton key = allButtons[i];

				// If key is pressed
				if (CheckButtonDown(playerIndex, key))
				{
					// If key was not previously held
					if (!buttonHeld[index].Contains(key))
					{
						// Mark this key as just pressed
						buttonPressed[index].Add(key);
					}
				}
				else // Key not pressed
				{
					// If this key is marked as pressed
					if (buttonHeld[index].Contains(key))
					{
						// Remove it and mark it as released
						buttonHeld[index].Remove(key);
						buttonReleased[index].Add(key);
					}
				}
			}
		}

		private bool CheckButtonDown(PlayerIndex index, XInputButton button)
		{
			var state = GamePad.GetState(index, GamePadDeadZone.Circular);

			switch (button)
			{
				case XInputButton.A: return state.Buttons.A == ButtonState.Pressed;
				case XInputButton.B: return state.Buttons.B == ButtonState.Pressed;
				case XInputButton.X: return state.Buttons.X == ButtonState.Pressed;
				case XInputButton.Y: return state.Buttons.Y == ButtonState.Pressed;
				case XInputButton.Back: return state.Buttons.Back == ButtonState.Pressed;
				case XInputButton.Start: return state.Buttons.Start == ButtonState.Pressed;
				case XInputButton.Guide: return state.Buttons.Guide == ButtonState.Pressed;
				case XInputButton.LeftShoulder: return state.Buttons.LeftShoulder == ButtonState.Pressed;
				case XInputButton.LeftStick: return state.Buttons.LeftStick == ButtonState.Pressed;
				case XInputButton.RightShoulder: return state.Buttons.RightShoulder == ButtonState.Pressed;
				case XInputButton.RightStick: return state.Buttons.RightStick == ButtonState.Pressed;
				default: throw new NotImplementedException();
			}
		}

		public bool IsButtonPressed(PlayerIndex index, XInputButton button)
		{
			return buttonPressed[(int)index].Contains(button);
		}
		public bool IsButtonDown(PlayerIndex index, XInputButton button)
		{
			return buttonHeld[(int)index].Contains(button) || buttonPressed[(int)index].Contains(button);
		}
		public bool IsButtonReleased(PlayerIndex index, XInputButton button)
		{
			return buttonReleased[(int)index].Contains(button);
		}
	}
}