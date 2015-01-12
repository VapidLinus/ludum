using System;
using SFML.Window;

namespace Ludum.Engine
{
	public struct RenderMode
	{
		public readonly VideoMode videoMode;
		public readonly string title;
		public readonly Styles style;

		public RenderMode(VideoMode videoMode, string title, Styles style)
		{
			this.videoMode = videoMode;
			this.title = title;
			this.style = style;
		}
	}
}