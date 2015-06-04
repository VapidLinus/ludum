using SFML.Audio;
using SFML.Graphics;
using System.Collections.Generic;

namespace Ludum.Engine
{
	public static class Resources
	{
		private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
		private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();
        private static Dictionary<string, SoundBuffer> sounds = new Dictionary<string, SoundBuffer>();

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

        public static Music LoadMusic(string path)
        {
            return new Music(path);
        }

        public static Sound LoadSound(string path)
		{
			bool exists = sounds.ContainsKey(path);

			SoundBuffer buffer = exists ? sounds[path] : new SoundBuffer(path);
			if (!exists) sounds.Add(path, buffer);

			return new Sound(buffer);
		}
	}
}