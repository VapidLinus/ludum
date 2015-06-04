using System.Collections.Generic;
using System.IO;
using System;

namespace Ludum.Engine
{
	public class Config
	{
		private readonly string path;

		private Dictionary<string, string> dictionary;

		public Config(string path, bool load = true)
		{
			this.path = path;

			dictionary = new Dictionary<string, string>();

			if (load) Load();
		}

		public void Save()
		{
			/*string directory = Path.GetDirectoryName(path);
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(path);*/

			var writer = new StreamWriter(File.Open(path, FileMode.Create));
			foreach (var pair in dictionary)
			{
				writer.WriteLine(pair.Key);
				writer.WriteLine(pair.Value);
			}
			writer.Close();
		}

		public void Load()
		{
			if (!File.Exists(path))
			{
				Debug.LogWarning("[Config] Path not found: " + path);
				return;
			}

			string[] lines = File.ReadAllLines(path);
			for (int i = 0; i < lines.Length; i += 2)
			{
				if (dictionary.ContainsKey(lines[i]))
				{
					Debug.LogWarning("Key already exists: " + lines[i]);
					continue;
				}
				dictionary.Add(lines[i], lines[i + 1]);
			}
		}

		public string GetValue(string key, string @default)
		{
			if (key == null) throw new ArgumentNullException("key");

			foreach (var pair in dictionary)
			{
				if (pair.Key != key) continue;
				return pair.Value;
			}

			SetValue(key, @default);
			return @default;
		}

		public void SetValue(string key, object value)
		{
			if (key == null) throw new ArgumentNullException("key");

			// Add/Update value of key
			if (dictionary.ContainsKey(key)) dictionary[key] = value.ToString();
			else dictionary.Add(key, value.ToString());
		}
	}
}