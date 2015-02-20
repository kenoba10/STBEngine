using System;

using OpenTK.Graphics;

namespace STBEngine.Core.Components
{

	public class BaseLight
	{

		private Color4 color;
		private float intensity;

		public BaseLight(Color4 color, float intensity)
		{

			this.color = color;
			this.intensity = intensity;

		}

		public Color4 Color
		{

			get
			{

				return color;

			}
			set
			{

				this.color = value;

			}

		}

		public float Intensity
		{

			get
			{

				return intensity;

			}
			set
			{

				this.intensity = value;

			}

		}

	}

}