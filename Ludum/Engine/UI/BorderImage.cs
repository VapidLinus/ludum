using Ludum.Engine;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Ludum.UI
{
	public class BorderImage : AbstractElement
	{
		protected const int OUTLINE_WIDTH = 20;

		private VertexArray vertexArray;
		private Texture texture;

		private Vector2f position;
		private Vector2f size;

		private bool isDirty = false;
		private bool isSelected = false;


		public virtual bool IsSelected
		{
			get { return isSelected; }
			protected set { isSelected = value; }
		}

		public Texture Texture
		{
			get { return texture; }
			set
			{
				if (value == null) throw new ArgumentNullException("Texture may not be null.");
				texture = value;
				isDirty = true;
			}
		}

		public override Vector2f Position
		{
			get { return position; }
			set
			{
				position = value;
				isDirty = true;
			}
		}
		public Vector2f Size
		{
			get { return size; }
			set
			{
				size = value;
				isDirty = true;
			}
		}

		public BorderImage(Vector2f position, Vector2f size)
		{
			Position = position;
			Size = size;
			Texture = GUI.Graphics_ButtonIdle;
		}

		public override bool OnClick(Vector2f mouse)
		{
			return IsSelected = mouse.X > Position.X && mouse.X < (Position.X + Size.X) && mouse.Y > Position.Y && mouse.Y < (Position.Y + Size.Y);
		}

		private void Recalculate()
		{
			Vertex[] vertices = new Vertex[16];

			float[] positionsX = new float[4],
				positionsY = new float[4],
				texturesX = new float[4],
				texturesY = new float[4];

			for (int i = 0; i < 4; i++)
			{
				positionsX[i] = ((i >= 1) ? OUTLINE_WIDTH : 0) + ((i >= 2) ? size.X - OUTLINE_WIDTH * 2 : 0) + ((i >= 3) ? OUTLINE_WIDTH : 0);
				positionsY[i] = ((i >= 1) ? OUTLINE_WIDTH : 0) + ((i >= 2) ? size.Y - OUTLINE_WIDTH * 2 : 0) + ((i >= 3) ? OUTLINE_WIDTH : 0);
				texturesX[i] = ((i >= 2) ? texture.Size.X : 0) + (i == 1 ? OUTLINE_WIDTH : 0) - (i == 2 ? OUTLINE_WIDTH : 0);
				texturesY[i] = ((i >= 2) ? texture.Size.Y : 0) + (i == 1 ? OUTLINE_WIDTH : 0) - (i == 2 ? OUTLINE_WIDTH : 0);
			}

			for (int y = 0; y < 4; y++)
			{
				for (int x = 0; x < 4; x++)
				{
					vertices[y * 4 + x] = new Vertex(
						new Vector2f(position.X + positionsX[x], position.Y + positionsY[y]),
						new Vector2f(texturesX[x], texturesY[y]));
				}
			}

			vertexArray = new VertexArray(PrimitiveType.Quads, 16);

			// Top Left
			vertexArray.Append(vertices[0]);
			vertexArray.Append(vertices[1]);
			vertexArray.Append(vertices[5]);
			vertexArray.Append(vertices[4]);
			// Top Center
			vertexArray.Append(vertices[1]);
			vertexArray.Append(vertices[2]);
			vertexArray.Append(vertices[6]);
			vertexArray.Append(vertices[5]);
			// Top Right
			vertexArray.Append(vertices[2]);
			vertexArray.Append(vertices[3]);
			vertexArray.Append(vertices[7]);
			vertexArray.Append(vertices[6]);
			// Center Left
			vertexArray.Append(vertices[4]);
			vertexArray.Append(vertices[5]);
			vertexArray.Append(vertices[9]);
			vertexArray.Append(vertices[8]);
			// Center Center
			vertexArray.Append(vertices[5]);
			vertexArray.Append(vertices[6]);
			vertexArray.Append(vertices[10]);
			vertexArray.Append(vertices[9]);
			// Center Right
			vertexArray.Append(vertices[6]);
			vertexArray.Append(vertices[7]);
			vertexArray.Append(vertices[11]);
			vertexArray.Append(vertices[10]);
			// Bottom Left
			vertexArray.Append(vertices[8]);
			vertexArray.Append(vertices[9]);
			vertexArray.Append(vertices[13]);
			vertexArray.Append(vertices[12]);
			// Bottom Center
			vertexArray.Append(vertices[9]);
			vertexArray.Append(vertices[10]);
			vertexArray.Append(vertices[14]);
			vertexArray.Append(vertices[13]);
			// Bottom Right
			vertexArray.Append(vertices[10]);
			vertexArray.Append(vertices[11]);
			vertexArray.Append(vertices[15]);
			vertexArray.Append(vertices[14]);

			/* 0, 1, 2, 3
			** 4, 5, 6, 7
			** 8, 9, 10, 11
			** 12, 13, 14, 15
			** Rectangle shape */
		}

		public override void Draw(RenderWindow window)
		{
			if (isDirty)
			{
				Recalculate();
				isDirty = false;
			}

			window.Draw(vertexArray, new RenderStates(texture));
		}
	}
}