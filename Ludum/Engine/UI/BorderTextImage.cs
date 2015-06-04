using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum.UI
{
	public class BorderTextImage : BorderImage
	{
		protected SFML.Graphics.Text text;

		private bool autoScale = false;

		public override Vector2f Position
		{
			get { return base.Position; }
			set
			{
				base.Position = value;
				if (text != null)
					text.Position = new Vector2f(Position.X + OUTLINE_WIDTH / 2, Position.Y + OUTLINE_WIDTH / 2);
			}
		}

		public virtual string Text
		{
			get { return text.DisplayedString; }
			set
			{
				text.DisplayedString = value;
				if (autoScale) OnTextChange();
			}
		}

		public bool AutoScale
		{
			get { return autoScale; }
			set { autoScale = value; }
		}

		public Font Font
		{
			get { return text.Font; }
			set
			{
				text = new SFML.Graphics.Text("Default Text", value, 16);
				text.Color = Color.Black;
			}
		}

		public BorderTextImage(Vector2f position, Vector2f size)
			: base(position, size)
		{
			Font = GUI.Graphics_Font;
			Position = position;
		}

		void OnTextChange()
		{
			if (autoScale)
			{
				Size = new Vector2f(text.GetLocalBounds().Width + OUTLINE_WIDTH, Size.Y);
			}
		}

		public override void Draw(RenderWindow window)
		{
			base.Draw(window);

			window.Draw(text);
		}
	}
}