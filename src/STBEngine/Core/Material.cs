using System;

using OpenTK.Graphics;

using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Material
	{

		private Color4 color;
		private Texture texture;

		private Texture displacementMap;
		private Texture normalMap;

		private float displacementScale;
		private float displacementOffset;

		private float ambientLight;

		private float specularIntensity;
		private float specularExponent;

		public Material()
		{

			color = new Color4(255, 255, 255, 255);
			texture = new Texture();

			displacementMap = new Texture();
			normalMap = new Texture();

			displacementScale = 0.04f;
			displacementOffset = 0f;

			ambientLight = 1f;

			specularIntensity = 2f;
			specularExponent = 32f;

		}

		public void Initialize()
		{

		}

		public void Terminate()
		{

			normalMap.UnloadTexture();
			displacementMap.UnloadTexture();

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

		public Texture DisplacementMap
		{

			get
			{

				return displacementMap;

			}
			set
			{

				this.displacementMap = value;

			}

		}

		public Texture NormalMap
		{

			get
			{

				return normalMap;

			}
			set
			{

				this.normalMap = value;

			}

		}

		public float DisplacementScale
		{

			get
			{

				return displacementScale;

			}
			set
			{

				this.displacementScale = value;

			}

		}

		public float DisplacementOffset
		{

			get
			{

				return displacementOffset;

			}
			set
			{

				this.displacementOffset = value;

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