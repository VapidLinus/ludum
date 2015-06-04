using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ludum.UI
{
	public class GUI : SingleInstance<GUI>
	{
		public static Texture Graphics_ButtonIdle { get; set; }
		public static Texture Graphics_ButtonHover { get; set; }
		public static Texture Graphics_ButtonDown { get; set; }
		public static Font Graphics_Font { get; set; }

		private bool isCacheDirty = false;
		private int id = 0;

		private Dictionary<int, IElement> elements;
		private List<IElement> chachedElements;

		public GUI()
		{
			DefaultGraphics();

			SetInstance(this);
			elements = new Dictionary<int, IElement>();
			chachedElements = new List<IElement>();
		}

		void DefaultGraphics()
		{
			const int WIDTH = 20 * 3;
			Color[,] idle = new Color[WIDTH, WIDTH];
			Color[,] hover = new Color[WIDTH, WIDTH];
			Color[,] down = new Color[WIDTH, WIDTH];
			for (int x = 0; x < WIDTH; x++)
			{
				for (int y = 0; y < WIDTH; y++)
				{
					bool center = (x > 5 && x < 55 && y > 5 && y < 55);
					idle[x, y] = center ? Color.White : Color.Black;
					hover[x, y] = center ? new Color(100, 100, 100) : Color.Black;
					down[x, y] = center ? new Color(50, 50, 50) : Color.Black;
				}
			}

			Graphics_ButtonIdle = new Texture(new Image(idle));
			Graphics_ButtonHover = new Texture(new Image(hover));
			Graphics_ButtonDown = new Texture(new Image(down));

			Graphics_Font = Resources.LoadFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "consola.ttf"));
        }

		internal void Update()
		{
			// Rebuild cache if it's dirty
			if (isCacheDirty) 
			{
				RebuildCache();
				isCacheDirty = false;
			}

			var reversed = new List<IElement>(chachedElements);
			reversed.Reverse();

			foreach (IElement element in reversed)
			{
				if (element.IsVisible) element.Update();
			}

			if (Input.IsMousePressed(Mouse.Button.Left))
			{
				Vector2f mouse = Input.GetMousePosition();
				foreach (IElement element in reversed)
				{
					if (element.OnClick((Vector2f)mouse))
						break;
				}
			}
		}

		internal void Render()
		{
			// Rebuild cache if it's dirty
			if (isCacheDirty)
			{
				RebuildCache();
				isCacheDirty = false;
			}

			foreach (IElement element in chachedElements)
			{
				if (element.IsVisible)
					element.Draw(Engine.Render.Window);
			}
		}

		public void RebuildCache()
		{
			chachedElements = new List<IElement>(elements.Values);
			chachedElements.Sort();
		}

		#region Static
		internal static int AddElement(IElement element)
		{
			Instance.elements.Add(Instance.id, element);
			Instance.isCacheDirty = true;

			return Instance.id++;
		}

		internal static void RemoveElement(int id)
		{
			Instance.elements.Remove(id);
			Instance.isCacheDirty = true;
		}

		public static T GetElement<T>(int id)
		{
			return (T)Instance.elements[id];
		}

		public static HashSet<T> GetElements<T>()
		{
			var set = new HashSet<T>();
			foreach (var element in Instance.elements.Values)
			{
				if (element is T) set.Add((T)element);
			}
			return set;
		}
		#endregion
	}
}