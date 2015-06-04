using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Ludum.UI
{
	public delegate void OnDone(PopupTextfield sender, string text);

	public class PopupTextfield : AbstractElement
	{
		public event OnDone OnDone;

		public override Vector2f Position
		{
			get { return background.Position; }
			set { background.Position = value; }
		}

		public string ButtonText
		{
			get { return button.Text; }
			set { button.Text = value; }
		}

		private BorderImage background;
		private TextField field;
		private Button button;

		public PopupTextfield(string initialText = "Enter Text") : this(new Vector2f(400, 200), initialText) { }

		public PopupTextfield(Vector2f size, string initialText = "Enter Text")
		{
			float centerX = Render.WindowWidth / 2 - size.X / 2;
			float centerY = Render.WindowHeight / 2 - size.Y / 2;

			background = new BorderImage(new Vector2f(centerX, centerY), size)
			{
				Layer = 198
			};

			field = new TextField(new Vector2f(0, 0), new Vector2f(0, 40))
			{
				AutoScale = true,
				Text = initialText,
				Layer = 200
			};

			const int BUTTON_WIDTH = 60;
			button = new Button(new Vector2f(Render.WindowWidth / 2 - BUTTON_WIDTH / 2, background.Position.Y + size.Y - 60), new Vector2f(BUTTON_WIDTH, 40))
			{
				Text = "Okay",
				Layer = 200
			};

			// When the okay button is clicked, dispose self
			button.OnClickedHandler += (button) =>
			{
				// Call event and then dispose
				if (OnDone != null) OnDone(this, field.Text);
				Dispose();
			};
		}

		public override void Update()
		{
			field.Position = new Vector2f(
				Render.WindowWidth / 2 - field.Size.X / 2,
				Render.WindowHeight / 2 - field.Size.Y / 2);
		}

		public override void Dispose()
		{
			base.Dispose();

			// Dipose resources
			background.Dispose();
			field.Dispose();
			button.Dispose();
		}

		public override bool OnClick(Vector2f position)
		{
			// Block all clicks behind this popup
			return true;
		}

		public override void Draw(RenderWindow window) { }
	}
}