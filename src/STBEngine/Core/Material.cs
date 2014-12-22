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

		private float specularIntensity;
		private float specularExponent;

		public Material()
		{

			color = new Color4(255, 255, 255, 255);
			texture = new Texture();

			ambientLight = 1f;

			specularIntensity = 2f;
			specularExponent = 32f;

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

		public float SpecularIntensity
		{

			get
			{

				return specularIntensity;

			}
			set
			{

				this.specularIntensity = value;

			}

		}

		public float SpecularExponent
		{

			get
			{

				return specularExponent;

			}
			set
			{

				this.specularExponent = value;

			}

		}

	}

}