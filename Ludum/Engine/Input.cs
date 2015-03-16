using System;
using System.Collections.Generic;
using SFML.Window;

/*
 * This could be optimized A LOT.
 * I should probably do that @-@
 * Use 2D arrays and a custom enum for states, silly Linus
 */
namespace Ludum.Engine
{
	public class Input : SingleInstance<Input>
	{
		private List<Keyboard.Key> keyPressed = new List<Keyboard.Key>();
		private List<Keyboard.Key> keyHeld = new List<Keyboard.Key>();
		private List<Keyboard.Key> keyReleased = new List<Keyboard.Key>();

		private List<Mouse.Button> mousePressed = new List<Mouse.Button>();
		private List<Mouse.Button> mouseHeld = new List<Mouse.Button>();
		private List<Mouse.Button> mouseReleased = new List<Mouse.Button>();

		private Keyboard.Key[] allKeys = (Keyboard.Key[])Enum.GetValues(typeof(Keyboard.Key));
		private Mouse.Button[] allMouseButtons = (Mouse.Button[])Enum.GetValues(typeof(Mouse.Button));

		public Input()
		{
			SetInstance(this);
		}

		internal void Update()
		{
			// Move all keys from keyPressed to keyHolds
			for (int i = 0; i < keyPressed.Count; i++) keyHeld.Add(keyPressed[i]);
			keyPressed.Clear();
			keyReleased.Clear(); // Clear all released key

			for (int i = 0; i < allKeys.Length; i++)
			{
				Keyboard.Key key = allKeys[i];

				// If key is pressed
				if (Keyboard.IsKeyPressed(key))
				{
					// If key was not previously held
					if (!keyHeld.Contains(key))
					{
						// Mark this key as just pressed
						keyPressed.Add(key);
					}
				}
				else // Key not pressed
				{
					// If this key is marked as pressed
					if (keyHeld.Contains(key))
					{
						// Remove it and mark it as released
						keyHeld.Remove(key);
						keyReleased.Add(key);
					}
				}
			}

			// Move all keys from keyPressed to keyHolds
			for (int i = 0; i < mousePressed.Count; i++) mouseHeld.Add(mousePressed[i]);
			mousePressed.Clear();
			mouseReleased.Clear(); // Clear all released key

			for (int i = 0; i < allMouseButtons.Length; i++)
			{
				Mouse.Button button = allMouseButtons[i];

				// If key is pressed
				if (Mouse.IsButtonPressed(button))
				{
					// If key was not previously held
					if (!mouseHeld.Contains(button))
					{
						// Mark this key as just pressed
						mousePressed.Add(button);
					}
				}
				else // Key not pressed
				{
					// If this key is marked as pressed
					if (mouseHeld.Contains(button))
					{
						// Remove it and mark it as released
						mouseHeld.Remove(button);
						mouseReleased.Add(button);
					}
				}
			}
		}

		public static bool IsKeyPressed(Keyboard.Key key)
		{
			return Instance.keyPressed.Contains(key);
		}
		public static bool IsKeyDown(Keyboard.Key key)
		{
			return Instance.keyHeld.Contains(key) || Instance.keyPressed.Contains(key);
		}
		public static bool IsKeyReleased(Keyboard.Key key)
		{
			return Instance.keyReleased.Contains(key);
		}

		public static bool IsMousePressed(Mouse.Button button)
		{
			return Instance.mousePressed.Contains(button);
		}
		public static bool IsMouseDown(Mouse.Button button)
		{
			return Instance.mouseHeld.Contains(button) || Instance.mousePressed.Contains(button);
		}
		public static bool IsMouseReleased(Mouse.Button button)
		{
			return Instance.mouseReleased.Contains(button);
		}

		public static Vector2f GetMousePosition()
		{
			Vector2i mouse = Mouse.GetPosition(Render.Window);
			return new Vector2f(mouse.X, mouse.Y);
		}
	}
}