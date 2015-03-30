using SFML.Graphics;
using System.Collections.Generic;

namespace Ludum.Engine
{
	public static class Resources
	{
		private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
		private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

		public static Texture LoadTexture(string path, bool smooth = false)
		{
			bool exists = textures.ContainsKey(path);

			Texture original = exists ? textures[path] : new Texture(path);
			if (!exists) textures.Add(path, original);
			Texture clone = new Texture(original);

			clone.Smooth = smooth;
			return clone;
		}

		public static Font LoadFont(string path)
		{
			bool exists = fonts.ContainsKey(path);

			Font original = exists ? fonts[path] : new Font(path);
			if (!exists) fonts.Add(path, original);
			Font clone = new Font(original);

			return clone;
		}
	}
}