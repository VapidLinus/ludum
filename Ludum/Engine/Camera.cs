using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum.Engine
{
	public class Camera : GameObject
	{
		private double zoom = 100;
		public double Zoom { get { return zoom; } set { this.zoom = value; } }
	}
}