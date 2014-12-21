using System;

using OpenTK;
using OpenTK.Graphics;

using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Material
	{

		private Color4 color;
		private Texture texture;

		private float ambientLight;

		public Material()
		{

			color = new Color4(255, 255, 255, 255);
			texture = new Texture();

			ambientLight = 1f;

		}

		public void Initialize()
		{

		}

		public void Terminate()
		{

			texture.UnloadTexture();

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

		public Texture Texture
		{

			get
			{

				return texture;

			}
			set
			{

				this.texture = value;

			}

		}

		public float AmbientLight
		{

			get
			{

				return ambientLight;

			}
			set
			{

				this.ambientLight = value;

			}

		}

	}

}