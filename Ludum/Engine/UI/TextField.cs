using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Windows.Forms;
using Text = SFML.Graphics.Text;

namespace Ludum.UI
{
	public delegate void OnTextConfirmed(TextField sender);

	public class TextField : BorderTextImage
	{
		public event OnTextConfirmed OnTextConfirmedHandler;

		private int cursor = 0;
		private bool blockTextInput = false;


		public override bool IsSelected
		{
			get { return base.IsSelected; }
			protected set
			{
				bool alreadySelected = IsSelected;
				base.IsSelected = value;

				// Only update when value toggles
				if (alreadySelected == value) return;
				Text = Text;
				if (!IsSelected && OnTextConfirmedHandler != null) OnTextConfirmedHandler(this);
			}
		}
		public override bool IsVisible
		{
			get { return base.IsVisible; }

			set { base.IsVisible = value; }
		}

		private string realText;
		public override string Text
		{
			get { return realText; }
			set
			{
				realText = value;

				cursor = MathUtil.Clamp(cursor, 0, value.Length);

				string display = value;
				if (IsSelected) display = display.Insert(cursor, "|");

				base.Text = display;
			}
		}

		public int CursorPosition
		{
			get { return cursor; }
			set
			{
				cursor = value;
				Text = Text;
			}
		}

		public TextField(Vector2f position, Vector2f size)
			: base(position, size)
		{
			Render.Window.TextEntered += Window_TextEntered;
			Render.Window.KeyPressed += Window_KeyPressed;
		}

		private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
		{
			if (!IsSelected) return;

			if (e.Control)
			{
				if (e.Code == Keyboard.Key.C)
				{
					Clipboard.SetText(Text);
					blockTextInput = true;
				}
				else if (e.Code == Keyboard.Key.V)
				{
					Text = Text.Insert(cursor, Clipboard.GetText());
					blockTextInput = true;
				}
			}
			else
			{
				if (e.Code == Keyboard.Key.Left)
				{
					CursorPosition--;
				}
				else if (e.Code == Keyboard.Key.Right)
				{
					CursorPosition++;
				}
				else if (e.Code == Keyboard.Key.Return)
				{
					if (OnTextConfirmedHandler != null) OnTextConfirmedHandler(this);
					blockTextInput = true;
				}
			}
		}

		private void Window_TextEntered(object sender, TextEventArgs e)
		{
			if (!IsSelected) return;

			// Don't handle input if the key press event did something
			if (blockTextInput)
			{
				blockTextInput = false;
				return;
			}

			string unicode = e.Unicode;
			if (unicode == "\b")
			{
				// Backspace, delete text
				if (Text.Length > 0)
					Text = Text.Substring(0, Text.Length - 1);
			}
			else
			{
				// Append text
				Text = Text.Insert(cursor++, unicode);
			}
		}

		public override bool OnClick(Vector2f mouse)
		{
			bool value = base.OnClick(mouse);

			// If not selected, return
			if (!IsSelected) return value;

			foreach (var textfield in GUI.GetElements<TextField>())
			{
				if (this != textfield) textfield.IsSelected = false;
			}

			float x = text.GetGlobalBounds().Left;

			int closestIndex = 0;
			float closestDistance = float.MaxValue;
			for (int i = 0; i < text.DisplayedString.Length; i++)
			{
				uint point = (uint)char.ConvertToUtf32(text.DisplayedString, Math.Max(i - 1, 0));
				uint point2 = (uint)char.ConvertToUtf32(text.DisplayedString, i);

				float glyphWidth = text.Font.GetGlyph(point, text.CharacterSize, false).Advance;

				x += glyphWidth;

				float distance = Math.Abs(x - mouse.X);
				if (distance < closestDistance)
				{
					closestIndex = i;
					closestDistance = distance;
				}
			}

			CursorPosition = closestIndex;

			return value;
		}
	}
}