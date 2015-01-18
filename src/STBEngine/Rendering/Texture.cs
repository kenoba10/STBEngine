using System;
using System.IO;

using OpenTK.Graphics.OpenGL;

using STBEngine.Utilities;

namespace STBEngine.Rendering
{

	public class Texture
	{

		private int texture;

		private bool initialized;

		public Texture()
		{

			texture = 0;

			initialized = false;

		}

		public void LoadTexture(Stream stream)
		{

			int width;
			int height;

			IntPtr data = IOUtils.LoadTexture(stream, out width, out height);

			texture = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float) TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float) TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) TextureWrapMode.Repeat);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			GL.BindTexture(TextureTarget.Texture2D, 0);

			initialized = true;

		}

		public void LoadTexture(IntPtr data, int width, int height)
		{

			texture = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float) TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float) TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) TextureWrapMode.Repeat);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			GL.BindTexture(TextureTarget.Texture2D, 0);

			initialized = true;

		}

		public void LoadTexture(IntPtr data, int width, int height, TextureMinFilter minFilter, TextureMagFilter magFilter, TextureWrapMode wrapMode)
		{

			texture = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float) minFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float) magFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) wrapMode);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) wrapMode);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			GL.BindTexture(TextureTarget.Texture2D, 0);

			initialized = true;

		}

		public void Bind(TextureUnit unit)
		{

			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, texture);

		}

		public void UnBind()
		{

			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, 0);

		}

		public void UnloadTexture()
		{

			initialized = false;

			GL.DeleteTexture(texture);

		}

		public bool Initialized
		{

			get
			{

				return initialized;

			}

		}

	}

}