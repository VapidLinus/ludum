using System.Collections.Generic;
using System.IO;
using System;

namespace Ludum.Engine
{
	public class Config
	{
		private readonly string path;
		private readonly string filename;

		private Dictionary<string, string> dictionary;

		public Config(string path, string filename, bool load = true)
		{
			this.path = path;
			this.filename = filename;

			dictionary = new Dictionary<string, string>();

			if (load) Load();
		}

		public void Save()
		{
			Directory.CreateDirectory(path);

			var writer = new StreamWriter(File.Open(Path.Combine(path, filename), FileMode.Create));
			foreach (var pair in dictionary)
			{
				writer.WriteLine(pair.Key);
				writer.WriteLine(pair.Value);
			}
			writer.Close();
		}

		public void Load()
		{
			if (!File.Exists(path)) return;

			string[] lines = File.ReadAllLines(path);
			for (int i = 0; i < lines.Length; i += 2)
			{
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

		public void SetValue(string key, string value)
		{
			if (key == null) throw new ArgumentNullException("key");

			// Add/Update value of key
			if (dictionary.ContainsKey(key)) dictionary[key] = value;
			else dictionary.Add(key, value);
		}
	}
}