using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public delegate void OnClicked(Button button);

	public class Button : BorderTextImage
	{
		public event OnClicked OnClickedHandler;

		private Texture idleTexture, hoverTexture, downTexture;
		private bool isDown;

		public Texture IdleTexture
		{
			get { return idleTexture; }
			set { idleTexture = value; }
		}
		public Texture HoverTexture
		{
			get { return hoverTexture; }
			set { hoverTexture = value; }
		}
		public Texture DownTexture
		{
			get { return downTexture; }
			set { downTexture = value; }
		}

		public override bool IsVisible
		{
			get { return base.IsVisible; }
			set
			{
				base.IsVisible = value;
				if (!value) isDown = false;
			}
		}

		public Button(Vector2f position, Vector2f size)
			: base(position, size)
		{
			IdleTexture = GUI.Graphics_ButtonIdle;
			HoverTexture = GUI.Graphics_ButtonHover;
			DownTexture = GUI.Graphics_ButtonDown;
		}

		public override void Update()
		{
			Vector2f mouse = Input.GetMousePosition();

			bool hovering = false;
			if (mouse.X > Position.X && mouse.X < Position.X + Size.X && mouse.Y > Position.Y && mouse.Y < Position.Y + Size.Y)
			{
				hovering = true;
			}

			if (isDown && Input.IsMouseReleased(Mouse.Button.Left))
			{
				isDown = false;
				Texture = idleTexture;
				if (hovering)
				{
					if (OnClickedHandler != null)
						OnClickedHandler(this);
				}
			}

			if (!isDown && hovering && hoverTexture != null) Texture = hoverTexture;
			else if (!isDown) Texture = idleTexture;
		}

		public override bool OnClick(Vector2f mouse)
		{
			bool value = base.OnClick(mouse);

			if (IsSelected)
			{
				isDown = true;
				if (downTexture != null) Texture = downTexture;
			}

			return value;
		}
	}
}