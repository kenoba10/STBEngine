using System;

using OpenTK.Graphics;

using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Material
	{

		private Texture texture;
		private Color4 color;

		public Material()
		{

			texture = new Texture();
			color = new Color4(255, 255, 255, 255);

		}

		public void Initialize()
		{

		}

		public void Terminate()
		{

			texture.UnloadTexture();

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

	}

}