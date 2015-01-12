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
		private Keyboard.Key[] allKeys = (Keyboard.Key[])Enum.GetValues(typeof(Keyboard.Key));

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
	}
}